using DChild.Gameplay.ArmyBattle.SpecialSkills;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class ArmyBattleSpecialSkillSelection : MonoBehaviour
    {
        [SerializeField]
        private ArmyDamageTypeOptionUI m_damageTypeIcon;

        [SerializeField]
        protected SpecialSkillGroupOptionUI m_ui;

        [SerializeField]
        private MoreGroupsClassLabel m_frontLabel;

        private List<ISpecialSkillGroup> m_specialSelection;

        private int m_selectionIndex;

        protected int selectionIndex
        {
            get => m_selectionIndex;
            set
            {
                m_selectionIndex = (int)Mathf.Repeat(value, m_specialSelection.Count);
                UpdateUI();
            }
        }

        public void SetSpecialSelectionList(List<ISpecialSkillGroup> selection)
        {
            m_specialSelection = selection;
            selectionIndex = 0;
        }
        public void Prev()
        {
            selectionIndex -= 1;
        }

        public void Next()
        {
            selectionIndex += 1;
        }

        public ISpecialSkillGroup GetSelectedSpecialSkillGroup() => m_specialSelection[m_selectionIndex];

        public void SetSelection(int index) => selectionIndex = index;

        public void SetSelectionIcon(DamageType type) => m_damageTypeIcon.SetType(type);

        public void SetPanelLabel(DamageType type) => m_frontLabel.SetPanelLabel(type);

        private void UpdateUI()
        {
            m_ui.Display(m_specialSelection[selectionIndex]);
        }
    }
}