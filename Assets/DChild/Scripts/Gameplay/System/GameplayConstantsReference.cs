using DChild.Gameplay.Items;
using UnityEngine;

namespace DChild.Gameplay.Systems
{
    public class GameplayConstantsReference : MonoBehaviour, IGameplaySystemModule
    {
        [SerializeField]
        private ItemData m_silverCoinItemData;

        public ItemData silverCoinItemData => m_silverCoinItemData;
    }
}