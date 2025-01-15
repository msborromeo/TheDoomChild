using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public struct DisableUnitType : ISpecialSkillModule, ISpecialSkillImplementor
    {
        [SerializeField]
        private DamageType m_damageType;
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            var isTargetPlayer = ArmyBattleSystem.GetPlayer() == target;
            var configuration = ArmyBattleSystem.turnConfiguration;
            if (isTargetPlayer)
            {
                configuration.playerConfiguration = SetUnitTypeAvailability(configuration.playerConfiguration, false);
            }
            else
            {
                configuration.enemyConfiguration = SetUnitTypeAvailability(configuration.enemyConfiguration, false);
            }

            ArmyBattleSystem.turnConfiguration = configuration;
            Debug.Log("Disable!");
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
            target.controlledArmy.ResetGroupAvailability();
            var isTargetPlayer = ArmyBattleSystem.GetPlayer() == target;
            var configuration = ArmyBattleSystem.turnConfiguration;

            if (isTargetPlayer)
            {
                configuration.playerConfiguration = SetUnitTypeAvailability(configuration.playerConfiguration, true);
            }
            else
            {
                configuration.enemyConfiguration = SetUnitTypeAvailability(configuration.enemyConfiguration, true);
            }

            ArmyBattleSystem.turnConfiguration = configuration;

            Debug.Log("Enable");
        }

        private ArmyBattleTurnHandle.ParticipantConfiguration SetUnitTypeAvailability(ArmyBattleTurnHandle.ParticipantConfiguration participantConfiguration, bool allowUse)
        {
            switch (m_damageType)
            {
                case DamageType.Melee:
                    participantConfiguration.canUseMelee = allowUse;
                    break;
                case DamageType.Range:
                    participantConfiguration.canUseRange = allowUse;
                    break;
                case DamageType.Magic:
                    participantConfiguration.canUseMagic = allowUse;
                    break;
            }

            return participantConfiguration;
        }
    }
}

