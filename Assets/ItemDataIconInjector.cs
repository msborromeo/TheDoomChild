using DChild.Gameplay.EquipmentSystem;
using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemDataIconInjector : MonoBehaviour
{
    public List<Sprite> ItemIcons;
    public List<SoulEquipmentItem> ItemData;
    public List<Sprite> Non_used_Icons;
    public List<ItemData> Non_used_Items;

#if UNITY_EDITOR
    [Button]
    private void SetIconsIntoSlots()
    {
        foreach (var Icon in ItemIcons)
        {
            string uniformedIconName = Icon.name.Replace("-", "").Replace("_", "").ToUpper();
            Debug.Log(uniformedIconName + " 1");
            foreach (var Item in ItemData)
            {
                string uniformedName = Item.name.Replace(" ", "").ToUpper();
                uniformedName = uniformedName.Replace("DATA", "");
                Debug.Log(uniformedName + " 2");
                if (uniformedIconName.Contains(uniformedName))
                {
                    Item.SetslotIcon(Icon);
                    Non_used_Icons.Add(Icon);
                    break;
                }
            }
            
        }

        EditorUtility.SetDirty(this);
    }

    [Button]
    private void GetnoSlotIconData()
    {
        foreach (var Item in ItemData)
        {
            if(Item.slotIcon==null)
            {
                Non_used_Items.Add(Item);
            }
        }
        EditorUtility.SetDirty(this);
    }

    [Button]
    private void SetIconsIntoEquipped()
    {
        foreach (var Icon in ItemIcons)
        {
            string uniformedIconName = Icon.name.Replace("-", "").ToLower();
            Debug.Log(uniformedIconName+" 1");
            foreach (var Item in ItemData)
            {
                string uniformedName = Item.name.Replace(" ", "").ToLower();
                uniformedName.Replace("data","");
                Debug.Log(uniformedName+" 2");
                if (uniformedIconName.Contains(uniformedName) && Item.slotIcon == null)
                {
                    Item.SetEquippedIcon(Icon);
                    break;
                }
            }
        }
        EditorUtility.SetDirty(this);
    }

    [Button]
    private void SetIcons()
    {
        foreach (var Icon in ItemIcons)
        {
            string uniformedIconName = Icon.name.Replace("-", "").ToLower();
            Debug.Log(uniformedIconName + " 1");
            foreach (var Item in ItemData)
            {
                string slot;
                //if (Item.soulEquipment.Slot == SoulSlot.Pauldron)
                //{
                //    slot = "arms";
                //}else
                //{
                    slot = Item.soulEquipment.Slot.ToString().ToLower();
                //}
                
                if (uniformedIconName.Contains(slot))
                {
                    Item.SetIcon(Icon);
                    Debug.Log(slot+" icon is updated");
                    break;
                }
            }
        }
        EditorUtility.SetDirty(this);
    }
#endif
}
