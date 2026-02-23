using DChild.Gameplay.SoulSkills.UI;
using Doozy.Runtime.UIManager.Containers;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DChild.Gameplay.SoulSkills
{
    public class SoulSkillSelection : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_acquiredListUI;
        [SerializeField]
        private RectTransform m_highlight;

        private SoulSkillUI m_currentSelectedSoulSkill;

        public event EventAction<SoulSkillUIEventArgs> OnSelected;
        public event EventAction<SoulSkillUIEventArgs> OnActionRequired;

        private void OnSkillSelected(object sender, SoulSkillUIEventArgs eventArgs)
        {
            var skillUI = eventArgs.soulskillUI;
            if (m_currentSelectedSoulSkill != skillUI)
            {
                m_currentSelectedSoulSkill = skillUI;
                OnSelected?.Invoke(this, eventArgs);
            }
        }

        private void OnSkillEquipped(object sender, SoulSkillUIEventArgs eventArgs)
        {
            OnActionRequired?.Invoke(this, eventArgs);
        }

        public void Reset() => m_currentSelectedSoulSkill = null;

        private void OnEnable()
        {
            var m_acquiredSoulSkillUIList = m_acquiredListUI.GetComponentsInChildren<SoulSkillUI>(true);
            for (int i = 0; i < m_acquiredSoulSkillUIList.Length; i++)
            {
                var soulSkillUI = m_acquiredSoulSkillUIList[i];
                soulSkillUI.OnSkillSelected += OnSkillSelected;
                soulSkillUI.OnSkillEquipped += OnSkillEquipped;
            }
        }

    }
}
