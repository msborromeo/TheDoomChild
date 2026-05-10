using DChild.Codex.Characters;
using DChild.Gameplay.ArmyBattle;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Codex.ArmyTroops
{

    public class ArmyTroopsIndexButton : MonoBehaviour
    {
        private ArmyGroupTemplateData m_armyData;
        public ArmyGroupTemplateData armyData => m_armyData;

        private List<CharacterCodexData> m_codexData;
        public List<CharacterCodexData> codexData => m_codexData;


        private UIButton m_button;
        public Action<ArmyGroupTemplateData> OnEntrySelected;

        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private Image m_unitOne;
        [SerializeField] private Image m_unitTwo;
        [SerializeField] private Image m_unitThree;
        
        [BoxGroup("Icon Handling"), SerializeField] private Image m_typeIcon;
        [BoxGroup("Icon Handling"), FoldoutGroup("Icon Handling/Sprites"), SerializeField] private Sprite m_meleeIcon;
        [BoxGroup("Icon Handling"), FoldoutGroup("Icon Handling/Sprites"), SerializeField] private Sprite m_magicIcon;
        [BoxGroup("Icon Handling"), FoldoutGroup("Icon Handling/Sprites"), SerializeField] private Sprite m_rangedIcon;


        public void SetArmyData(ArmyGroupTemplateData groupData)
        {
            m_armyData = groupData;

            UpdateUI(groupData.armyCharacterGroup);
            UpdateGroupIcon(groupData.damageType);
        }

        private void UpdateUI(ArmyCharacterGroup group)
        {
            m_name.text = group.name;
            m_unitOne.sprite = group.GetCharacter(0).icon;
            m_unitTwo.sprite = group.GetCharacter(1).icon;
            m_unitThree.sprite = group.GetCharacter(2).icon;
        }

        private void UpdateGroupIcon(DamageType value)
        {
            switch (value)
            {
                case DamageType.Melee:
                    m_typeIcon.sprite = m_meleeIcon;
                    break;
                case DamageType.Range:
                    m_typeIcon.sprite = m_rangedIcon;
                    break;
                case DamageType.Magic:
                    m_typeIcon.sprite = m_magicIcon;
                    break;
            }
        }

        public void AddUnitCodexData(CharacterCodexData codexData)
        {
            m_codexData.Add(codexData);
        }

        public void Select()
        {
            m_button?.Select();
        }

        public void SetInteractable(bool isInteractable)
        {
            EnsureReferences();
            if (m_button != null)
                m_button.interactable = isInteractable;
        }
        private void EnsureReferences()
        {
#if UNITY_EDITOR
            if (m_button == null)
            {
                m_button = GetComponent<UIButton>();
            }
#endif
        }


    }
}

