using DChild.Gameplay;
using DChild.Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Items
{
    public class ItemRemover : MonoBehaviour
    {
        [SerializeField]
        private ItemData m_data;

        public void RemoveItem()
        {
            GameplaySystem.playerManager.player.inventory.RemoveItem(m_data);
        }
    }
}
