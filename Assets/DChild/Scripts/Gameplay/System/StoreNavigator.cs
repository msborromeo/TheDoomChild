using DChild.Menu.Codex;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityCursor = UnityEngine.Cursor;

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
        private UIContainer m_storeView;
        private bool m_ownsCursor;
        private bool m_previousCursorVisibility;
        private CursorLockMode m_previousCursorLock;

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

        private void OnEnable()
        {
            if (TryGetComponent(out m_storeView))
            {
                m_storeView.OnVisibilityChangedCallback.AddListener(OnStoreVisibilityChanged);
            }
        }

        private void OnDisable()
        {
            if (m_storeView != null)
            {
                m_storeView.OnVisibilityChangedCallback.RemoveListener(OnStoreVisibilityChanged);
            }

            RestoreCursor();
        }

        private void OnStoreVisibilityChanged(VisibilityState state)
        {
            if (state == VisibilityState.IsShowing && GameSystem.CurrentGameMode == GameMode.Overworld)
            {
                ShowCursor();
            }
            else if (state == VisibilityState.IsHiding)
            {
                RestoreCursor();
            }
        }

        private void ShowCursor()
        {
            if (m_ownsCursor || (UnityCursor.visible && UnityCursor.lockState == CursorLockMode.None))
            {
                return;
            }

            m_previousCursorVisibility = UnityCursor.visible;
            m_previousCursorLock = UnityCursor.lockState;
            m_ownsCursor = true;
            GameSystem.SetCursorVisibility(true);
        }

        private void RestoreCursor()
        {
            if (!m_ownsCursor)
            {
                return;
            }

            m_ownsCursor = false;
            if (GameplaySystem.isGamePaused)
            {
                return;
            }

            GameSystem.SetCursorVisibility(m_previousCursorVisibility);
            UnityCursor.lockState = m_previousCursorLock;
        }
    }
}