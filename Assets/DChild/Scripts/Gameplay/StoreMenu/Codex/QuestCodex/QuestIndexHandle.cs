using DChild.Gameplay.ArmyBattle;
using DChild.Menu.Codex;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Collections;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestIndexHandle : MonoBehaviour
    {
        [SerializeField] private CodexScrollNavigationHandle m_navigationHandle;
        [SerializeField] private List<QuestButtonUI> m_questButtons;

        private Quest[] m_quests;
        private int m_startingIndex = 0;

        public event EventAction<EventActionArgs> PageChange;

        private bool m_revealAllQuests;
        public void Initialize(Quest[] quests, bool debugReveal)
        {
            m_revealAllQuests = debugReveal;

            m_quests = quests;

            m_navigationHandle.SetupScroll(quests.Length, m_questButtons.Count);
            SetPage(0);
        }

        public void Display(int startOffset, bool debugReveal = false)
        {
            var selectedFirst = false;

            for (int i = 0; i < m_questButtons.Count; i++)
            {
                var questButton = m_questButtons[i];

                int dataIndex = i + startOffset;

                var hasData = dataIndex < m_quests.Length;
                questButton.gameObject.SetActive(hasData);

                if (!hasData)
                    continue;

                var questData = m_quests[dataIndex];
                questButton.Display(questData);

                var discoveredQuest = questData.state != QuestState.Unassigned;
                questButton.SetInteractability(discoveredQuest || debugReveal);

                if (selectedFirst == false && discoveredQuest)
                {
                    questButton.Select();
                    selectedFirst = true;
                }
            }
        }

        public void SetPage(int pageIndex)
        {
            m_startingIndex = pageIndex * m_questButtons.Count;
            Display(m_startingIndex, m_revealAllQuests);
        }


        private void Awake()
        {
            m_navigationHandle.OnCurrentPageChange += SetPage;
        }
    }
}