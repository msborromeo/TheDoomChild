using DChild.Codex.Characters;
using DChild.Gameplay.ArmyBattle;
using DChild.Gameplay.ArmyBattle.SpecialSkills;
using Sirenix.OdinInspector;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Codex.ArmyTroops
{
    public class ArmyTroopsGroupInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_groupName;
        [SerializeField] private TextMeshProUGUI m_skillDescription;

        [BoxGroup("Damage Type"), SerializeField] private TextMeshProUGUI m_damageType;
        [BoxGroup("Damage Type"), SerializeField] private Image m_iconType;
        [BoxGroup("Damage Type"), SerializeField, FoldoutGroup("Damage Type/Army Type Sprites")] private Sprite m_meleeIcon;
        [BoxGroup("Damage Type"), SerializeField, FoldoutGroup("Damage Type/Army Type Sprites")] private Sprite m_magicIcon;
        [BoxGroup("Damage Type"), SerializeField, FoldoutGroup("Damage Type/Army Type Sprites")] private Sprite m_rangedIcon;

        [Button]
        public void Display(ArmyGroupTemplateData groupData)
        {
            m_groupName.text = groupData.armyCharacterGroup.name;
            SetTypeVisuals(groupData.damageType);

            SetSkillSectionVisibility(groupData.specialSkill);
        }

        private void SetSkillSectionVisibility(SpecialSkill skill)
        {
            bool hasSkill = skill != null;

            m_skillDescription.transform.parent.gameObject.SetActive(hasSkill);

            if (!hasSkill) return;

            m_skillDescription.text = hasSkill ? skill.GetDescription() : "";
        }

        private void SetTypeVisuals(DamageType type)
        {
            switch (type)
            {
                case DamageType.Melee:
                    m_damageType.text = "Melee";
                    m_iconType.sprite = m_meleeIcon;
                    break;
                case DamageType.Range:
                    m_damageType.text = "Ranged";
                    m_iconType.sprite = m_rangedIcon;
                    break;
                case DamageType.Magic:
                    m_damageType.text = "Magic";
                    m_iconType.sprite = m_magicIcon;
                    break;
            }
        }
    }
}