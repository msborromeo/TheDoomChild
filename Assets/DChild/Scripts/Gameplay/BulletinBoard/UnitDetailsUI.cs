using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI.BulletinBoard
{
    public class UnitDetailsUI : MonoBehaviour
    {
        [SerializeField] private Image m_portrait;
        [SerializeField] private TextMeshProUGUI m_unitName;
        [SerializeField] private TextMeshProUGUI m_attackPower;
        [SerializeField] private TextMeshProUGUI m_troopCount;


        public void Display(ArmyCharacterData character)
        {
            m_portrait.sprite = character.icon;
            m_unitName.text = $"{character.name}";
            m_attackPower.text = $"{character.attackPower}";
            m_troopCount.text = $"{character.troopCount}";
        }
    }
}