using UnityEngine;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class SpecialSkillNavigationToggleUI : MonoBehaviour
    {
        [SerializeField, BoxGroup("NORMAL")]
        private UIButton m_prevButton;
        [SerializeField, BoxGroup("NORMAL")]
        private UIButton m_nextButton;

        [SerializeField, BoxGroup("SPECIAL")]
        private UIButton m_specialPrevButton;
        [SerializeField, BoxGroup("SPECIAL")]
        private UIButton m_specialNextButton;


        public void ToggleSpecialUnitNavigation(bool value)
        {
            m_prevButton.gameObject.SetActive(!value);
            m_nextButton.gameObject.SetActive(!value);

            m_specialPrevButton.gameObject.SetActive(value);
            m_specialNextButton.gameObject.SetActive(value);
        }

        public void UpdateMoreGroupButtonNavigation(UIButton moreGroups)
        {
            var newNavigation = moreGroups.navigation;

            if (m_specialPrevButton.isActiveAndEnabled)
            {
                newNavigation.selectOnUp = m_specialPrevButton;
                newNavigation.selectOnDown = m_specialNextButton;
                moreGroups.navigation = newNavigation;
                return;
            }
            newNavigation.selectOnUp = m_prevButton;
            newNavigation.selectOnDown = m_nextButton;
            moreGroups.navigation = newNavigation;
        }
    }
}
