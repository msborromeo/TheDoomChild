using DChild.Codex.Characters;
using DChild.Gameplay.ArmyBattle;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Codex.ArmyTroops
{
    public class ArmyTroopsUnitEntryUI : MonoBehaviour
    {

        [BoxGroup("TMP Panels"), SerializeField] private TextMeshProUGUI m_unitName;
        [BoxGroup("TMP Panels"), SerializeField] private TextMeshProUGUI m_unitDescription;
        [BoxGroup("TMP Panels"), SerializeField] private TextMeshProUGUI m_troopCount;
        [BoxGroup("TMP Panels"), SerializeField] private TextMeshProUGUI m_attackPower;

        [SerializeField, FoldoutGroup("Army Type Sprites")] private Image m_iconType;
        [SerializeField, FoldoutGroup("Army Type Sprites")] private Sprite m_meleeIcon;
        [SerializeField, FoldoutGroup("Army Type Sprites")] private Sprite m_magicIcon;
        [SerializeField, FoldoutGroup("Army Type Sprites")] private Sprite m_rangedIcon;

        [Button]
        public void Display(CharacterCodexData character, DamageType type)
        {
            m_unitName.text = character.armyData.name;
            m_unitDescription.text = character.armyData.description ?? character.description;
            m_troopCount.text = $"{character.armyData.troopCount}";
            m_attackPower.text = $"{character.armyData.attackPower}";

            SetIconType(type);
        }

        private void SetIconType(DamageType type)
        {
            switch (type)
            {
                case DamageType.Melee:
                    m_iconType.sprite = m_meleeIcon;
                    break;
                case DamageType.Range:
                    m_iconType.sprite = m_rangedIcon;
                    break;
                case DamageType.Magic:
                    m_iconType.sprite = m_magicIcon;
                    break;
            }
        }
    }
}