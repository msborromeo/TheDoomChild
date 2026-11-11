/***************************************************
 * 
 * Attackers should look for this in order to damage an Object
 * 
 ***************************************************/
using DChild.Gameplay.VFX;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.Combat
{
    [AddComponentMenu("DChild/Gameplay/Combat/Hitbox")]
    public class Hitbox : MonoBehaviour
    {
        public const string TAG = "Hitbox";

        private IDamageable m_damageable;
        [SerializeField, DisableInPlayMode, HideInEditorMode]
        private Collider2D[] m_collider2Ds;
        [SerializeField]
        private FXSpawnConfigurationInfo m_damageFXInfo;
        [SerializeField]
        private Invulnerability m_invulnerabilityLevel;
        [SerializeField, HideIf("@m_invulnerabilityLevel == Invulnerability.MAX"), Range(0, 0.99f)]
        private float m_damageReduction;
        [SerializeField]
        private bool m_canBlockDamage;

        public IDamageable damageable => m_damageable;
        public FXSpawnConfigurationInfo damageFXInfo => m_damageFXInfo;
        public BodyDefense defense => new BodyDefense(m_invulnerabilityLevel, m_damageReduction);
        public Invulnerability invulnerabilityLevel => m_invulnerabilityLevel;

        private readonly Dictionary<object, Invulnerability> m_invulnerabilityRequests = new Dictionary<object, Invulnerability>();
        private Invulnerability m_baseInvulnerability;
        private bool m_invulnerabilityInitialized;
        public bool canBlockDamage => m_canBlockDamage;

        public void Enable()
        {
            for (int i = 0; i < m_collider2Ds.Length; i++)
            {
                m_collider2Ds[i].enabled = true;
            }
        }

        [Button]
        public void Disable()
        {
            for (int i = 0; i < m_collider2Ds.Length; i++)
            {
                m_collider2Ds[i].enabled = false;
            }
        }

        public virtual bool CanBeDamageBy(params Collider2D[] colliders) => true;

        //public void SetInvulnerability(Invulnerability value) => m_invulnerabilityLevel = value;
        private void InitializeInvulnerability()
        {
            if (m_invulnerabilityInitialized)
                return;

            // Preserve the initial value configured on this hitbox.
            m_baseInvulnerability = m_invulnerabilityLevel;
            m_invulnerabilityInitialized = true;
        }

        public void SetInvulnerability(Invulnerability value)
        {
            InitializeInvulnerability();

            // Older callers change the base value only.
            m_baseInvulnerability = value;
            RefreshInvulnerability();
        }

        public void RequestInvulnerability(object owner, Invulnerability level)
        {
            if (owner == null)
                throw new System.ArgumentNullException(nameof(owner));

            InitializeInvulnerability();

            if (level == Invulnerability.None)
            {
                ReleaseInvulnerability(owner);
                return;
            }

            // Repeated requests from the same ability update its existing entry.
            m_invulnerabilityRequests[owner] = level;
            RefreshInvulnerability();
        }

        public void ReleaseInvulnerability(object owner)
        {
            if (owner == null)
                return;

            // Releasing an already-removed request does nothing.
            if (m_invulnerabilityRequests.Remove(owner))
            {
                RefreshInvulnerability();
            }
        }

        private void RefreshInvulnerability()
        {
            Invulnerability effectiveLevel = m_baseInvulnerability;

            foreach (Invulnerability level in m_invulnerabilityRequests.Values)
            {
                if (level > effectiveLevel)
                {
                    effectiveLevel = level;
                }
            }

            m_invulnerabilityLevel = effectiveLevel;
        }

        public void SetCanBlockDamageState(bool canBlockDamage) => m_canBlockDamage = canBlockDamage;

        private void Awake()
        {
            m_damageable = GetComponentInParent<IDamageable>();
        }

        private void OnValidate()
        {
            var compositeCollider = GetComponent<CompositeCollider2D>();
            if (compositeCollider)
            {
                var colliders = new List<Collider2D>(GetComponentsInChildren<Collider2D>());
                colliders.Remove(compositeCollider);
                m_collider2Ds = colliders.ToArray();
            }
            else
            {
                m_collider2Ds = GetComponentsInChildren<Collider2D>();
            }

            foreach (var collider2D in m_collider2Ds)
            {
                if(collider2D.tag != Hitbox.TAG)
                {
                    collider2D.tag = Hitbox.TAG;
                }
            }
        }
    }
}
