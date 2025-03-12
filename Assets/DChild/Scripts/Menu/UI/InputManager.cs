using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

[CreateAssetMenu(fileName = "InputManagerButtonPrompt", menuName = "DChild/Debug/InputManager")]
public class InputManager : ScriptableObject
{

    [SerializeField]
    private PlayerControls m_playerControls;

    private CurrentDeviceType m_deviceType = CurrentDeviceType.Keyboard;

    public event Action OnActiveDeviceChange;
    public event Action BindingsChangedEvent;

    private void OnEnable()
    {
        if(m_playerControls == null)
        {
            m_playerControls = new PlayerControls();
            m_playerControls.Enable();

            InputSystem.onActionChange += OnActionDeviceChange;
        }
    }
    private void OnDisable()
    {
        InputSystem.onActionChange -= OnActionDeviceChange;
    }

    public CurrentDeviceType GetCurrentDevice() 
    {
        return m_deviceType;
    }

    private void OnActionDeviceChange(object arg1, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            InputAction inputAction = (InputAction)arg1;
            InputControl inputControl = inputAction.activeControl;


            var newDevice = CurrentDeviceType.Keyboard;

            if (inputControl.device is Keyboard)
            {
                newDevice = CurrentDeviceType.Keyboard;

            }

            if(inputControl.device is Gamepad)
            {
                

                if (inputControl.device is DualShockGamepad)
                {
                    newDevice = CurrentDeviceType.PS4;

                }
                else
                {
                    newDevice = CurrentDeviceType.Gamepad;
                }
            }

            if (m_deviceType != newDevice)
            {
                m_deviceType = newDevice;
                OnActiveDeviceChange?.Invoke();
            }

        }

        if (change == InputActionChange.BoundControlsChanged)
        {
            BindingsChangedEvent?.Invoke();
        }
    }

    public PlayerControls GetPlayerInput()
    {
        return m_playerControls;
    }

    public InputAction GetAction(string actionName)
    {
        return m_playerControls.FindAction(actionName);
    }

    public InputBinding GetBinding(string actionName, CurrentDeviceType deviceType)
    {
        InputAction action = GetAction(actionName);

        InputBinding deviceBinding = action.bindings[(int)deviceType];
        return deviceBinding;
    }

}
