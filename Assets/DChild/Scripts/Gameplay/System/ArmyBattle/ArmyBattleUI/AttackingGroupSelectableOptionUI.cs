using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class AttackingGroupSelectableOptionUI :  AttackingGroupOptionUI
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
        private TextMeshProUGUI m_targetPartyName;


        public override void Display(IAttackingGroup group)
        {
            if(group == null)
            {
                return;
            }

            base.Display(group);
        }

        private void NullifyArmyGroupUI()
        {
            m_targetPartyName.enabled = false;
            for(int i = 0; i < m_NullAssets.Count; i++)
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