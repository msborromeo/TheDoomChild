using Holysoft.Event;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentDetailsUI : MonoBehaviour
    {
        [BoxGroup("INFO"), SerializeField] private Image m_highlightedItem;
        [BoxGroup("INFO"), SerializeField] private TextMeshProUGUI m_itemNameLabel;

        [BoxGroup("STATS"), SerializeField] private GameObject m_statRow;
        [BoxGroup("SKILL BONUS"),SerializeField] private TextMeshProUGUI m_descriptionLabel;

        [Button]
        public void UpdateUI()
        {
            var arr = new int[3];

            for (int i = 0; i < arr.Length; i++)
            {
            var gameobject = Instantiate(m_statRow, m_statRow.transform.parent);
                gameobject.transform.localPosition = m_statRow.transform.localPosition;
                gameobject.transform.localRotation = m_statRow.transform.localRotation;
                gameobject.transform.localScale = m_statRow.transform.localScale;
            }

        }

        private void SetStatEntryData()
        {

        }

        private void Reset()
        {

        }
    }
}
