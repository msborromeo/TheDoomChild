using DarkTonic.MasterAudio.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.Inputs
{
    [CreateAssetMenu(menuName = "Input Reader")]
    public class InputReader : ScriptableObject, PlayerControls.IUnderworldActions //Implement other interafaces for other control types later slowly
    {
        [SerializeField]
        private PlayerControls m_playerControls;

        private void OnEnable()
        {
            if (m_playerControls == null)
            {
                m_playerControls = new PlayerControls();

                m_playerControls.Underworld.SetCallbacks(this); //add other inputs later
            }

            SetInputModeToUnderworldGameplay(); //eventually change to UI because we expect to start at Main Menu
        }

        #region Input Events
        public event Action<Vector2> Vector2InputEvent;
        public event Action<Vector2> Vector2CancelledInputEvent;
        public event Action JumpEvent;
        public event Action JumpStartedEvent;
        public event Action JumpCancelledEvent;
        public event Action PauseEvent;
        public event Action StoreEvent;
        public event Action DashEvent;
        public event Action LevitateEvent;
        public event Action LevitateStartedEvent;
        public event Action InteractEvent;
        #endregion

        public void SetInputModeToUnderworldGameplay()
        {
            m_playerControls.Underworld.Enable();
            //Disable all other input modes
        }

        #region Underworld Actions
        //Underworld Actions
        public void OnVector2(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
            {
                Vector2InputEvent?.Invoke(context.ReadValue<Vector2>());
            }

            if(context.phase == InputActionPhase.Canceled)
            {
                Vector2CancelledInputEvent?.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Started)
            {
                JumpStartedEvent?.Invoke();
            }

            if(context.phase == InputActionPhase.Performed)
            {
                JumpEvent?.Invoke();
            }

            if(context.phase == InputActionPhase.Canceled)
            {
                JumpCancelledEvent?.Invoke();
            }
        }

        public void OnBlock(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnControllerCursorHorizontalInput(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnControllerCursorVerticalInput(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                DashEvent?.Invoke();
            }
        }

        public void OnGrab(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Started)
            {
                InteractEvent?.Invoke();
            }
        }

        public void OnMouseDelta(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            PauseEvent?.Invoke();
        }

        public void OnQuickItemCycle(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnQuickItemUse(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlash(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnStore(InputAction.CallbackContext context)
        {
            StoreEvent?.Invoke();
        }

        #endregion

        #region Primary Skills
        public void OnShadowMorph(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnWhip(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnProjectileThrow(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnLevitate(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Started)
            {
                LevitateStartedEvent?.Invoke();
            }

            if(context.phase == InputActionPhase.Performed)
            {
                LevitateEvent?.Invoke();
            }
        }
        #endregion

        #region Combat Arts
        //Combat Arts
        public void OnAirSlashCombo(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnBackDiver(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnBarrier(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnReaperHarvest(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSoulFireBlast(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSovereignImpale(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnTeleportingSkull(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnDiagonalSwordDash(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnEdgedFury(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnHellTrident(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnIcarusWings(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnEarthShaker(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnLightningSpear(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}

