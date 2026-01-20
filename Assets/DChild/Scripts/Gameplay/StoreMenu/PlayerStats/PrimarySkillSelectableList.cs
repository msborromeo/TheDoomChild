using DChild.Gameplay.Characters.Players;
using UnityEngine;

namespace DChild.Gameplay.UI.PrimarySkills
{
    public class PrimarySkillSelectableList : MonoBehaviour
    {
        [SerializeField]
        private PrimarySkillList m_data;

        private PrimarySkillSelectable[] m_selectables;

        private int m_firstUnlocked = 0;

        public PrimarySkillSelectable GetFirstAvailable() => m_selectables[m_firstUnlocked];

        public void UpdateListAvailability()
        {
            m_firstUnlocked = m_selectables.Length;

            var skills = GameplaySystem.playerManager.player.skills;

            for (int i = 0; i < m_selectables.Length; i++)
            {
                var selectable = m_selectables[i];
                var isUnlocked = skills.IsSkillUnlocked(selectable.reference.skill);
                
                selectable.SetAsUnlocked(isUnlocked);
                
                if(isUnlocked && i < m_firstUnlocked)
                    m_firstUnlocked = i;
            }
        }

        public void InitializeList()
        {
            m_selectables = GetComponentsInChildren<PrimarySkillSelectable>();
            for (int i = 0; i < m_selectables.Length; i++)
            {
                var selectable = m_selectables[i];
                selectable.SetSelectableFor(m_data.GetData(i));
            }
        }
    }
}