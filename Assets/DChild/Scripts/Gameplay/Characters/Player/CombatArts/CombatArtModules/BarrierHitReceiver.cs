using System.Collections.Generic;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Projectiles;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.BattleAbilityModule
{
    /// <summary>
    /// Visual feedback for attacks touching a barrier, with optional projectile removal.
    /// Does not apply damage or protect against damage applied without a collider.
    /// Put this on a dedicated trigger object without a Hitbox component.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("DChild/Gameplay/Combat/Barrier Hit Receiver")]
    public class BarrierHitReceiver : MonoBehaviour
    {
        [SerializeField, Tooltip("Layers containing enemy attack and environmental damage colliders. Select these explicitly.")]
        private LayerMask m_attackLayers;
        [SerializeField, Tooltip("Impact prefab. Prefabs with a root FX component use the FX pool; ordinary prefabs are instantiated directly.")]
        private GameObject m_impactFX;
        [SerializeField, Min(0.1f), Tooltip("Cleanup delay for ordinary prefabs without a root FX component. Allow enough time for all particles and animations to finish.")]
        private float m_impactLifetime = 3f;
        [SerializeField, Min(0f), Tooltip("Minimum seconds between visual impacts from the same damage dealer.")]
        private float m_hitCooldown = 0.1f;
        [SerializeField, Tooltip("Return incoming Projectile components to their pool on contact, without triggering their damaging impact effects.")]
        private bool m_removeProjectiles = true;
        [SerializeField, Tooltip("Player root, used to ignore the player's own attacks. Defaults to the nearest Character.")]
        private Transform m_owner;

        private Collider2D m_collider;
        private FXSpawnHandle<FX> m_spawnHandle;
        private readonly Dictionary<MonoBehaviour, float> m_recentHits = new Dictionary<MonoBehaviour, float>();
        private readonly List<MonoBehaviour> m_expiredHits = new List<MonoBehaviour>();

        private void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
            gameObject.tag = "Hitbox";
        }

        private void Awake()
        {
            m_collider = GetComponent<Collider2D>();
            m_collider.isTrigger = true;
            // ColliderDamage and AttackProjectile explicitly ignore Sensor targets.
            // This also prevents parent Hitbox lookup from reaching the player.
            gameObject.tag = "Hitbox";
            if (m_owner == null)
            {
                var character = GetComponentInParent<Character>();
                if (character != null)
                    m_owner = character.transform;
            }

            if (GetComponent<Hitbox>() != null)
            {
                Debug.LogError("BarrierHitReceiver needs a dedicated trigger without a Hitbox component.", this);
                m_collider.enabled = false;
                enabled = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TryShowImpact(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            // Continuous hazards need feedback while overlapping. Ordinary attacks
            // still produce feedback only on entry.
            var dps = other.GetComponentInParent<DPSColliderDamage>();
            if (dps != null && dps.isActiveAndEnabled)
                TryShowImpact(other);
        }

        private void TryShowImpact(Collider2D other)
        {
            if (!isActiveAndEnabled || !other.gameObject.activeInHierarchy ||
                (m_attackLayers.value & (1 << other.gameObject.layer)) == 0)
                return;

            if (m_owner != null && other.transform.IsChildOf(m_owner))
                return;

            // Avoid showing impacts for enemy bodies and detection sensors.
            if (!other.CompareTag("DamageCollider") && other.GetComponentInParent<ColliderDamage>() == null)
                return;

            var dealer = other.GetComponentInParent<IDamageDealer>() as MonoBehaviour;
            if (dealer == null)
                return;

            var dps = other.GetComponentInParent<DPSColliderDamage>();
            if (dps != null && !dps.isActiveAndEnabled)
                return;

            float now = Time.time;
            bool onCooldown = m_recentHits.TryGetValue(dealer, out float nextHit) && now < nextHit;
            if (!onCooldown && m_impactFX != null)
            {
                var distance = m_collider.Distance(other);
                Vector2 point = distance.isValid ? distance.pointA : m_collider.ClosestPoint(other.bounds.center);
                var direction = GameplayUtility.GetHorizontalDirection(other.bounds.center, point);
                float interval = dps != null ? Mathf.Max(0.02f, dps.damageInterval, m_hitCooldown) : m_hitCooldown;
                m_recentHits[dealer] = now + interval;
                SpawnImpact(transform.position, direction);
            }

            // Removal must still happen when visual feedback is on cooldown or unassigned.
            if (m_removeProjectiles)
            {
                var projectile = other.GetComponentInParent<Projectile>();
                if (projectile != null && projectile.gameObject.activeInHierarchy)
                {
                    projectile.CallPoolRequest();
                    // Registered projectiles are moved under inactive pool storage.
                    // Scene-placed/unregistered projectiles need explicit cleanup instead.
                    if (projectile != null && projectile.gameObject.activeInHierarchy)
                    {
                        projectile.gameObject.SetActive(false);
                        projectile.DestroyInstance();
                    }
                }
            }
        }

        private void SpawnImpact(Vector2 point, HorizontalDirection direction)
        {
            if (m_impactFX.GetComponent<FX>() != null)
            {
                m_spawnHandle.InstantiateFX(m_impactFX, point, direction, transform);
                return;
            }

            // Ordinary particle/animation prefabs do not implement the pool's FX contract.
            // Keep them independent of the barrier so disabling it won't cut the effect short.
            var instance = Instantiate(m_impactFX, point, m_impactFX.transform.rotation, transform);
            instance.SetActive(true);
            foreach (var particles in instance.GetComponentsInChildren<ParticleSystem>())
            {
                particles.Play(false);
            }
            Destroy(instance, Mathf.Max(0.1f, m_impactLifetime));
        }

        private void LateUpdate()
        {
            // Release references to projectiles after the short visual cooldown.
            m_expiredHits.Clear();
            foreach (var hit in m_recentHits)
            {
                if (hit.Key == null || Time.time >= hit.Value)
                    m_expiredHits.Add(hit.Key);
            }
            foreach (var dealer in m_expiredHits)
                m_recentHits.Remove(dealer);
        }

        private void OnDisable()
        {
            m_recentHits.Clear();
            m_expiredHits.Clear();
        }
    }
}
