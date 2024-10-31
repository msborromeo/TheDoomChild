using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class AttackingGroupSelectableOptionUI : AttackingGroupOptionUI
    {
        /*[SerializeField]
        private Image m_nullBackPlate;
        [SerializeField]
        private Image m_nullFrontPanel;
        [SerializeField]
        private Image m_nullPartyName;
        [SerializeField]
        private Image m_nullSlotFrame;
        [SerializeField]
        private Image m_nullPowerHolder;
        [SerializeField]
        private Image m_nullSkillHolder;*/

        [SerializeField]
        private List<Sprite> m_NullAssets;

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


        public override void Display(IAttackingGroup group)
        {
            if (group == null)
            {
                NullifyArmyGroupUI();
                return;
            }

            base.Display(group);
        }

        private void NullifyArmyGroupUI()
        {
            m_targetCommandIcon.enabled = false;
            m_targetPartyName.enabled = false;
            m_targetPowerLabel.text = $"<color=#82A4C7>{m_targetPowerLabel.text}</color>";
            m_targetPowerValue.enabled = false;

            for (int i = 0; i < m_NullAssets.Count; i++)
            {
                NullifyGroupElement(i, m_targetAssets[i]);
            }

        }

        private void NullifyGroupElement(int index, Image target)
        {
            target.sprite = m_NullAssets[index];
        }
    }
}