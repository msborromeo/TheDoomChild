using DChild.Gameplay.Characters.Players.SoulSkills;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.SoulSkills.UI
{
    public sealed class AvailableSoulSkillUI : SoulSkillUI
    {
        [SerializeField]
        private GameObject m_shownVersion;
        [SerializeField]
        private GameObject m_hiddenVersion;

        public override void SetIsAnActivatedUIState(bool isAnEquippedUI)
        {
            if (m_button == null)
            {
                Awake();
            }

            m_hiddenVersion.SetActive(false);
            m_button.interactable = !isAnEquippedUI;
            base.SetIsAnActivatedUIState(isAnEquippedUI);
        }

 
    }
}
