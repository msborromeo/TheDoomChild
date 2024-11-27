using Doozy.Runtime.UIManager.Animators;
using Doozy.Runtime.UIManager.Components;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class AttackingGroupSelectableOptionUI : AttackingGroupOptionUI
    {

        [SerializeField]
        private List<Sprite> m_NullAssets;
        [SerializeField]
        private List<Sprite> m_GroupAssets;

        [SerializeField]
        private List<Image> m_targetAssets;
        [SerializeField]
        private Image m_targetCommandIcon;
        [SerializeField]
        private TextMeshProUGUI m_targetPartyName;
        [SerializeField]
        private TextMeshProUGUI m_targetPowerLabel;
        [SerializeField]
        private TextMeshProUGUI m_targetPowerValue;

        [SerializeField]
        private UIButton m_armyRow;
        [SerializeField]
        private Image m_highlightGlow;


        private int m_selectionIndex;

        public int selectionIndex => m_selectionIndex;

        public void SetSelectionIndex(int index)
        {
            m_selectionIndex = index;
        }

        public override void Display(IAttackingGroup group)
        {
            if (group == null)
            {
                NullifyArmyGroupUI();
                return;
            }

            if (m_targetCommandIcon.enabled == false)
            {
                RestoreArmyGroupUI();
            }
            base.Display(group);
        }
        private void RestoreArmyGroupUI()
        {
            for (int i = 0; i < m_GroupAssets.Count; i++)
            {
                RestoreGroupElement(i, m_targetAssets[i]);
            }
            m_armyRow.interactable = true;
            m_highlightGlow.enabled = true;
            m_targetCommandIcon.enabled = true;
            m_targetPartyName.enabled = true;
            m_targetPowerLabel.text = "<color=#EA9E03>ATTACK POWER</color>";
            m_targetPowerValue.enabled = true;
        }


        private void NullifyArmyGroupUI()
        {
            m_armyRow.interactable = false;
            m_highlightGlow.enabled = false;
            m_targetCommandIcon.enabled = false;
            m_targetPartyName.enabled = false;
            m_targetPowerLabel.text = $"<color=#82A4C7>ATTACK POWER</color>";
            m_targetPowerValue.enabled = false;
            

            for (int i = 0; i < m_NullAssets.Count; i++)
            {
                NullifyGroupElement(i, m_targetAssets[i]);
            }
        }

        private void RestoreGroupElement(int index, Image target)
        {
            target.sprite = m_GroupAssets[index];

        }

        private void NullifyGroupElement(int index, Image target)
        {
            target.sprite = m_NullAssets[index];
        }
    }
}