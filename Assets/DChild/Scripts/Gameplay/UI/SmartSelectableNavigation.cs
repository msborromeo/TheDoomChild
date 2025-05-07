using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.UI
{
    public class SmartSelectableNavigation : MonoBehaviour
    {
        private Selectable m_main;

        private Selectable m_upSelection;
        private Selectable m_downSelection;
        private Selectable m_leftSelection;
        private Selectable m_rightSelection;

        [Button]
        public void UpdateSelectionAvailability()
        {
            if (m_main == null)
                return;

            var navigation = m_main.navigation;

            if (m_upSelection)
                navigation.selectOnUp = m_upSelection.interactable ? m_upSelection : null;

            if (m_downSelection)
                navigation.selectOnDown = m_downSelection.interactable ? m_downSelection : null;

            if (m_leftSelection)
                navigation.selectOnLeft = m_leftSelection.interactable ? m_leftSelection : null;

            if (m_rightSelection)
                navigation.selectOnRight = m_rightSelection.interactable ? m_rightSelection : null;

            m_main.navigation = navigation;
        }

        void Awake()
        {
            m_main = GetComponent<Selectable>();

            var navigation = m_main.navigation;
            m_upSelection = navigation.selectOnUp;
            m_downSelection = navigation.selectOnDown;
            m_leftSelection = navigation.selectOnLeft;
            m_rightSelection = navigation.selectOnRight;
        }
    }

}