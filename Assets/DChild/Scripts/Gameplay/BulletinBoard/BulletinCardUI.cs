using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI.BulletinBoard
{
    public class BulletinCardUI : MonoBehaviour
    {
        private ArmyCharacterData m_character;
        public ArmyCharacterData character => m_character;

        public void SetCharacterData(ArmyCharacterData value)
        {
            m_character = value;
        }

    }
}