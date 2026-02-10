using System;
using System.Collections;
using UnityEngine;

namespace DChild.Gameplay.Combat.StatusAilment.UI
{
    public class StatusEffectUI : MonoBehaviour
    {
        [SerializeField]
        private StatusEffectReciever m_reciever;

        [SerializeField]
        private StatusEffectScreenFilterUI m_screenFilter;
        [SerializeField]
        private StatusEffectIconManager m_iconManager;

        private void OnStatusRecieved(object sender, StatusEffectRecieverEventArgs eventArgs)
        {
            m_screenFilter.ShowFilter(eventArgs.type);
            m_iconManager?.ShowIconFor(eventArgs.type, m_reciever);
        }

        private void OnStatusEnd(object sender, StatusEffectRecieverEventArgs eventArgs)
        {
            m_screenFilter.HideFilter(eventArgs.type);
            m_iconManager?.HideIconFor(eventArgs.type, m_reciever);
        }

        private void Awake()
        {
            m_reciever.StatusRecieved += OnStatusRecieved;
            m_reciever.StatusEnd += OnStatusEnd;

            m_screenFilter.HideFilter(StatusEffectType._COUNT);
            m_iconManager?.HideAllIcons();
        }

        private void OnDisable()
        {
            m_reciever.StatusRecieved -= OnStatusRecieved;
            m_reciever.StatusEnd -= OnStatusEnd;
        }
    }
}