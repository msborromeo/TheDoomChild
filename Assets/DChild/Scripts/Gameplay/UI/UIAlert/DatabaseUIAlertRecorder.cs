using System;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{

    [System.Serializable]
    public class DatabaseUIAlertRecorder<T> : UIAlertRecorder<T> where T : DatabaseAsset
    {
        [SerializeField]
        private List<int> m_alerts;

        public DatabaseUIAlertRecorder(int[] alerts)
        {
            m_alerts = new List<int>();
            if (alerts != null)
            {
                m_alerts.AddRange(alerts);
            }
        }

        public int[] GetAlertSaveData() => m_alerts.ToArray();
        public override void RecordNewNotification(T data, bool hasNewInfo = true)
        {
            RecordNewNotification(m_alerts, data.id, hasNewInfo);
        }

        public void RecordNewNotification(int ID, bool hasNewInfo = true)
        {
            RecordNewNotification(m_alerts, ID, hasNewInfo);
        }

        public override bool HasNewNotification(T data)
        {
            return data != null && m_alerts.Contains(data.id) ;
        }
        public override bool HasNewNotification(int id)
        {
            return m_alerts.Contains(id);
        }
        public override bool HasAnyNewNotification() => m_alerts.Count > 0;

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