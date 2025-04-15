using DChild.Gameplay.Characters.Players;
using Holysoft.Event;
using UnityEngine;

namespace DChild.Gameplay.Environment
{
    public class PlayerCollisionSensor : MonoBehaviour, IPlayerWallStickPlatformReaction
    {
        [SerializeField]
        private float m_hangDuration = 1.2f;

        private float m_playerinDuration;

        private bool m_playerAttached;
        private Transform m_playerPos;
        public event EventAction<EventActionArgs> CollisionDetected;

        public void ReactToPlayerWallStick(Character player)
        {
            CollisionDetected?.Invoke(this, EventActionArgs.Empty);
        }

        public void ReactToPlayerWallUnstick(Character player)
        {
            m_playerinDuration = 0;
            m_playerAttached = false;
            m_playerPos = null;
        }

        
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (GameplaySystem.playerManager.IsPartOfPlayer(collision.gameObject))
            {
                CollisionDetected?.Invoke(this, EventActionArgs.Empty);
            }
        }
        
        /*
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (GameplaySystem.playerManager.IsPartOfPlayer(collision.gameObject))
            {
                m_playerAttached = true;
                m_playerPos = collision.transform;
            }
        }

        private void FixedUpdate()
        {
            if(m_playerAttached&&m_playerinDuration<m_hangDuration+1)
            {
                m_playerinDuration += Time.deltaTime;
                if (m_playerinDuration >= m_hangDuration)
                {
                    CollisionDetected?.Invoke(this, EventActionArgs.Empty);
                    m_playerinDuration = 0;
                }

                if(Vector2.Distance(transform.position,m_playerPos.position)<0.4)
                {
                    m_playerinDuration = 0;
                    m_playerAttached = false;
                    m_playerPos = null;
                }
            }
        }

        private void OnDisable()
        {
            m_playerinDuration = 0;
            m_playerAttached = false;
            m_playerPos = null;
        }
        /*
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (GameplaySystem.playerManager.IsPartOfPlayer(collision.gameObject))
            {
                CollisionDetected?.Invoke(this, EventActionArgs.Empty);
            }
        }*/
    }
}
