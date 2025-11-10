using DChild.Gameplay.EquipmentSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentUI : MonoBehaviour
    {
        [BoxGroup("SAMPLE DATA"), SerializeField] private List<SoulEquipmentItem> m_sampleData;
        [SerializeField] private PlayerSoulEquipmentHandle m_equipmentHandle;
        public PlayerSoulEquipmentHandle equipmentHandle => m_equipmentHandle;

        [SerializeField] private EquipmentSelectionUI m_selectionUI;
        public EquipmentSelectionUI selectionUI => m_selectionUI;

        public void Initialize()
        {
            m_selectionUI.SetEquipmentHandle(m_equipmentHandle);
            m_selectionUI.SetupUI(m_sampleData);
        }
    }
}
