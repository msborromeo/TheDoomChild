using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Systems;
using Holysoft.Event;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Essence
{
    public abstract class EssenceLoot : Loot
    {
        [SerializeField]
        private Collider2D m_pickupCollider;
        [SerializeField, MinValue(0.1f)]
        private float m_pickUpVelocity;
        [SerializeField, MinValue(0.1f)]
        private float m_fadeAfterRestingDuration;

        protected bool m_isFading;
        private float m_gravity;
        private bool m_hasBeenPickUp;
        private float m_timer;
        private bool m_hasReachedCenterMass; // testing , checking if the object has reach the player's center mass
        protected abstract void OnApplyPickup(IPlayer player);

        public override void PickUp(IPlayer player)
        {
            base.PickUp(player);
            m_hasBeenPickUp = true;
            m_isFading = false;
            m_pickupCollider.isTrigger = true;
        }


        public override void SpawnAt(Vector2 position, Quaternion rotation)
        {
            base.SpawnAt(position, rotation);
            m_isFading = false;
            m_rigidbody.gravityScale = m_gravity;
            m_hasBeenPickUp = false;
            m_timer = 0;
            m_pickupCollider.enabled = false;
            m_pickupCollider.isTrigger = false;
        }

        protected override void ApplyPickUp()
        {
            base.ApplyPickUp();
            OnApplyPickup(m_pickedBy);
            CallPoolRequest();
        }


        protected override void OnPopDurationEnd(object sender, EventActionArgs eventArgs)
        {
            base.OnPopDurationEnd(sender, eventArgs);
            m_pickupCollider.enabled = true;
            m_pickupCollider.isTrigger = true;
            if (m_isPopping == false && m_hasBeenPickUp)
            {
                m_animator?.SetBool("PickedUp", true);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            m_gravity = m_rigidbody.gravityScale;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (m_isPopping)
                return;

            if (m_isFading)
            {
                m_timer -= GameplaySystem.time.fixedDeltaTime;
                if (m_timer <= 0)
                {
                    CallPoolRequest();

                    return;
                }
            }

            if (m_hasBeenPickUp)
            {
                Vector2 toPLayer;
                if (m_hasReachedCenterMass)
                {
                    // because the body reference as of now does not move with the model,
                    // i am using the body reference feet to make sure the player gets the loot
                    toPLayer = ((Vector2)m_pickedBy.character.GetBodyPart(BodyReference.BodyPart.Feet).position - m_rigidbody.position).normalized;
                }
                else
                {
                    toPLayer = (m_pickedBy.damageableModule.position - m_rigidbody.position).normalized;
                }

                //Added to check if the loot has already reached the player's center mass
                if (Vector2.Distance(transform.position, m_pickedBy.damageableModule.position) < 0.5f && !m_hasReachedCenterMass)
                {
                    m_hasReachedCenterMass = true;
                }

                m_rigidbody.velocity = toPLayer * m_pickUpVelocity;
                
            }


        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (m_isFading == false)
            {
                m_isFading = true;
                m_timer = m_fadeAfterRestingDuration;
            }

        }
    }
}