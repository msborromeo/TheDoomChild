using UnityEngine;

namespace DChild.Menu.Codex
{
    public abstract class CodexGalleryHandle<InfoType, ProgressTracker> : MonoBehaviour
    {
        [SerializeReference]
        protected CodexGalleryUI<InfoType, ProgressTracker> m_gallery;
        [SerializeReference]
        protected CodexGalleryPopupInfoUI<InfoType> m_popupPage;

        public void SetPopupDetails(InfoType data)
        {
            if (m_popupPage != null)
            {
                m_popupPage.ShowInfo(data);
            }
        }

        public virtual void Awake()    
        {
            m_gallery.OnGalleryEntryReceived -= m_popupPage.ShowInfo;
            m_gallery.OnGalleryEntryReceived += m_popupPage.ShowInfo;
        }
    }


}
