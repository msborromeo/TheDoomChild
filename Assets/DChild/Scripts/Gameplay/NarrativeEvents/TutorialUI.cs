using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.Narrative
{    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_entryTitle;
        [SerializeField] private TutorialEntryUI m_entryUI;
        private TutorialEntry[] m_entryInfos;

        private int pageIndex;

        [ShowInInspector, BoxGroup("TEST DATA"), SerializeField] private TutorialData m_testData;

        [Button]
        public void SetEntry(TutorialData data)
        {
            pageIndex = 0;
            m_entryTitle.text = data.entryTitle;
            m_entryInfos = data.entrySections;
            Display();
        }

        public void Display()
        {
            m_entryUI.Display(m_entryInfos[pageIndex]);
        }

        public void Previous()
        {
            pageIndex++;
        }

        public void Next()
        {
            pageIndex--;
        }

    }
}
