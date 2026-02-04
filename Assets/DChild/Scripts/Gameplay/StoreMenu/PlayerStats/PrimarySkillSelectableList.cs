using DChild.Gameplay.Characters.Players;
using UnityEngine;

namespace DChild.Gameplay.UI.PrimarySkills
{
    public class PrimarySkillSelectableList : MonoBehaviour
    {
        [SerializeField]
        private PrimarySkillList m_data;

        private PrimarySkillSelectable[] m_selectables;

        private int m_firstUnlocked = -1;

        public PrimarySkillSelectable GetFirstAvailable()
        {
            if (m_firstUnlocked == -1) return null;

            return m_selectables[m_firstUnlocked];
        }

        public void UpdateListAvailability()
        {
            var skills = GameplaySystem.playerManager.player.skills;
            bool foundFirst = false;

            for (int i = 0; i < m_selectables.Length; i++)
            {
                var selectable = m_selectables[i];
                bool isUnlocked = skills.IsSkillUnlocked(selectable.reference.skill);

                selectable.SetAsUnlocked(isUnlocked);

                if (isUnlocked && !foundFirst)
                {
                    m_firstUnlocked = i;
                    foundFirst = true;
                }
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