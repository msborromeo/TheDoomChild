using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Characters.Players.State;
using DChild.Gameplay.Environment;
using DChild.Serialization;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.Gameplay
{
    public class EnvironmentTrigger : MonoBehaviour, ISerializableComponent
    {
        [System.Serializable]
        public struct SaveData : ISaveData
        {
            public SaveData(bool wasTriggered)
            {
                this.m_isTriggered = wasTriggered;
            }

            [ShowInInspector]
            public bool m_isTriggered;
            public bool isTriggered => m_isTriggered;

            ISaveData ISaveData.ProduceCopy() => new SaveData(m_isTriggered);
        }

        [SerializeField, OnValueChanged("OnValueChange")]
        private bool m_oneTimeOnly;
        [SerializeField]
        private bool m_waitForPlayerToBeGrounded;   //Using Coroutines to check for Player State before execution when this is TRUE. Have to Directly Access Player State for this to work.
        [SerializeField]
        private bool m_playerHitboxOnly;
        [SerializeField, TabGroup("Enter")]
        private UnityEvent m_enterEvents;
        [SerializeField, HideIf("m_oneTimeOnly"), TabGroup("Exit")]
        private UnityEvent m_exitEvents;
        [SerializeField, TabGroup("Delayed Exit")]
        private UnityEvent m_DelayedExit;
        [SerializeField, TabGroup("Delayed Exit")]
        private float m_DelaySeconds = 1;

        [SerializeField]
        private Collider2D m_collider;
        private bool m_wasTriggered;
        private IGroundednessState m_playerGroundedness;
        private Coroutine m_enterEventRoutine;
        private Coroutine m_exitEventRoutine;
        private Coroutine m_DelayedExitRoutine;

        public ISaveData Save()
        {
            return new SaveData(m_wasTriggered);
        }

        public void Load(ISaveData data)
        {
            m_wasTriggered = ((SaveData)data).isTriggered;
        }
        public void Initialize()
        {
            m_wasTriggered = false;
            if (m_waitForPlayerToBeGrounded)
            {
                m_playerGroundedness = GameplaySystem.playerManager.player.character.GetComponentInChildren<IGroundednessState>();
            }
            TryGetComponent(out m_collider);
        }

        private IEnumerator ExecuteEnterWhenPlayerIsGrounded()
        {
            while (m_playerGroundedness.isGrounded == false)
            {
                yield return null;
            }

            TriggerEnterEvent();
            m_enterEventRoutine = null;
        }

        private IEnumerator ExecuteExitWhenPlayerIsGrounded()
        {
            while (m_playerGroundedness.isGrounded == false)
            {
                yield return null;
            }

            TriggerExitEvent();
            m_exitEventRoutine = null;
        }

        private void TriggerEnterEvent()
        {
            m_enterEvents?.Invoke();
            if (m_oneTimeOnly)
            {
                m_wasTriggered = true;
            }
            if (m_DelayedExitRoutine != null)
            {
                StopCoroutine(m_DelayedExitRoutine);
                m_DelayedExitRoutine = null;
            }
        }

        private void TriggerExitEvent()
        {
            m_exitEvents?.Invoke();
            if(m_DelayedExitRoutine!=null)
            {
                return;
            }
            m_DelayedExitRoutine = StartCoroutine(DelayedExit());
        }

        private IEnumerator DelayedExit()
        {
            yield return new WaitForSeconds(m_DelaySeconds);
            m_DelayedExit?.Invoke();
        }

        private void Start()
        {
            Initialize();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_playerHitboxOnly && collision.tag != "Hitbox")
                return;

            var playerObject = collision.gameObject.GetComponentInParent<PlayerControlledObject>();
            if (playerObject != null && collision.tag != "Sensor" && playerObject.owner == (IPlayer)GameplaySystem.playerManager.player)
            {
                if ((m_oneTimeOnly && !m_wasTriggered) || !m_oneTimeOnly)
                {
                    if (m_waitForPlayerToBeGrounded)
                    {
                        if (m_exitEventRoutine != null)
                        {
                            StopCoroutine(m_exitEventRoutine);
                            m_exitEventRoutine = null;
                        }

                        if (m_enterEventRoutine == null)
                        {
                            m_enterEventRoutine = StartCoroutine(ExecuteEnterWhenPlayerIsGrounded());
                        }
                    }
                    else
                    {
                        TriggerEnterEvent();
                    }
                }
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (m_playerHitboxOnly && collision.tag != "Hitbox")
                return;

            if(m_collider!=null)
            {
                if(m_collider.bounds.Contains(collision.transform.position))
                {
                    return;
                }
            }
            if (!transform.GetComponentInParent<HiddenAreaCover>())
            {
                if (!m_oneTimeOnly)
                {
                    TriggerExitEvent();
                }
                //else
                //{
                //    TriggerExitEvent();
                //}

                if (!m_waitForPlayerToBeGrounded)
                {
                    if (!m_oneTimeOnly)
                        TriggerExitEvent();
                }
                else
                {
                    if (m_enterEventRoutine != null)
                    {
                        StopCoroutine(m_enterEventRoutine);
                        m_enterEventRoutine = null;
                    }

                    if (m_exitEventRoutine == null)
                    {
                        m_exitEventRoutine = StartCoroutine(ExecuteExitWhenPlayerIsGrounded());
                    }
                }
            }

            if (!m_oneTimeOnly)
            {
                var playerObject = collision.gameObject.GetComponentInParent<PlayerControlledObject>();
                if (playerObject != null && collision.tag == "Hitbox" && playerObject.owner == (IPlayer)GameplaySystem.playerManager.player)
                {
                    Collider2D collider = GetComponent<Collider2D>();
                    Transform transformToCheck = playerObject.transform;

                    if (collider.OverlapPoint(transformToCheck.position))
                    {

                    }
                    else
                    {
                        TriggerExitEvent();
                    }
                }
            }
        }


        private void OnValidate()
        {
            DChildUtility.ValidateSensor(gameObject);
        }

#if UNITY_EDITOR
        private void OnValueChange()
        {
            if (m_oneTimeOnly)
            {
                m_exitEvents.RemoveAllListeners();
            }
        }

        [Button, HideInEditorMode]
        private void OnEnter()
        {
            m_enterEvents?.Invoke();
        }

        [Button, HideIf("m_oneTimeOnly"), HideInEditorMode]
        private void OnExit()
        {
            m_exitEvents?.Invoke();
        }
#endif
    }
}