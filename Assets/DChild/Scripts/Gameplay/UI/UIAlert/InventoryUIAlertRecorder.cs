using DChild.Gameplay.Items;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{
    public class InventoryUIAlertRecorder : UIAlertRecorder<ItemData>
    {
        [SerializeField]
        private List<int> m_recordedItems;
        [SerializeField]
        private List<int> m_alerts;

        public InventoryUIAlertRecorder(int[] recordedItems, int[] alerts)
        {
            m_recordedItems = new List<int>();
            if (recordedItems != null)
            {
                m_recordedItems.AddRange(recordedItems);
            }
            m_alerts = new List<int>();
            if (alerts != null)
            {
                m_alerts.AddRange(alerts);
            }
        }

        public int[] GetRecordedItems() => m_recordedItems.ToArray();

        public int[] GetAlertSaveData() => m_alerts.ToArray();

        public override bool HasAnyNewNotification()
        {
            return m_alerts.Count > 0;
        }

        public override bool HasNewNotification(ItemData data)
        {
            return m_alerts.Contains(data.id);
        }

        public override void RecordNewNotification(ItemData data, bool hasNewInfo = true)
        {
            var id = data.id;
            if (hasNewInfo)
            {
                if (m_recordedItems.Contains(id))
                    return;

                m_recordedItems.Add(id);
                m_alerts.Add(id);
            }
            else
            {
                m_alerts.Remove(id);
            }
        }

        public void RecordNewNotification(ItemData[] datas, bool hasNewInfo = true)
        {
            for (int i = 0; i < datas.Length; i++)
            {
                RecordNewNotification(datas[i], hasNewInfo);
            }
        }
    }
}