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
        private ParticleSystem m_physicalSlashStreak;
        [SerializeField, BoxGroup("Elemental Streaks")]
        private ParticleSystem m_blazeSlashStreak;
        [SerializeField, BoxGroup("Elemental Streaks")]
        private ParticleSystem m_frostSlashStreak;
        [SerializeField, BoxGroup("Elemental Streaks")]
        private ParticleSystem m_zapSlashStreak;
        [SerializeField, BoxGroup("Elemental Streaks")]
        private ParticleSystem m_shadowSlashStreak;



        // Start is called before the first frame update
        void Start()
        {
            m_attacker.CharacterTargetDamaged += OnCharacterDamaged;
        }

        private void OnDestroy()
        {
            m_attacker.CharacterTargetDamaged -= OnCharacterDamaged;
        }

        private void OnCharacterDamaged(Vector3 vector)
        {
            m_damageType = m_elementalSwordController.currentDamageType;

            switch (m_damageType)
            {
                case DamageType.Physical:
                    m_physicalSlashStreak.transform.position = vector;
                    m_physicalSlashStreak.Play();
                    break;
                case DamageType.Fire:
                    m_blazeSlashStreak.transform.position = vector;
                    m_blazeSlashStreak.Play();
                    break;
                case DamageType.Ice:
                    m_frostSlashStreak.transform.position = vector;
                    m_frostSlashStreak.Play();
                    break;
                case DamageType.Lightning:
                    m_zapSlashStreak.transform.position = vector;
                    m_zapSlashStreak.Play();
                    break;

            }
        }
    }
}

