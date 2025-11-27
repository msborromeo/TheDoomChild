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

        [BoxGroup("GRID SELECTION"),SerializeField] private EquipmentSelectionUI m_selectionUI;
        public EquipmentSelectionUI selectionUI => m_selectionUI;

        [BoxGroup("DETAILS"), SerializeField] private EquipmentDetailsUI m_detailsUI;
        public EquipmentDetailsUI detailsUI => m_detailsUI;

        public void Initialize()
        {
            m_selectionUI.SetupUI(m_sampleData);
        }
    }
}
