using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI.BulletinBoard
{
    public class CharacterDetailsUI : MonoBehaviour
    {
        [SerializeField] private Image m_portrait;
        [SerializeField] private TextMeshProUGUI m_characterName;
        [SerializeField] private TextMeshProUGUI m_attackPower;
        [SerializeField] private TextMeshProUGUI m_troopCount;


        public void Display(CharacterRecruitmentData character)
        {
            if (character == null)
                return;

            m_portrait.sprite = character.characterData.icon;
            m_characterName.text = $"{character.characterData.name}";
            m_attackPower.text = $"{character.characterData.attackPower}";
            m_troopCount.text = $"{character.characterData.troopCount}";
        }
    }
}