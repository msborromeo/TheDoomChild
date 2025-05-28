using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI.BulletinBoard
{
    public class BulletinCardUI : MonoBehaviour
    {

        [SerializeField] private CharacterDetailsUI m_characterUI;
        [SerializeField] private RecruitmentDetailsUI m_recruitmentUI;

        private CharacterRecruitmentData m_character;
        public CharacterRecruitmentData character => m_character;

        public void SetCharacterData(CharacterRecruitmentData value)
        {
            m_character = value;
        }

        [Button]
        public void Display(CharacterRecruitmentData value)
        {
            m_character = value;
            m_characterUI.Display(m_character);
            m_recruitmentUI.Display(m_character);
        }

        public void RecruitUnit()
        {
            //m_characterReward..RecruitCharacter(m_character);
        }

        private void Awake()
        {
            SetCharacterData(m_character);
        }
    }
}