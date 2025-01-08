using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class ArmyBattlePlayerOption : MonoBehaviour
    {
        [SerializeField]
        private PlayerArmyController m_player;
        [SerializeField]
        private ArmyDamageOptionSelection m_damageSelection;
        [SerializeField]
        private ArmyBattleAttackGroupSelection m_groupSelection;
        [SerializeField]
        private ArmyBattleSpecialSkillSelection m_specialSelection;

        [SerializeField]
        private ArmyGroupIndexHandle m_groupIndex;


        public void Initialize(PlayerArmyController player)
        {
            m_player = player;
            m_damageSelection.Initialize(player.controlledArmy);
        }

        public void UpdateOptions()
        {
            m_damageSelection.UpdateSelectionAvailability();
        }

        public void SetAttackGroupSelection(ArmyDamageTypeOptionUI option)
        {
            var damageType = option.damageType;
            var playerGroups = m_player.controlledArmy.GetAvailableGroups(damageType);

            m_groupSelection.SetSelectionIcon(damageType);
            m_groupSelection.SetPanelLabel(damageType);
            m_groupSelection.SetSelectionList(playerGroups);
            m_groupIndex.SetAvailableGroups(playerGroups);
            SelectCurrentAttackingGroup();
        }

        public void SetSpecialSkillSelection()
        {
            var playerSpecialGroups = m_player.controlledArmy.GetAvailableSkills();
            m_specialSelection.SetSpecialSelectionList(playerSpecialGroups);
            m_specialSelection.GetSelectedSpecialAttackGroup();
            m_groupIndex.SetAvailableSpecialGroups(playerSpecialGroups);
        }

        public void SelectCurrentAttackingGroup()
        {
            m_player.UseThisTurn(m_groupSelection.GetSelectedAttackGroup());
            Debug.Log($"Selecting To Attack With {m_groupSelection.GetSelectedAttackGroup().GetCharacterGroup().name}");
        }


    }
}