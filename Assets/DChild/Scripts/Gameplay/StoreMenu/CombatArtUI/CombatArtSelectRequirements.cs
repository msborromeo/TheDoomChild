using Doozy.Runtime.Reactor;
using UnityEngine;

namespace DChild.Gameplay.UI.CombatArts
{
    [RequireComponent(typeof(CombatArtSelectButton))]
    public class CombatArtSelectRequirements : MonoBehaviour
    {
        [SerializeField] private CombatArtUISelectableProgressor m_branchProgressors;
        [SerializeField] private CombatArtSelectButton m_requirement;
        private CombatArtSelectButton m_button;

        public void ValidateButtonState(Characters.Players.CombatArts progression)
        {
            m_branchProgressors.DisplayProgress(0f);

            if (m_requirement != null)
            {
                bool metRequiredArt = HasUnlockedCombatArt(progression, m_requirement);
                bool metRequiredLevel = progression.GetAbilityLevel(m_requirement.skillUnlock) >= m_requirement.unlockLevel;
                
                if (!metRequiredArt || !metRequiredLevel)
                {
                    m_button.SetState(CombatArtUnlockState.Locked);
                    return;
                }
            }

            if (progression.GetAbilityLevel(m_button.skillUnlock) < m_button.unlockLevel)
            {
                m_button.SetState(CombatArtUnlockState.Unlockable);
                return;
            }

            m_branchProgressors.DisplayProgress(1f);
            m_button.SetState(CombatArtUnlockState.Unlocked);
        }

        private bool HasUnlockedCombatArt(Characters.Players.CombatArts progression, CombatArtSelectButton combatArtButton)
        {
            return progression.IsAbilityActivated(combatArtButton.skillUnlock);
        }

        private void Awake() => m_button = GetComponent<CombatArtSelectButton>();
    }

}