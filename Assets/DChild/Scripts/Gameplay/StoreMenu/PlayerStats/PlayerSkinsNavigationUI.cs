using DChild.Gameplay.Characters.Player.Skins;
using DChild.Gameplay.Items;
using Holysoft.Event;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.UI.PlayerStats
{
    public class PlayerSkinsNavigationUI : MonoBehaviour
    {
        [SerializeField] private List<SkinToggleUI> m_skinSlotToggles;

        private FullSkinList m_fullSkinList;
        private int m_referenceIndex = 0;

        public EventAction<PlayerSkinArgs> OnCurrentSkinUpdated;

        public void SetFullSkinList(FullSkinList value) => m_fullSkinList = value;
        public void Previous() => m_referenceIndex--;
        public void Next() => m_referenceIndex++;

        private void OnToggleSelect(object sender, PlayerSkinArgs eventArgs)
        {
            var selectedSkin = eventArgs.data;

            if (selectedSkin != null)
                OnCurrentSkinUpdated?.Invoke(sender, eventArgs);
        }

        public void UpdateVisibleSkinSlots(List<SkinData> acquiredSkins)
        {
            for (int i = 0; i < m_skinSlotToggles.Count; i++)
            {
                if (i < acquiredSkins.Count)
                {
                    m_skinSlotToggles[i].Display(acquiredSkins[i]);
                    continue;
                }
                m_skinSlotToggles[i].Display(null);
            }
        }

        private void ToggleSkinSlotSubscription(bool toggle)
        {
            foreach (var skinToggle in m_skinSlotToggles)
            {
                if (toggle)
                {
                    skinToggle.OnToggleSelected += OnToggleSelect;
                    continue;
                }
                skinToggle.OnToggleSelected -= OnToggleSelect;
            }
        }

        private void Awake() => ToggleSkinSlotSubscription(true);

        private void OnDisable() => ToggleSkinSlotSubscription(false);
    }
}