using UnityEngine;

namespace DChild.Codex.Characters
{

    public abstract class CodexGalleryHandle<InfoType, ProgressTracker> : MonoBehaviour
    {
        [SerializeReference]
        protected CodexGalleryUI<InfoType, ProgressTracker> m_gallery;
        [SerializeReference]
        private CodexGalleryPopupInfoUI<InfoType> m_popupPage;

        public void SetPopupDetails(InfoType data)
        {
            if (m_popupPage != null)
            {
                m_popupPage.ShowInfo(data);
            }
        }

        private void Awake()    
        {
            m_gallery.OnGalleryEntryReceived += m_popupPage.ShowInfo;

        }
        private void OnDestroy()
        {
            m_gallery.OnGalleryEntryReceived -= m_popupPage.ShowInfo;
        }
    }


}
