using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.Gameplay.UI
{
    [System.Serializable]
    public class InputActionConfiguration
    {
        public enum InputActionType
        {
            NoneComposite,
            Directional,
            Cycle,
            Modifier,
            _Count
        }

        public enum DirectionActionPart
        {
            Up,
            Down,
            Left,
            Right,
            _Count
        }

        public enum CycleActionPart
        {
            Negative,
            Positive,
            _Count
        }

        public enum ModifierPart
        {
            ModifierOne,
            ModifierTwo,
            Binding,
            Modifier,
            _Count
        }

        [SerializeField]
        private InputActionType m_actionType;
        public InputActionType actionType => m_actionType;

        //Directional
        [SerializeField, ShowIf("@m_actionType == InputActionType.Directional")]
        private DirectionActionPart m_directionPart;
        public DirectionActionPart directionPart => m_directionPart;

        //Cycle
        [SerializeField, ShowIf("@m_actionType == InputActionType.Cycle")]
        private CycleActionPart m_cycleActionPart;
        public CycleActionPart cycleActionPart => m_cycleActionPart;

        //Modifier
        [SerializeField, ShowIf("@m_actionType == InputActionType.Modifier")]
        private ModifierPart m_modifierPart;
        public ModifierPart modifierPart => m_modifierPart;

        [SerializeField]
        private InputActionReference m_inputAction;
        public InputActionReference inputAction => m_inputAction;
    }

}
