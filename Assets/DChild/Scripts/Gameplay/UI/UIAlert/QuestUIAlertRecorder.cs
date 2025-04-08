using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{
    public class QuestUIAlertRecorder : UIAlertRecorder<Quest>
    {
        [SerializeField]
        private List<string> m_recordedItems;
        [SerializeField]
        private List<string> m_alerts;

        public QuestUIAlertRecorder(string[] recordedItems, string[] alerts)
        {
            m_recordedItems = new List<string>();
            if (recordedItems != null)
            {
                m_recordedItems.AddRange(recordedItems);
            }
            m_alerts = new List<string>();
            if (alerts != null)
            {
                m_alerts.AddRange(alerts);
            }
        }
        public string[] GetRecordedItems() => m_recordedItems.ToArray();

        public string[] GetAlertSaveData() => m_alerts.ToArray();
        public override bool HasAnyNewNotification()
        {
            return m_alerts.Count > 0;
        }

        public override bool HasNewNotification(int id)
        {
            throw new System.NotImplementedException();
        }

        public override bool HasNewNotification(Quest data)
        {
            return m_alerts.Contains(data.name);
        }

        public override void RecordNewNotification(Quest data, bool hasNewInfo = true)
        {
            var id = data.name;
            if (!hasNewInfo)
            {
                m_alerts.Remove(id);
                return;
            }

            if (m_recordedItems.Contains(id))
                return;

            m_recordedItems.Add(id);
            m_alerts.Add(id);

        }
    }
}