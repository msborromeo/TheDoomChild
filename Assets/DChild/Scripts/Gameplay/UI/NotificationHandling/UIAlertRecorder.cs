using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{
    [System.Serializable]
    public class UIAlertRecorder<T> where T : DatabaseAsset
    {
        [SerializeField]
        private List<int> m_alerts;

        public UIAlertRecorder(int[] alerts)
        {
            m_alerts = new List<int>();
            m_alerts.AddRange(alerts);
        }

        public int[] GetAlertSaveData() => m_alerts.ToArray();
        public void RecordNewNotification(T data, bool hasNewInfo = true)
        {
            RecordNewNotification(m_alerts, data.id, hasNewInfo);
        }

        public void RecordNewNotification(int ID, bool hasNewInfo = true)
        {
            RecordNewNotification(m_alerts, ID, hasNewInfo);
        }

        public bool HasNewNotification(T data)
        {
            return m_alerts.Contains(data.id);
        }

        public bool HasAnyNewNotification() => m_alerts.Count > 0;

        private void RecordNewNotification(List<int> record, int id, bool hasNewInfo = true)
        {
            if (hasNewInfo == false)
            {
                record.Remove(id);
                return;
            }

            if (record.Contains(id))
                return;

            record.Add(id);
        }
    }
}