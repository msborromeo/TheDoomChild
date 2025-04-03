using DChild.Gameplay.Characters;
using DChild.Gameplay.Combat;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Environment
{
    public class CrusherColliderDamage : MonoBehaviour
    {
        private enum CrusherPosition
        {
            Top, 
            Bottom
        }

        [SerializeField]
        private CrusherPosition m_crusherPosition;
        [SerializeField]
        private float m_rayOriginOffset;
        [SerializeField]
        private MovingPlatform m_movingPlatform;

        private Collider2D m_collider;
        private IDamageDealer m_damageDealer;

        private List<Damageable> m_damageable;

        protected virtual void Awake()
        {
            m_collider = GetComponent<Collider2D>();
            m_damageable = new List<Damageable>();
            m_damageDealer = GetComponentInParent<IDamageDealer>();
        }

        protected void InitializeTargetInfo(Cache<TargetInfo> cache, Damageable damageable, Collider2D damageableHitCollider)
        {
            if (damageable.CompareTag(Character.objectTag))
            {
                var character = damageable.GetComponent<Character>();
                cache.Value.Initialize(damageable, false,new BodyDefense(), damageableHitCollider, character, character.GetComponentInChildren<IFlinch>());
            }
            else
            {
                cache.Value.Initialize(damageable, false, new BodyDefense(), damageableHitCollider, damageable.GetComponent<BreakableObject>());
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.enabled)
            {
                var colliderGameObject = collision.gameObject;
                if (colliderGameObject.CompareTag("DamageCollider") || colliderGameObject.CompareTag("Sensor"))
                    return;

                if (colliderGameObject.TryGetComponentInParent(out Damageable damageable) && colliderGameObject.TryGetComponentInParent(out Character character))
                {
                    if (m_damageable.Contains(damageable) == false)
                    {
                        Raycaster.SetLayerMask(DChildUtility.GetEnvironmentMask());
                        var collisionPoint = collision.GetContact(0).point;
                        var hits = Raycaster.Cast(m_crusherPosition == CrusherPosition.Bottom ? new Vector2(collisionPoint.x, collisionPoint.y - m_rayOriginOffset) : new Vector2(collisionPoint.x, collisionPoint.y + m_rayOriginOffset), 
                            m_crusherPosition == CrusherPosition.Bottom ? -transform.up : transform.up, 
                            character.height, 
                            true, 
                            out int hitCount);
                        if (hitCount > 0)
                        {
                            if (m_movingPlatform.isStopped) //this check may cause problems with big enemies that would prevent the elevator from actually reaching the destination
                            {
                                m_damageable.Add(damageable);
                                Crush(damageable, collision.collider);
                            }
                            
                        }
                    }
                }
            }
        }

        private void Crush(Damageable damageable, Collider2D damageableHitCollider)
        {
            using (Cache<TargetInfo> cacheTargetInfo = Cache<TargetInfo>.Claim())
            {
                InitializeTargetInfo(cacheTargetInfo, damageable, damageableHitCollider);
                m_damageDealer?.Damage(cacheTargetInfo.Value, m_collider);
                cacheTargetInfo?.Release();
            }
        }

        private void Reset()
        {
            m_damageable.Clear();
        }
    }

}