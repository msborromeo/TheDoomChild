using DChild.Gameplay.EquipmentSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentUI : MonoBehaviour
    {
        [BoxGroup("SAMPLE DATA"), SerializeField] private SoulEquipmentList m_equipmentList;

        [SerializeField] private PlayerSoulEquipmentHandle m_equipmentHandle;
        public PlayerSoulEquipmentHandle equipmentHandle => m_equipmentHandle;

        [BoxGroup("GRID SELECTION"),SerializeField] private EquipmentSelectionUI m_selectionUI;
        public EquipmentSelectionUI selectionUI => m_selectionUI;

        [BoxGroup("DETAILS"), SerializeField] private EquipmentDetailsUI m_detailsUI;
        public EquipmentDetailsUI detailsUI => m_detailsUI;

        private List<SoulEquipmentItem> m_acquiredItems = new();

        private void GetEquipmentData(SoulEquipmentList equipmentList)
        {
            int[] IDs = equipmentList.GetIDs();

            for (int i = 0; i < IDs.Length; i++)
            {
                m_acquiredItems.Add(equipmentList.GetInfo(IDs[i]));
            }
        }

        public void Initialize()
        {
            //get acquired items list from player data
            //m_equipmentList = m_equipmentHandle.GetFullSoulEquipmentList();
            GetEquipmentData(m_equipmentList);

            m_selectionUI.SetupUI(m_acquiredItems);

            m_detailsUI.SetHighlightedEquipment(m_acquiredItems.First());
            m_detailsUI.UpdateUI();
        }
    }
}
