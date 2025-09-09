using DChild.Gameplay.LevelFinish.UI;
using DChild.Gameplay.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay
{
    public class LevelEndHandle : MonoBehaviour
    {
        [SerializeField]
        private InputActionConfiguration m_overworldInputReference;

        [SerializeField]
        private InputActionConfiguration m_throneRoomInputReference;

        public void DisplayTeleportToOverworldBanner()
        {
            GameplaySystem.gamplayUIHandle.NotifyUnlockedLocation(AvailableLocations.Overworld, m_overworldInputReference);
        }

        public void DisplayTeleportToThroneRoomBanner()
        {
            GameplaySystem.gamplayUIHandle.NotifyUnlockedLocation(AvailableLocations.Throne_Room, m_throneRoomInputReference);
        }

        public void DisplayBothTeleportBanners()
        {
        }

        private IEnumerator DisplayBannersRoutine()
        {
            DisplayTeleportToOverworldBanner();
            yield return new WaitForSeconds(2f);
            DisplayTeleportToThroneRoomBanner();
        }
    }
}

