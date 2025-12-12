using DChild.Gameplay.Pooling;
using DChild.Gameplay.Systems;
using Holysoft.Event;
using DChild.Gameplay.Characters.Players;
using UnityEngine;
using System.Collections;
using Spine.Unity.Examples;

namespace DChild.Gameplay.Characters.Players.Behaviour
{
    public class LootPicker : MonoBehaviour
    {
        [SerializeField]
        private Animator m_animator;
        [SerializeField]
        private SkeletonGhost m_auraGhost;
        [SerializeField]
        private float m_auraDuration;

        private IPlayer m_owner;

        public event EventAction<EventActionArgs> OnLootPickup;
        public event EventAction<EventActionArgs> OnLootPickupEnd;

        public void Glow()
        {
            //m_animator.SetTrigger("Glow");
            StartCoroutine(AuraRoutine());
            OnLootPickupEnd?.Invoke(this, EventActionArgs.Empty);
        }

        private void Start()
        {
            m_owner = GetComponentInParent<PlayerControlledObject>().owner;
        }

        private IEnumerator AuraRoutine()
        {
            m_auraGhost.enabled = true;
            yield return new WaitForSeconds(m_auraDuration);
            m_auraGhost.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var loot = collision.GetComponentInParent<Loot>();
            if (loot)
            {
                loot.PickUp(m_owner);
                OnLootPickup?.Invoke(this,EventActionArgs.Empty);
            }
        }
    }
}