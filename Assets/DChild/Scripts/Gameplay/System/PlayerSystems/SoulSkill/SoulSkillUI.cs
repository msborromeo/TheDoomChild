using DChild.Gameplay.Characters.Players.SoulSkills;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.SoulSkills.UI
{
    public sealed class SoulSkillUI : MonoBehaviour
    {
        [SerializeField, BoxGroup("Soul Skill Info")] private TextMeshProUGUI m_soulName;
        [SerializeField, BoxGroup("Soul Skill Info")] private TextMeshProUGUI m_soulDescription;
        [SerializeField, BoxGroup("Soul Skill Info")] private TextMeshProUGUI m_soulCapacity;

        [SerializeField, BoxGroup("Soul Skill Visuals")] private Image m_icon;
        [SerializeField, BoxGroup("Soul Skill Visuals")] private CanvasGroup m_equippedCG;
        [SerializeField, BoxGroup("Soul Skill Visuals/Current Progress")] private Image[] m_progressBars;

        [SerializeField, BoxGroup("Soul Skill Visuals/Learned Panel")] private Image m_soulFrame;
        [SerializeField, BoxGroup("Soul Skill Visuals/Learned Panel/Assets")] private Sprite m_undiscoveredPanel;
        [SerializeField, BoxGroup("Soul Skill Visuals/Learned Panel/Assets")] private Sprite m_inProgressPanel;
        [SerializeField, BoxGroup("Soul Skill Visuals/Learned Panel/Assets")] private Sprite m_learnedPanel;

        [SerializeField, BoxGroup("Soul Skill Visuals/No Data")] private CanvasGroup m_soulElementsCG;
        [SerializeField, BoxGroup("Soul Skill Visuals/No Data")] private CanvasGroup m_unidscoveredLabelCG;


        private bool m_isActivated;
        public bool isActivated => m_isActivated;

        private int m_soulSkillID;
        public int soulSkillID => m_soulSkillID;

        public event EventAction<SoulSkillUIEventArgs> OnSkillSelected;
        public event EventAction<SoulSkillUIEventArgs> OnSkillEquipped;

        private SoulSkillUIEventArgs m_attachedUIEvent;

        [Button]
        public void Display(SoulSkill soulSkill)
        {
            Reset();
            if (soulSkill == null)
                return;

            m_soulSkillID = soulSkill.id;
            SetUIVisuals(soulSkill);

            SetEventArgs(this);
        }
        private void SetUIVisuals(SoulSkill soulSkill)
        {
            bool isDiscovered = soulSkill != null;

            m_unidscoveredLabelCG.alpha = isDiscovered ? 0f : 1f;
            m_soulElementsCG.alpha = isDiscovered ? 1f : 0f;

            m_soulFrame.sprite = GetCurrentPanel(soulSkill);

            if (!isDiscovered)
                return;

            m_icon.sprite = soulSkill.icon;
            m_soulName.text = soulSkill.name;
            m_soulDescription.text = soulSkill.description;
            m_soulCapacity.text = soulSkill.capacity.ToString();
        }

        private Sprite GetCurrentPanel(SoulSkill soulSkill)
        {
            return soulSkill == null
                ? m_undiscoveredPanel
                : !soulSkill.isFullyLearned
                    ? m_inProgressPanel
                    : m_learnedPanel;
        }

        public void ShowDetails()
        {
            OnSkillSelected?.Invoke(this, m_attachedUIEvent);
        }

        public void EquipSkill()
        {
            OnSkillEquipped?.Invoke(this, m_attachedUIEvent);
            m_equippedCG.alpha = Convert.ToSingle(m_isActivated);
        }

        public void SetAcivatedStatus(bool value) => m_isActivated = value;

        private void SetEventArgs(SoulSkillUI ui)
        {
            m_attachedUIEvent = new SoulSkillUIEventArgs(ui);
        }

        private void Reset()
        {
            m_soulSkillID = -1;
            SetUIVisuals(null);
        }
    }
}
