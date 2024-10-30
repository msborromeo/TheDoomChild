using UnityEngine;

namespace DChild.Gameplay.ArmyBattle
{
    [System.Serializable]
    public class ArmyCharactersSaveData
    {
        [SerializeField]
        private int[] m_recruitedCharacterIds;

        public ArmyCharactersSaveData()
        {
            m_recruitedCharacterIds = new int[0];
        }

        public ArmyCharactersSaveData(ArmyCharacterData[] characters)
        {
            m_recruitedCharacterIds = new int[characters.Length];
            for (int i = 0; i < m_recruitedCharacterIds.Length; i++)
            {
                m_recruitedCharacterIds[i] = characters[i].ID;
            }
        }

        public ArmyCharactersSaveData(ArmyCharactersSaveData reference)
        {
            m_recruitedCharacterIds = new int[reference.recruitedCharacterCount];
            for (int i = 0; i < m_recruitedCharacterIds.Length; i++)
            {
                m_recruitedCharacterIds[i] = reference.GetRecruitedCharacterID(i);
            }
        }

        public ArmyCharactersSaveData(int[] recruitedCharacterIds)
        {
            m_recruitedCharacterIds = recruitedCharacterIds;
        }

        public bool HasCharacter(ArmyCharacterData character)
        {
            for (int i = 0; i < m_recruitedCharacterIds.Length; i++)
            {
                if (m_recruitedCharacterIds[i] == character.ID)
                    return true;
            }
            return false;
        }

        public int recruitedCharacterCount => m_recruitedCharacterIds.Length;

        public int GetRecruitedCharacterID(int index) => m_recruitedCharacterIds[index];
    }
}