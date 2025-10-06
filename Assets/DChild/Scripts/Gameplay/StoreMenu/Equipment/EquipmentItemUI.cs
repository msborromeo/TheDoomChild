using DChild.Gameplay.EquipmentSystem;
using NSubstitute.Exceptions;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemUI : MonoBehaviour
{
    [SerializeField]
    private SoulEquipmentItem m_item;

    [SerializeField]
    private Image m_itemIcon;

    [Button]
    private void Display(SoulEquipmentItem item)
    {
        if (m_item == null)
            return;

        m_itemIcon.sprite = m_item.icon;
    }
}
