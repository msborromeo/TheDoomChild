using Doozy.Runtime.UIManager;
using UnityEngine;
using Doozy.Runtime.UIManager.Components;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class SpecialSkillNavigationToggleUI : MonoBehaviour
    {
        [SerializeField]
        private UIButton m_prevButton;
        [SerializeField]
        private UIButton m_specialPrevButton;
        [SerializeField]
        private UIButton m_nextButton;
        [SerializeField]
        private UIButton m_specialNextButton;

        public void ToggleSpecialUnitNavigation(bool value)
        {
            m_prevButton.gameObject.SetActive(!value);
            m_nextButton.gameObject.SetActive(!value);

            m_specialPrevButton.gameObject.SetActive(value);
            m_specialNextButton.gameObject.SetActive(value);
        }
    }
}
