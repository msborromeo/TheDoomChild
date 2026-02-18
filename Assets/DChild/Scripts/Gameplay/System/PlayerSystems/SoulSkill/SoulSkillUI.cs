using DChild.Gameplay.Characters.Players.SoulSkills;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.SoulSkills.UI
{
    public class SoulSkillUI : MonoBehaviour
    {
        [SerializeField] protected Image m_icon;
        [SerializeField] protected TextMeshProUGUI m_soulName;
        [SerializeField] protected TextMeshProUGUI m_soulDescription;

        public Sprite soulSkillIcon => m_icon.sprite;

        public int soulSkillID { get; private set; }

        protected UIButton m_button;
        protected bool m_isAnActivatedSoulSkill;

        public bool isAnActivatedSoulSkill => m_isAnActivatedSoulSkill;

        public event EventAction<SoulSkillUIEventArgs> OnSelected;
        public event EventAction<SoulSkillUIEventArgs> OnClick;

        public void DisplayAs(SoulSkill soulSkill)
        {
            if (soulSkill == null)
            {
                soulSkillID = -1;
            }
            else
            {
                soulSkillID = soulSkill.id;
                m_icon.sprite = soulSkill.icon;
            }
        }


        public void CopyUI(SoulSkillUI reference)
        {
            m_isAnActivatedSoulSkill = reference.isAnActivatedSoulSkill;
            soulSkillID = reference.soulSkillID;
            m_icon.sprite = reference.soulSkillIcon;
        }



        public virtual void SetIsAnActivatedUIState(bool isAnEquippedUI)
        {
            m_isAnActivatedSoulSkill = isAnEquippedUI;
        }

        protected virtual void Awake()
        {
            m_button = GetComponent<UIButton>();
        }
    }
}
