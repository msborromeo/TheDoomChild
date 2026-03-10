using System;
using UnityEngine;
using UnityEngine.InputSystem.Users;

namespace DChild.Menu.Inputs
{
    public abstract class InputPromptUI : MonoBehaviour
    {
        protected GamepadIconData m_currentIconData;

        protected abstract void UpdateGamepadInputIcons(GamepadIconData iconData);
        protected abstract void UpdateKeyboardInputIcons(GamepadIconData iconData);

        private void UpdateInputIcons(object sender, InputIconChangeEventArgs eventArgs)
        {
            UpdateInputIcons(eventArgs.iconData);
        }

        protected void UpdateInputIcons(GamepadIconData iconData)
        {
            if (InputIconHandle.useGamepad)
            {
                UpdateGamepadInputIcons(iconData);

            }
            else
            {
                UpdateKeyboardInputIcons(iconData);
            }
        }

        private void Start()
        {
            //InputIconHandle.UpdateInputIcons += UpdateInputIcons;
            InputIconHandle.CurrentDeviceTypeChanged += OnDeviceTypeChanged;
            UpdateInputIcons(InputIconHandle.GetCurrentInputIcon());
        }

        private void OnDeviceTypeChanged(CurrentDeviceType type, GamepadIconData data)
        {
            UpdateInputIcons(data);
        }

        private void OnDestroy()
        {
            //InputIconHandle.CurrentDeviceTypeChanged -= OnDeviceTypeChanged;
            InputIconHandle.UpdateInputIcons -= UpdateInputIcons;
        }
    }
}