using DChild.Gameplay.EquipmentSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentUI : MonoBehaviour
    {
        [SerializeField] private EquipmentSelectionUI m_selectionUI;

        public void Initialize()
        {
            m_selectionUI.SetupUI();
        }
    }
}
