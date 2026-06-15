using UnityEngine;

namespace DChild.Gameplay.UI.CombatArts
{
    [RequireComponent(typeof(CombatArtSelectButton))]
    public class CombatArtSelectRequirements : MonoBehaviour
    {
        [SerializeField]
        private CombatArtSelectButton m_requirement;
        private CombatArtSelectButton m_button;

        public void ValidateButtonState()
        {
            if (HasUnlockedRequired())
            {
                if (m_button.currentState == CombatArtUnlockState.Unlocked)
                    return;

                m_button.SetState(CombatArtUnlockState.Unlockable);
            }

            Reset();
        }

        private bool HasUnlockedRequired()
        {
            return (m_requirement == null || (m_requirement.currentState == CombatArtUnlockState.Unlocked));
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