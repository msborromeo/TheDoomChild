using DChild.Gameplay.Characters.Players;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using Sirenix.OdinInspector;
using static DChild.Gameplay.UI.CombatArts.CombatArtSelectButton;
using System;

namespace DChild.Gameplay.UI.CombatArts
{
    [RequireComponent(typeof(UIButton))]
    public class CombatArtSelectButton : MonoBehaviour
    {
        [SerializeField, HideInPrefabAssets, OnValueChanged("OnConfigurationChanged")]
        private CombatArt m_toUnlock;
        [SerializeField, HideInPrefabAssets, OnValueChanged("OnConfigurationChanged"), MinValue(1)]
        private int m_unlockLevel = 1;
        [ShowInInspector, ReadOnly]
        private CombatArtUnlockState m_currentState = CombatArtUnlockState.Unlockable;
        [SerializeField]
        private CombatArtSelectButtonVisual m_visuals;

        public event Action<CombatArtSelectButton> OnButtonSelected;

        public CombatArt skillUnlock => m_toUnlock;
        public int unlockLevel => m_unlockLevel;
        public CombatArtUnlockState currentState => m_currentState;

        private UIButton m_button;
        public UIButton uiButton => m_button;

        private void EnsureReference()
        {
            m_button = GetComponent<UIButton>();
        }

        public void SetState(CombatArtUnlockState state)
        {
            m_currentState = state;
            m_visuals.SetState(state);
        }

        public void ForceVisualSync() => m_visuals.SetState(m_currentState);

        public void Select()
        {
            OnButtonSelected?.Invoke(this);
        }

        public void DisplayAs(CombatArtLevelData artLevelData) => m_visuals.DisplayAs(artLevelData);

        private void Awake()
        {
            EnsureReference();
            m_visuals.Initialize(m_button);
        }

#if UNITY_EDITOR
        private void OnConfigurationChanged()
        {
            gameObject.name = "CombatArtSelectable_" + m_toUnlock.ToString().Replace(" ", "") + m_unlockLevel.ToString();
        }
#endif
    }

}