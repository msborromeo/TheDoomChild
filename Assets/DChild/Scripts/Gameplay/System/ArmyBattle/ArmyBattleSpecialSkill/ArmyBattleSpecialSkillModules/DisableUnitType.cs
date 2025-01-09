using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class DisableUnitType : ISpecialSkillModule, ISpecialSkillImplementor
    {
        [SerializeField]
        private DamageType m_damageType;
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            var isTargetPlayer = ArmyBattleSystem.GetPlayer() == target;
            var configuration = ArmyBattleSystem.turnConfiguration;
            configuration = SetArmyDamageType(isTargetPlayer, configuration, false);

            ArmyBattleSystem.turnConfiguration = configuration;
            Debug.Log("Disable!");
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
            target.controlledArmy.ResetGroupAvailability();
            var isTargetPlayer = ArmyBattleSystem.GetPlayer() == target;
            var configuration = ArmyBattleSystem.turnConfiguration;
            configuration = SetArmyDamageType(isTargetPlayer, configuration, true);

            ArmyBattleSystem.turnConfiguration = configuration;

            Debug.Log("Enable");
        }

        private ArmyBattleTurnHandle.TurnConfiguration SetArmyDamageType(bool isTargetPlayer, ArmyBattleTurnHandle.TurnConfiguration configuration, bool allowUse)
        {
            ArmyBattleTurnHandle.TurnConfiguration setConfiguration;
            if (isTargetPlayer)
            {
                switch (m_damageType)
                {
                    case DamageType.Melee:
                        configuration.playerCanUseMelee = allowUse;
                        break;
                    case DamageType.Range:
                        configuration.playerCanUseRange = allowUse;
                        break;
                    case DamageType.Magic:
                        configuration.playerCanUseMagic = allowUse;
                        break;
                }
                setConfiguration = configuration;
            }
            else
            {
                switch (m_damageType)
                {
                    case DamageType.Melee:
                        configuration.enemyCanUseMelee = allowUse;
                        break;
                    case DamageType.Range:
                        configuration.enemyCanUseRange = allowUse;
                        break;
                    case DamageType.Magic:
                        configuration.enemyCanUseMagic = allowUse;
                        break;
                }
                setConfiguration = configuration;
            }

            return setConfiguration;
        }
    }
}

