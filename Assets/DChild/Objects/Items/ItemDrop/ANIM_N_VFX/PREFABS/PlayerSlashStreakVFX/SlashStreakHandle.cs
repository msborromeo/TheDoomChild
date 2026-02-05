using DChild.Gameplay.Pooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Combat
{
    public class SlashStreakHandle : MonoBehaviour
    {
        [SerializeField]
        private Attacker m_attacker;
        [SerializeField]
        private ElementalSwordController m_elementalSwordController;
        [SerializeField]
        private DamageType m_damageType;

        [SerializeField, BoxGroup("Elemental Streaks")]
        private ParticleFX m_physicalSlashStreak;
        [SerializeField, BoxGroup("Elemental Streaks")]
        private ParticleFX m_blazeSlashStreak;
        [SerializeField, BoxGroup("Elemental Streaks")]
        private ParticleFX m_frostSlashStreak;
        [SerializeField, BoxGroup("Elemental Streaks")]
        private ParticleFX m_zapSlashStreak;
        [SerializeField, BoxGroup("Elemental Streaks")]
        private ParticleFX m_shadowSlashStreak;



        // Start is called before the first frame update
        void Start()
        {
            m_attacker.CharacterTargetDamaged += OnCharacterDamaged;
        }

        private void OnDisable()
        {
            m_attacker.CharacterTargetDamaged -= OnCharacterDamaged;
        }

        private void OnCharacterDamaged(Vector3 vector)
        {
            m_damageType = m_elementalSwordController.currentDamageType;

            switch (m_damageType)
            {
                case DamageType.Physical:
                    var physicalStreak = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_physicalSlashStreak.gameObject, vector, Quaternion.identity);
                    break;
                case DamageType.Fire:
                    var blazeStreak = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_blazeSlashStreak.gameObject, vector, Quaternion.identity);
                    break;
                case DamageType.Ice:
                    var frostStreak = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_frostSlashStreak.gameObject, vector, Quaternion.identity);
                    break;
                case DamageType.Lightning:
                    var zapStreak = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_zapSlashStreak.gameObject, vector, Quaternion.identity);
                    break;
            }
        }
    }
}

