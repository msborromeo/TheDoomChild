using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DChild.Gameplay.Items;
using DChild.Gameplay.Inventories;
using DChild.Gameplay;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ArmyBattleRewardGiver : SerializedMonoBehaviour
{
    [SerializeField]
    private int m_SoulEssence;
    [SerializeField,TabGroup("ITEMS"), DictionaryDrawerSettings(KeyLabel = "Item", ValueLabel = "Amount")]
    private Dictionary<ItemData,int> m_Items = new Dictionary<ItemData, int>();
    private PlayerInventory m_Inventory;
    // Start is called before the first frame update
    void Start()
    {
        if(!GameplaySystem.playerManager.player)
        {
            return;
        }
        m_Inventory = GameplaySystem.playerManager.player.inventory;
    }

    [Button]
    public void GiveReward()
    {
        foreach(ItemData item in m_Items.Keys)
        {
            m_Inventory.AddItem(item,m_Items.GetValueOrDefault(item));
        }
        m_Inventory.AddSoulEssence(m_SoulEssence);
    }
}
