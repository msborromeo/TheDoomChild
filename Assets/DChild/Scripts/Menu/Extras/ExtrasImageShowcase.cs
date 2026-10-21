using DChild.Menu.Extras;
using System;
using Holysoft.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Extras
{
    public class ExtrasImageShowcase : AdjacentNavigation
    {
#if UNITY_EDITOR
        [SerializeField]
#endif
        private SpriteList m_list;

        [SerializeField]
        private Image m_image;
        [SerializeField] private Button m_next;
        [SerializeField] private Button m_previous;
        private CanvasGroup m_group;

        internal event Action Opened;
        internal event Action Closed;
        internal bool isOpen { get; private set; }

        protected override int lastNavigationIndex => m_list == null ? -1 : m_list.count - 1;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
            Close();
        }

        private void Initialize()
        {
            if (m_group != null) return;
            m_group = GetComponent<CanvasGroup>();
            if (m_group == null) m_group = gameObject.AddComponent<CanvasGroup>();
            // The legacy showcase was parked outside the screen instead of having a view lifecycle.
            ((RectTransform)transform).anchoredPosition = Vector2.zero;
            transform.SetAsLastSibling();
        }

        public void Showcase(SpriteList list, int index)
        {
            if (list == null || index < 0 || index >= list.count || m_image == null) return;
            Initialize();
            m_list = list;
            m_currentNavigationIndex = index;
            m_image.sprite = m_list.GetSprite(m_currentNavigationIndex);
            m_image.preserveAspect = true;
            var color = m_image.color;
            color.a = 1f;
            m_image.color = color;
            bool wasOpen = isOpen;
            isOpen = true;
            m_group.alpha = 1;
            m_group.interactable = true;
            m_group.blocksRaycasts = true;
            RefreshButtons();
            if (!wasOpen) Opened?.Invoke();
        }

        internal void Close()
        {
            Initialize();
            bool wasOpen = isOpen;
            isOpen = false;
            m_group.alpha = 0;
            m_group.interactable = false;
            m_group.blocksRaycasts = false;
            RefreshButtons();
            if (wasOpen) Closed?.Invoke();
        }

        public override void Next()
        {
            if (!isOpen) return;
            base.Next();
            RefreshButtons();
        }

        public override void Previous()
        {
            if (!isOpen) return;
            base.Previous();
            RefreshButtons();
        }

        private void RefreshButtons()
        {
            if (m_next != null) m_next.interactable = isOpen && m_currentNavigationIndex < lastNavigationIndex;
            if (m_previous != null) m_previous.interactable = isOpen && m_currentNavigationIndex > 0;
        }

        protected override void GoToNextItem()
        {
            m_currentNavigationIndex++;
            m_image.sprite = m_list.GetSprite(m_currentNavigationIndex);
        }

        protected override void GoToPreviousItem()
        {
            m_currentNavigationIndex--;
            m_image.sprite = m_list.GetSprite(m_currentNavigationIndex);
        }
    }
}
