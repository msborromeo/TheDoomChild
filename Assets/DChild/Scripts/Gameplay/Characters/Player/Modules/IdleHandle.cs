using DChild.Gameplay.Characters.Players.Behaviour;
using DChild.Gameplay.Narrative;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.Modules
{
    public class IdleHandle : MonoBehaviour, ICancellableBehaviour, IComplexCharacterModule
    {
        [SerializeField, HideLabel]
        private IdleHandleStatsInfo m_configuration;

        private Animator m_animator;
        private int m_idleAnimationParameter;
        private int m_idleStateAnimationParameter;
        private int m_currentIdleIndex;
        private float m_timer;
        private bool m_isInIdle;
        private bool m_isLyingDown = false;

        public void Initialize(ComplexCharacterInfo info)
        {
            m_animator = info.animator;
            m_idleAnimationParameter = info.animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsIdle);
            m_idleStateAnimationParameter = info.animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IdleState);
            m_currentIdleIndex = 0;
        }

        public void SetConfiguration(IdleHandleStatsInfo info)
        {
            m_configuration.CopyInfo(info);
        }

        public void GenerateRandomState()
        {
            m_currentIdleIndex = Random.Range(1, m_configuration.maxIdleAnimCount + 1);
            m_animator.SetInteger(m_idleStateAnimationParameter, m_currentIdleIndex);
        }

        public void Cancel()
        {
            m_isInIdle = false;
            m_animator.SetBool(m_idleAnimationParameter, false);
            m_currentIdleIndex = 0;
            m_animator.SetInteger(m_idleStateAnimationParameter, m_currentIdleIndex);
        }

        public void Execute(bool allowExtendedIdle)
        {
            m_animator.SetBool(m_idleAnimationParameter, true);
            if (allowExtendedIdle == true)
            {
                if (m_isInIdle && m_currentIdleIndex == 0)
                {
                    if (m_timer > 0)
                    {
                        m_timer -= GameplaySystem.time.deltaTime;
                        if (m_timer <= 0)
                        {
                            if(m_isLyingDown == false)
                            {
                                GenerateRandomState();
                            }
                        }
                    }
                }
                else
                {
                    m_isInIdle = true;
                    m_timer = m_configuration.playExtendedIdleAnimAfter;
                }
            }
            else
            {
                if (m_currentIdleIndex != 0)
                {
                    m_currentIdleIndex = 0;
                    m_animator.SetInteger(m_idleStateAnimationParameter, 0);
                }

                m_isInIdle = true;
            }
        }

        public void BackToDefaultIdle()
        {
            m_currentIdleIndex = 0;
            m_animator.SetInteger(m_idleStateAnimationParameter, m_currentIdleIndex);
        }

        private void Start()
        {
            NewGameIntroEvent.NewGameIntroStarted += OnNewGameIntroStarted;
            NewGameIntroEvent.NewGameIntroPromptPressed += OnWakeUpPressed;
        }

        private void OnDisable()
        {
            NewGameIntroEvent.NewGameIntroStarted -= OnNewGameIntroStarted;
            NewGameIntroEvent.NewGameIntroPromptPressed -= OnWakeUpPressed;
        }

        private void OnWakeUpPressed()
        {
            m_isLyingDown = false;
            m_timer = m_configuration.playExtendedIdleAnimAfter;
        }

        private void OnNewGameIntroStarted()
        {
            m_isLyingDown = true;
        }
    }
}
