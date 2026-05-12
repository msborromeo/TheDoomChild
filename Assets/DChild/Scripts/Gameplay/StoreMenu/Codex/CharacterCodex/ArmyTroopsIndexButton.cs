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

        private List<CharacterCodexData> m_codexData = new();
        public List<CharacterCodexData> codexData => m_codexData;


        private UIButton m_button;
        public Action<ArmyGroupTemplateData> OnArmyDataSent;
        public Action<List<CharacterCodexData>> OnCodexDataSent;

        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private Image m_unitOne;
        [SerializeField] private Image m_unitTwo;
        [SerializeField] private Image m_unitThree;

        [BoxGroup("Icon Handling"), SerializeField] private Image m_typeIcon;
        [BoxGroup("Icon Handling"), FoldoutGroup("Icon Handling/Sprites"), SerializeField] private Sprite m_meleeIcon;
        [BoxGroup("Icon Handling"), FoldoutGroup("Icon Handling/Sprites"), SerializeField] private Sprite m_magicIcon;
        [BoxGroup("Icon Handling"), FoldoutGroup("Icon Handling/Sprites"), SerializeField] private Sprite m_rangedIcon;


        #region Setters
        public void SetArmyData(ArmyGroupTemplateData groupData)
        {
            m_armyData = groupData;

            UpdateUI(groupData.armyCharacterGroup);
            UpdateGroupIcon(groupData.damageType);
        }
        public void AddUnitCodexData(CharacterCodexData codexData)
        {
            m_codexData.Add(codexData);
        }
        public void SetGalleryPopupData()
        {
            OnCodexDataSent.Invoke(m_codexData);
            OnArmyDataSent.Invoke(m_armyData);
        }
        #endregion

        #region UI Visuals
        private void UpdateUI(ArmyCharacterGroup group)
        {
            m_name.text = group.name;

            Image[] spriteIcons = { m_unitOne, m_unitTwo, m_unitThree };

            for (int i = 0; i < spriteIcons.Length; i++)
            {
                bool isActive = i < group.memberCount;
                spriteIcons[i].gameObject.SetActive(isActive);

                if (isActive)
                    SetUnitIcon(group.GetCharacter(i).icon, spriteIcons[i]);
            }
        }
        private void SetUnitIcon(Sprite icon, Image target)
        {
            target.sprite = icon;
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
        public void SetInteractable(bool isInteractable)
        {
            EnsureReferences();
            if (m_button != null)
                m_button.interactable = isInteractable;
        }

        public void Select()
        {
            m_button?.Select();
        }
        #endregion

    }
}

