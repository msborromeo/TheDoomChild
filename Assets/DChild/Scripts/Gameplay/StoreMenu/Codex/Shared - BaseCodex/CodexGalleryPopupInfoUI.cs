using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Menu.Codex
{
    public abstract class CodexGalleryPopupInfoUI<InfoType> : MonoBehaviour
    {
        [ShowInInspector, OnValueChanged("UpdateInfo")]
        protected InfoType m_showDataOf;
        public void ShowInfo(InfoType data)
        {
            if (m_showDataOf == null || !m_showDataOf.Equals(data))
            {
                m_showDataOf = data;
                UpdateInfo();
            }
        }

        protected abstract void UpdateInfo();
    }
}
