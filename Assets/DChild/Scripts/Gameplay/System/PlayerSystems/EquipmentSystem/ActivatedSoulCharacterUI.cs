using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.SoulSkills.UI
{
    public class ActivatedSoulCharacterUI : SoulSkillButton
    {
        [SerializeField]
        private SoulSlot m_soulSlot;

        public override void Show(bool immidiate)
        {
            if (m_button == null)
            {
                Awake();
            }
            m_button.interactable = true;
        }

        public override void Hide(bool immidiate)
        {
            if (m_button == null)
            {
                Awake();
            }
            m_button.interactable = false;
        }

        protected override void Awake()
        {
            base.Awake();
            m_isAnActivatedSoulSkill = true;
        }
    }
}

