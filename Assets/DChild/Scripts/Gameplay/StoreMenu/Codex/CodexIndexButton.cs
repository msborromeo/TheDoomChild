using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Codex
{
    public abstract class CodexIndexButton<DatabaseAssetType> : MonoBehaviour where DatabaseAssetType : DatabaseAsset
    {
        [SerializeField, OnValueChanged("UpdateInfo")]
        protected DatabaseAssetType m_data;

        private CanvasGroup m_canvas;

        private UIToggle m_toggle;
        private UIButton m_button;

        // A helper property to treat both as a basic 'Selectable'
        private Selectable m_selectable => (Selectable)m_toggle ?? m_button;

        public bool isAvailable => m_selectable.gameObject.activeInHierarchy && m_selectable.interactable;
        public DatabaseAssetType data => m_data;
        public abstract void SetData(DatabaseAssetType data);

        public void SetIsOn(bool isOn)
        {
            if (m_toggle != null)
            {
                if (m_toggle.isOn != isOn)
                {
                    m_toggle.SetIsOn(isOn);
                    if (isOn) m_toggle.Select();
                    m_toggle.SendSignal(isOn);
                }
            }
            else if (m_button != null && isOn)
            {
                // Buttons don't have 'isOn', but we can still 'Select' them
                m_button.Select();
            }
        }

        public void Select()
        {
            m_selectable?.Select();
        }

        public void SetInteractable(bool isInteractable)
        {
            EnsureReferences();
            if (m_selectable != null)
                m_selectable.interactable = isInteractable;
        }

        private void EnsureReferences()
        {
#if UNITY_EDITOR
            if (m_toggle == null && m_button == null)
            {
                m_toggle = GetComponent<UIToggle>();
                m_button = GetComponent<UIButton>();
            }
#endif
        }

        private void Awake()
        {
            m_toggle = GetComponent<UIToggle>();
            m_button = GetComponent<UIButton>();

            // Safety check: Ensure at least one exists
            if (m_toggle == null && m_button == null)
            {
                Debug.LogError($"{gameObject.name} needs either a UIToggle or UIButton!");
            }
        }
    }

    public abstract class CodexIndexButton<DatabaseAssetType, IndexInfoType> : CodexIndexButton<DatabaseAssetType> where DatabaseAssetType : DatabaseAsset, IndexInfoType
    {
        [SerializeReference]
        private CodexIndexInfoUI<IndexInfoType> m_info;

        public override void SetData(DatabaseAssetType data)
        {
            if (m_data != data)
            {
                m_data = data;
                m_info?.SetInfo(data);
            }
        }
        private void Start()
        {
            if (m_data != null)
            {
                m_info.SetInfo(m_data);
            }
        }

#if UNITY_EDITOR
        [Button]
        private void UpdateInfo()
        {
            m_info.SetInfo(m_data);
        }
#endif
    }
}