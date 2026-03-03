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
            if (AreRequiredArtsUnlocked())
            {
                if (m_button.currentState != CombatArtUnlockState.Unlocked)
                {
                    m_button.SetState(CombatArtUnlockState.Unlockable);
                }
            }
            else
            {
                m_button.SetState(CombatArtUnlockState.Locked);
            }
            m_button.ForceVisualSync();
        }

        private bool AreRequiredArtsUnlocked()
        {
            return (m_requirement == null || ( m_requirement.currentState == CombatArtUnlockState.Unlocked));
        }

        private void Awake()
        {
            m_button = GetComponent<CombatArtSelectButton>();
        }
    }

}