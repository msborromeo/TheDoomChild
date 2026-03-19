using DChild.Gameplay.Items;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public abstract class ItemUI : MonoBehaviour
    {
        [SerializeField]
        protected ItemDetailsUI m_detailsUI;

        protected IStoredItem m_reference;

        public IStoredItem reference => m_reference;

        public abstract void Show();
        public abstract void Hide();

        public abstract void SetItemFrame(Sprite value);
        public abstract void SetIconColor(bool isModified);

        public void SetReference(IStoredItem reference)
        {
            if (m_reference != reference)
            {
                m_reference = reference;
            }

            ShowDetailsOf(m_reference);
        }

        protected virtual void ShowDetailsOf(IStoredItem reference)
        {
            m_detailsUI.ShowDetails(reference);
        }
    }
}