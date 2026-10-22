using DChild.Gameplay.Characters.Players.Behaviour;
using DChild.Gameplay.Characters.Players.State;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.Modules
{
    public class WallMovement : MonoBehaviour, ICancellableBehaviour, IComplexCharacterModule
    {
        public enum SensorType
        {
            Overhead,
            Ceiling,
            Body
        }

        [SerializeField, HideLabel]
        private WallMovementStatsInfo m_configuration;
        [SerializeField]
        private RaySensor m_overheadSensor;
        [SerializeField]
        private RaySensor m_ceilingSensor;
        [SerializeField]
        private RaySensor m_bodySensor;

        private IWallStickState m_state;
        private Rigidbody2D m_rigibody;
        private Animator m_animator;
        private int m_animationParameter;
        private int m_yInputParameter;

        public void Initialize(ComplexCharacterInfo info)
        {
            m_state = info.state;
            m_rigibody = info.rigidbody;
            m_gravityCache = m_rigibody.gravityScale;
            m_animator = info.animator;
            m_animationParameter = info.animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsWallCrawling);
            m_yInputParameter = info.animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.YInput);
        }

        public void SetConfiguration(WallMovementStatsInfo info)
        {
            m_configuration.CopyInfo(info);
        }

        public bool IsThereAWall(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Overhead:
                    m_overheadSensor.Cast();
                    return m_overheadSensor.allRaysDetecting && m_overheadSensor.GetProminentHitCollider().gameObject.CompareTag("InvisibleWall") == false;
                case SensorType.Body:
                    m_bodySensor.Cast();
                    return m_bodySensor.allRaysDetecting;
                case SensorType.Ceiling:
                    m_ceilingSensor.Cast();
                    return m_ceilingSensor.allRaysDetecting;
                default:
                    return true;
            }
        }

        public void Cancel()
        {
            m_animator.SetBool(m_animationParameter, false);
            m_animator.SetFloat(m_yInputParameter, 0);
            m_rigibody.velocity = Vector2.zero;
        }
        private float m_gravityCache;
        /*public void Move(float direction)
        {
            if (m_state.canWallCrawl == true)
            {
                if (direction != 0)
                {
                    if (direction > 0 && IsThereAWall(SensorType.Ceiling))
                    {
                        m_rigibody.gravityScale = 0;
                        m_rigibody.velocity = Vector2.zero;
                        m_animator.SetBool(m_animationParameter, false);
                        m_animator.SetFloat(m_yInputParameter, direction);
                    }
                    else
                    {
                        m_rigibody.gravityScale = m_gravityCache;
                        *//*m_animator.SetBool(m_animationParameter, true);

                        m_rigibody.velocity = new Vector2(m_rigibody.velocity.x, m_configuration.speed * direction);
                        m_animator.SetFloat(m_yInputParameter, direction);*//*
                        m_rigibody.velocity = new Vector2(m_rigibody.velocity.x, m_configuration.speed * direction);
                        m_animator.SetBool(m_animationParameter, true);
                        m_animator.SetFloat(m_yInputParameter, direction);
                    }
                }
                else
                {
                    m_animator.SetBool(m_animationParameter, false);
                    m_rigibody.velocity = new Vector2(m_rigibody.velocity.x, m_configuration.speed * 0);
                    m_animator.SetFloat(m_yInputParameter, 0);
                }
            }
        }*/
        public void Move(float direction)
        {
            if (!m_state.canWallCrawl)
                return;

            if (direction > 0 && IsThereAWall(SensorType.Ceiling))
            {
                m_rigibody.gravityScale = 0;
                m_rigibody.velocity = Vector2.zero;

                m_animator.SetBool(m_animationParameter, false);
                m_animator.SetFloat(m_yInputParameter, 0f);
            }
            else
            {
                m_rigibody.gravityScale = m_gravityCache;

                m_rigibody.velocity = new Vector2(m_rigibody.velocity.x, m_configuration.speed * direction);

                m_animator.SetBool(m_animationParameter, direction != 0);
                m_animator.SetFloat(m_yInputParameter, direction);
            }
        }
    }
}
