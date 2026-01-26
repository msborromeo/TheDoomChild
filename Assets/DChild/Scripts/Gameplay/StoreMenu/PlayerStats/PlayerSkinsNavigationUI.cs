using DChild.Gameplay.Characters.Player.Skins;
using Holysoft.Event;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.UI.PlayerStats
{
    public class PlayerSkinsNavigationUI : MonoBehaviour
    {
        [SerializeField] private PlayerSkinsUIManager m_uiManager;
        [SerializeField] private List<SkinToggleUI> m_skinSlotToggles;

        private int m_currentIndex;



        public void Previous() => m_currentIndex--;
        public void Next() => m_currentIndex++;


        private void OnToggleSelect(object sender, PlayerSkinArgs eventArgs)
        {
            var selectedSkin = eventArgs.data;

            if (selectedSkin != null)
                m_uiManager.SaveCurrentSkin(selectedSkin);
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

        private void OnDestroy() => ToggleSkinSlotSubscription(false);
    }
}