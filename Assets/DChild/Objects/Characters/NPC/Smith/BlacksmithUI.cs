using DChild.Gameplay;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Inventories;
using DChild.Gameplay.Systems;
using Holysoft.Event;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.UI
{
    public class BlacksmithUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_subHeaderLabel;

        [SerializeField] private List<BlacksmithRequirementUI> m_requirementsUI;
        public List<BlacksmithRequirementUI> requirementsUI => m_requirementsUI;

        public EventAction<EventActionArgs> OnUpgradeConfirmed;

        public void SetSubHeaderLabel(WeaponLevel nextLevel)
        {
            m_subHeaderLabel.text = $"Weapon Level: {nextLevel - 1} -> {nextLevel}";
        }

        public void OnConfirmationSuccess()
        {
            OnUpgradeConfirmed?.Invoke(this, EventActionArgs.Empty);
        }

        public void ForceSetUIControls() => GameplaySystem.gamplayUIHandle.OverrideCurrentUIState(
    DChild.UI.GameplayUIState.InteractableUI);
    }

}
