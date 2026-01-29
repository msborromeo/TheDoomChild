using DChild.Gameplay.Characters.Player.Skins;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.UI.PlayerStats
{
    public class PlayerSkinsActivationDebug : MonoBehaviour
    {
        public void UnlockAllSkins()
        {
            var skinHandle = GameplaySystem.playerManager.player.skinHandle;
            int[] allIDs = skinHandle.fullSkinList.GetIDs();

            // Get a reference to what we already own to avoid duplicates
            var alreadyOwned = skinHandle.acquiredSkins;

            for (int i = 0; i < allIDs.Length; i++)
            {
                int currentID = allIDs[i];

                // Only add if the ID doesn't exist in our acquired list yet
                if (!alreadyOwned.Any(s => s.id == currentID))
                {
                    skinHandle.AddAcquiredSkin(skinHandle.fullSkinList.GetInfo(currentID));
                }
            }
        }
    }
}