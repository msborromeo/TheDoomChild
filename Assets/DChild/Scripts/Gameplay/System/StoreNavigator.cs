using DChild.Menu.Codex;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Systems
{
    public class StoreNavigator : SerializedMonoBehaviour
    {
        [SerializeField]
        private SignalSender m_openStoreSignal;
        [SerializeField]
        private StorePage m_currentPage;
        public StorePage currentPage => m_currentPage;

        [SerializeField]
        private Dictionary<StorePage, UIToggle> m_pageToggleButtons;

        [SerializeField]
        private CodexPageHandler m_codexHandler;
        public CodexPageHandler codexHandler => m_codexHandler;

        [HideInInspector] public Action<StorePage> OnStoreTabClicked;
        [ReadOnly] private bool m_isOpen = false;

        public void SetPage(StorePage page) => m_currentPage = page;
        public void SetPage(int page) => m_currentPage = (StorePage)page;

        public void OpenStore()
        {
            m_isOpen = true;
            m_openStoreSignal.SendSignal();
            m_currentPage = 0;
        }

        public void OpenPage()
        {
            m_pageToggleButtons[m_currentPage].SetIsOn(true);
        }

        public void Reset()
        {
            OnStoreTabClicked.Invoke(0);
            m_isOpen = false;
        }

        public bool IsStoreOpen() => m_isOpen;
    }
}