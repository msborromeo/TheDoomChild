using DChild.Gameplay.Characters.Players.Modules;
using DChild.Inputs;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputToUIBridge : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_actionReference;

    [SerializeField]
    private UIButton m_button;

    private void OneEnabledUIBridge(InputAction.CallbackContext context)
    {
        m_button.Click();
    }

    private void OnEnable()
    {
        m_actionReference.action.performed += OneEnabledUIBridge;
    }
    private void OnDisable()
    {
        m_actionReference.action.performed -= OneEnabledUIBridge;
    }
}
