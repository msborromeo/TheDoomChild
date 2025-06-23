using DChild.Gameplay;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Inventories;
using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class OnItemPickupEvent : SerializedMonoBehaviour
{
    [SerializeField]
    private ItemData m_item;
    [SerializeField]
    private UnityEvent m_event;

    protected Player m_player;

    private void onPickup(object sender, ItemEventArgs eventArgs)
    {
        if (eventArgs.data.itemName == m_item.itemName)
        {
            m_event?.Invoke();
        }
    }

    protected virtual void OnEnable()
    {
        var currentPlayer = GameplaySystem.playerManager.player;
        if (m_player == null || m_player == currentPlayer)
        {
            m_player = GameplaySystem.playerManager.player;
            m_player.inventory.InventoryItemUpdate += onPickup;
        }
    }

    protected virtual void OnDisable()
    {
        if(m_player != null)
        {
            m_player.inventory.InventoryItemUpdate -= onPickup;
        }
    }
}
