using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.EquipmentSystem;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DChild.Gameplay.EquipmentSystem.PlayerSoulEquipmentHandle;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentDetailsUI : MonoBehaviour
    {
        [BoxGroup("MAIN UI"), SerializeField] private EquipmentUI m_equipmentUI;

        [BoxGroup("INFO"), SerializeField] private Image m_equipmentIcon;
        [BoxGroup("INFO"), SerializeField] private TextMeshProUGUI m_itemNameLabel;

        [BoxGroup("STATS"), SerializeField] private GameObject m_statRow;
        [BoxGroup("SKILL BONUS"), SerializeField] private TextMeshProUGUI m_bonusLabel;

        private List<GameObject> m_instantiatedRows = new List<GameObject>();
        private SoulEquipment m_highlightedEquipment;

        [BoxGroup("TEST DATA"), HideInPlayMode,SerializeField] private SoulEquipmentItem m_sampleItem;

        public void SetHighlightedEquipment(SoulEquipment value)
        {
            m_highlightedEquipment = value;
        }

        [Button]
        public void UpdateUI()
        {
            Reset();

            var equipment = m_sampleItem.soulEquipment;

            var boostList = equipment.statBoostList;
            
            if (boostList != null)
                ShowStatBuffs(boostList);
        }

        private void ShowStatBuffs(List<IEquipmentStatBoostModule> statBuffs)
        {
            m_statRow.SetActive(false);
            Transform parentTransform = m_statRow.transform.parent;
            for (int i = 0; i < statBuffs.Count; i++)
            {
                var gameobject = Instantiate(m_statRow, parentTransform);
                gameobject.name = $"Row - StatBuff ({i + 1})";
                m_instantiatedRows.Add(gameobject);

                gameobject.GetComponent<EquipmentStatBuffUI>().Display(statBuffs[i]);
            }
        }


        private void SetSkillBonusLabel(SoulSkill soulSkill)
        {
            m_bonusLabel.text = soulSkill.description;
        }

        private void Reset()
        {
            m_statRow.SetActive(true);
            foreach (GameObject row in m_instantiatedRows)
            {
                Destroy(row);
            }
            m_instantiatedRows.Clear();
        }
    }
}
