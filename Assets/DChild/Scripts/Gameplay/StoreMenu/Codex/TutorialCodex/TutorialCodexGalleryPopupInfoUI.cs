using DChild.Codex.Tutorial;
using DChild.Gameplay.Narrative;
using TMPro;
using UnityEngine;

namespace DChild.Menu.Codex.Tutorials
{
    public class TutorialCodexGalleryPopupInfoUI : CodexGalleryPopupInfoUI<TutorialCodexData>
    {
        [SerializeField] private TextMeshProUGUI m_titlePanel;
        [SerializeField] private TutorialEntryUI m_entryUI;

        protected override void UpdateInfo()
        {
            m_entryUI.gameObject.SetActive(m_showDataOf != null);
            if (m_showDataOf == null) return;

            m_titlePanel.text = m_showDataOf.indexName;
            m_entryUI.Display(m_showDataOf);
        }
    }
}
