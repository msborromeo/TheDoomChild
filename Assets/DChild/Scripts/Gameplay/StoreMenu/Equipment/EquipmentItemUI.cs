using DChild.Gameplay.EquipmentSystem;
using NSubstitute.Exceptions;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentItemUI : MonoBehaviour
    {
        [SerializeField]
        private Image m_itemIcon;

        public void Display(SoulEquipmentItem item)
        {
            gameObject.SetActive(item != null);
            if (item == null)
                return;

            m_itemIcon.sprite = item.icon;
        }
    }
}