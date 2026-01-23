using DChild.Gameplay.Characters.Players;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.UI.PrimarySkills
{
    public class PrimarySkillSelectable : MonoBehaviour
    {
        [SerializeField, BoxGroup("Undiscovered Slot")] private GameObject m_undiscoveredSlot;

        [SerializeField]
        private PrimarySkillIcon m_icon;

        private PrimarySkillData m_reference;
        private UIToggle m_toggle;

        public PrimarySkillData reference => m_reference;

        public event Action<PrimarySkillData> OnPrimarySkillDataChanged;
        
        public void SetAsUnlocked(bool isUnlocked)
        {
            m_undiscoveredSlot.SetActive(!isUnlocked);
            m_toggle.interactable = isUnlocked;
        }

        public void SetSelectableFor(PrimarySkillData data)
        {
            m_icon.DisplayAs(data);
            m_reference = data;
            OnPrimarySkillDataChanged?.Invoke(data);
        }

        private void Awake()
        {
            m_toggle = GetComponent<UIToggle>();
        }
    }
}