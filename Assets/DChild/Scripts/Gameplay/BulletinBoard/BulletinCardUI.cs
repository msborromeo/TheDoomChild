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
        [SerializeField] private CharacterGiver m_systemRecruiter;


        private CharacterRecruitmentData m_character;
        public CharacterRecruitmentData character => m_character;

        public void SetCharacterData(CharacterRecruitmentData value)
        {
            m_character = value;
        }

        [Button]
        public void Display(CharacterRecruitmentData value)
        {
            if (value == null)
            {
                gameObject.SetActive(false);
                return;
            }

            m_character = value;
            m_characterUI.Display(m_character);
            m_recruitmentUI.Display(m_character);
            gameObject.SetActive(true);

        }

        public void RecruitCharacter()
        {
            m_systemRecruiter.RecruitCharacter(m_character.characterData);
        }

        private void Awake()
        {
            SetCharacterData(m_character);
        }
    }
}