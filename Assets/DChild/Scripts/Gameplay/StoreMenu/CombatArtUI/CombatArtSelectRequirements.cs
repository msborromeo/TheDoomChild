using UnityEngine;

namespace DChild.Gameplay.UI.CombatArts
{
    [RequireComponent(typeof(CombatArtSelectButton))]
    public class CombatArtSelectRequirements : MonoBehaviour
    {
        [SerializeField]
        private CombatArtSelectButton m_requirement;
        private CombatArtSelectButton m_button;

        public void ValidateButtonState(Characters.Players.CombatArts progression)
        {
            if (!HasUnlockedRequired())
            {
                Reset();
                return;
            }

            var unlockState = HasUnlockedCombatArt(progression)
                ? CombatArtUnlockState.Unlocked
                : CombatArtUnlockState.Unlockable;

            m_button.SetState(unlockState);
        }

        private bool HasUnlockedRequired()
        {
            return m_requirement == null || m_requirement.currentState == CombatArtUnlockState.Unlocked;
        }

        private bool HasUnlockedCombatArt(Characters.Players.CombatArts progression)
        {
            //check if player has already activated combat art AND player's ability level meets required level
            return progression.IsAbilityActivated(m_button.skillUnlock) && progression.GetAbilityLevel(m_button.skillUnlock) >= m_button.unlockLevel;
        }

        private void Awake()
        {
            m_button = GetComponent<CombatArtSelectButton>();
        }

        private void Reset()
        {
            m_button.SetState(CombatArtUnlockState.Locked);
        }

    }

}