using DChild.Gameplay.Characters.Players;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{
    [System.Serializable]
    public class PrimarySkillUIAlertRecorder : UIAlertRecorder<PrimarySkill>
    {
        [SerializeField]
        private PrimarySkill m_value;

        public PrimarySkillUIAlertRecorder(PrimarySkill value)
        {
            m_value = value;
        }

        public int GetAlertSaveData() => (int)m_value;

        public override bool HasAnyNewNotification()
        {
            return (int)m_value > 0;
        }

        public override bool HasNewNotification(PrimarySkill data) => m_value.HasFlag(data);

        public override bool HasNewNotification(int id)
        {
            throw new System.NotImplementedException();
        }

        public override void RecordNewNotification(PrimarySkill data, bool hasNewInfo = true)
        {
            if (hasNewInfo)
            {
                m_value |= data;
            }
            else
            {
                m_value &= ~data;
            }
        }
    }
}