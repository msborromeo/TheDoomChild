using DarkTonic.MasterAudio.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.Inputs
{
    [CreateAssetMenu(menuName = "Input Reader")]
    public class InputReader : ScriptableObject, PlayerControls.IUnderworldActions, PlayerControls.IOverworldActions, PlayerControls.IUIActions, PlayerControls.IArmyBattleActions
    {
        [SerializeField]
        private PlayerControls m_playerControls;

        private void OnEnable()
        {
            if (m_playerControls == null)
            {
                m_playerControls = new PlayerControls();

                m_playerControls.Underworld.SetCallbacks(this);
                m_playerControls.Overworld.SetCallbacks(this);
                m_playerControls.UI.SetCallbacks(this);
                m_playerControls.ArmyBattle.SetCallbacks(this);
            }

            SetInputModeToUnderworldGameplay(); //eventually change to UI because we expect to start at Main Menu
        }

        #region Input Events
        #region Underworld Input
        public event Action<Vector2> Vector2InputPerformedEvent;
        public event Action<Vector2> Vector2CancelledInputEvent;
        public event Action JumpPerformedEvent;
        public event Action JumpStartedEvent;
        public event Action JumpCancelledEvent;
        public event Action PauseStartedEvent;
        public event Action StoreStartedEvent;
        public event Action DashStartedEvent;
        public event Action LevitatePerformedEvent;
        public event Action LevitateStartedEvent;
        public event Action LevitateCancelledEvent;
        public event Action InteractStartedEvent;
        #endregion
        #region Overworld Input
        public event Action<Vector2> OverworldMovePerformedEvent;
        public event Action<Vector2> OverworldMoveCancelledEvent;
        public event Action<Vector2> OverworldMoveStartedEvent;
        #endregion
        #region UI Input
        public event Action<Vector2> UINavigatePerformedEvent;
        public event Action<Vector2> UINavigateCancelledEvent;
        public event Action<Vector2> UINavigateStartedEvent;
        public event Action UISubmitPerformedEvent;
        public event Action UISubmitCancelledEvent;
        public event Action UISubmitStartedEvent;
        public event Action UICancelPerformedEvent;
        public event Action UICancelCancelledEvent;
        public event Action UICancelStartedEvent;
        public event Action<Vector2> UIPointPerformedEvent;
        public event Action<Vector2> UIPointCancelledEvent;
        public event Action<Vector2> UIPointStartedEvent;
        public event Action<Vector2> UIScrollWheelPerformedEvent;
        public event Action<Vector2> UIScrollWheelCancelledEvent;
        public event Action<Vector2> UIScrollWheelStartedEvent;
        public event Action UIResumePerformedEvent;
        public event Action UIResumeCancelledEvent;
        public event Action UIResumeStartedEvent;
        public event Action UIClickPerformedEvent;
        public event Action UIClickCancelledEvent;
        public event Action UIClickStartedEvent;
        #endregion
        #region Army Battle Input
        public event Action ArmyBattleSelectCommandPerformedEvent;
        public event Action ArmyBattleSelectCommandCancelledEvent;
        public event Action ArmyBattleSelectCommandStartedEvent;
        #endregion
        #endregion

        public void SetInputModeToUnderworldGameplay()
        {
            m_playerControls.Underworld.Enable();
            m_playerControls.Overworld.Disable();
            m_playerControls.UI.Disable();
            m_playerControls.ArmyBattle.Disable();
        }

        public void SetInputModeTOverworldGameplay()
        {
            m_playerControls.Overworld.Enable();
            m_playerControls.Underworld.Disable();
            m_playerControls.UI.Disable();
            m_playerControls.ArmyBattle.Disable();
        }

        public void SetInputModeToUI()
        {
            m_playerControls.UI.Enable();
            m_playerControls.Underworld.Disable();
            m_playerControls.Overworld.Disable();
            m_playerControls.ArmyBattle.Disable();
        }

        public void SetInputModeToArmyBattleGameplay()
        {
            m_playerControls.ArmyBattle.Enable();
            m_playerControls.Underworld.Disable();
            m_playerControls.Overworld.Disable();
            m_playerControls.UI.Disable();
        }

        #region Underworld Actions
        //Underworld Actions
        public void OnVector2(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
            {
                Vector2InputPerformedEvent?.Invoke(context.ReadValue<Vector2>());
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
                JumpPerformedEvent?.Invoke();
            }

            if(context.phase == InputActionPhase.Canceled)
            {
                JumpCancelledEvent?.Invoke();
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                DashStartedEvent?.Invoke();
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
                InteractStartedEvent?.Invoke();
            }
        }

        public void OnMouseDelta(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            PauseStartedEvent?.Invoke();
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
            StoreStartedEvent?.Invoke();
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
                LevitatePerformedEvent?.Invoke();
            }

            if(context.phase == InputActionPhase.Canceled)
            {
                LevitateCancelledEvent?.Invoke();
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

        #region Overworld Controls
        public void OnMove(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Started)
            {
                OverworldMoveStartedEvent?.Invoke(context.ReadValue<Vector2>());
            }

            if (context.phase == InputActionPhase.Performed)
            {
                OverworldMovePerformedEvent?.Invoke(context.ReadValue<Vector2>());
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                OverworldMoveCancelledEvent?.Invoke(context.ReadValue<Vector2>());
            }
        }
        #endregion

        #region UI Controls
        public void OnNavigate(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Started)
            {
                UINavigateStartedEvent?.Invoke(context.ReadValue<Vector2>());
            }

            if (context.phase == InputActionPhase.Performed)
            {
                UINavigatePerformedEvent?.Invoke(context.ReadValue<Vector2>());
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                UINavigateCancelledEvent?.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                UISubmitStartedEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Performed)
            {
                UISubmitPerformedEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                UISubmitCancelledEvent?.Invoke();
            }
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                UICancelStartedEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Performed)
            {
                UICancelPerformedEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                UICancelCancelledEvent?.Invoke();
            }
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                UIPointStartedEvent?.Invoke(context.ReadValue<Vector2>());
            }

            if (context.phase == InputActionPhase.Performed)
            {
                UIPointPerformedEvent?.Invoke(context.ReadValue<Vector2>());
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                UIPointCancelledEvent?.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                UIClickStartedEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Performed)
            {
                UIClickPerformedEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                UIClickCancelledEvent?.Invoke();
            }
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                UIScrollWheelStartedEvent?.Invoke(context.ReadValue<Vector2>());
            }

            if (context.phase == InputActionPhase.Performed)
            {
                UIScrollWheelPerformedEvent?.Invoke(context.ReadValue<Vector2>());
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                UIScrollWheelCancelledEvent?.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                UIResumeStartedEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Performed)
            {
                UIResumePerformedEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                UIResumeCancelledEvent?.Invoke();
            }
        }
        #endregion

        #region Army Battle Controls
        public void OnSelectCommand(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                ArmyBattleSelectCommandStartedEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Performed)
            {
                ArmyBattleSelectCommandPerformedEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                ArmyBattleSelectCommandCancelledEvent?.Invoke();
            }
        }
        #endregion

    }
}

