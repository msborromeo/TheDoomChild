using Doozy.Runtime.UIManager.Components;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestProgressIndexHandle : MonoBehaviour
    {
        [SerializeField]
        private QuestProgressUI[] m_progressUIs;

        public void Display(Quest quest, bool debugReveal = false)
        {
            if (quest == null) return;

            for (int i = 0; i < quest.entryCount; i++)
            {
                //TODO update subEntry button interactability via quest state
                var subEntryUI = m_progressUIs[i];
                var entry = quest.GetEntry(i);

                SetupSubEntry(subEntryUI, entry, debugReveal);

                subEntryUI.Display(entry, i);
            }
        }

        private void SetupSubEntry(QuestProgressUI targetUI, QuestEntry entry, bool debugReveal)
        {
            targetUI.gameObject.SetActive(true);
            targetUI.SetInteractablility(entry.state != QuestState.Unassigned || debugReveal);
        }

        private void OnDisable() => ResetSubEntryUIs();

        public void ResetSubEntryUIs()
        {
            foreach (QuestProgressUI obj in m_progressUIs)
            {
                if (obj.gameObject.activeSelf)
                    obj.gameObject.SetActive(false);
            }
        }
    }
}