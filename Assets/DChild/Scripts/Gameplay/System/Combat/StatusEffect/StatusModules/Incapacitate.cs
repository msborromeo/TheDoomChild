using DChild.Gameplay.Systems;
using Holysoft.Event;
using System;
using UnityEngine;

namespace DChild.Gameplay.Combat.StatusAilment
{
    public class Incapacitate : IStatusEffectModule
    {
        public IStatusEffectModule GetInstance() => this;

        private IController controller;

        public void Start(Character character)
        {
             controller = character.GetComponent<IController>();
            if (controller != null)
            {
                controller.Disable();
                controller.ControllerStateChange += OnControllerStateChange;
                PlayerManager.PlayerControlsEnabled += OnPlayerControlsEnabled;
            }
        }

        public void Stop(Character character)
        {
            controller = character.GetComponent<IController>();
            if (controller != null)
            {
                controller.ControllerStateChange -= OnControllerStateChange;
                PlayerManager.PlayerControlsEnabled -= OnPlayerControlsEnabled;
                controller.Enable();
            }
        }

        private void OnControllerStateChange(object sender, EventActionArgs<bool> eventArgs)
        {
            if (eventArgs.info == true)
            {
                //Force Disable Controller if something else enables this
                controller = (IController)sender;
                controller.Disable();
            }
        }
        private void OnPlayerControlsEnabled(bool obj)
        {
            if (obj)
            {
                controller.Disable();
            }
        }
    }
}