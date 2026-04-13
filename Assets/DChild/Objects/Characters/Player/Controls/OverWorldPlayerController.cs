using DChild.Gameplay.Systems;
using DChild.Inputs;
using Holysoft.Event;
using PlayerNew;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.Gameplay.Characters.Players.Modules
{
    public class OverWorldPlayerController : MonoBehaviour, IMainController
    {
        [SerializeField]
        private InputReader m_inputReader;
        [SerializeField]
        private float m_moveSpeed;
        [SerializeField]
        private Rigidbody2D m_rigidbody;
        [SerializeField]
        private PlayerInput m_playerinput;
        [SerializeField]
        private OverworldObjectInteraction m_objectInteraction;
        [SerializeField]
        private PlayerInput m_playerInput;

        private float m_currentSpeed;
        public float horizontalInput;
        public float verticalInput;
        //public bool interactPressed;
        public OverworldCharacterAnimatorHandle m_animationhandler;


        public event EventAction<EventActionArgs> ControllerDisabled;
        public event EventAction<EventActionArgs> ControllerEnabled;
        public static event Action<string> ActiveControllerChanged;

        public void Enable()
        {
            m_inputReader.SetInputModeTOverworldGameplay();
        }

        public void Disable()
        {
            m_inputReader.SetInputModeToUI();
        }

        public void OnEnable()
        {
            m_inputReader.OverworldMovePerformedEvent += OnVector2Input;
            m_inputReader.OverworldMoveCancelledEvent += OnVector2InputCancelled;
            m_inputReader.InteractStartedEvent += OnInteract;
            m_inputReader.StoreStartedEvent += OnStoreStartedInput;
            m_inputReader.PauseStartedEvent += OnPauseStartedInput;
        }

        public void OnDisable()
        {
            m_inputReader.OverworldMovePerformedEvent -= OnVector2Input;
            m_inputReader.OverworldMoveCancelledEvent -= OnVector2InputCancelled;
            m_inputReader.InteractStartedEvent -= OnInteract;
            m_inputReader.StoreStartedEvent -= OnStoreStartedInput;
            m_inputReader.PauseStartedEvent -= OnPauseStartedInput;

        }

        private void OnPauseStartedInput()
        {
            GameplaySystem.gamplayUIHandle.OpenPauseMenu();
        }

        private void OnStoreStartedInput()
        {
            GameplaySystem.gamplayUIHandle.OpenStoreAtPage(StorePage.Map);
        }

        private void OnVector2InputCancelled(Vector2 vector)
        {
            horizontalInput = 0;
            verticalInput = 0;  
        }

        private void OnInteract()
        {
            m_objectInteraction?.Interact();
        }

        private void OnVector2Input(Vector2 vector)
        {
            horizontalInput = vector.x;
            verticalInput = vector.y;
        }

        public void Move(float directionx, float directiony)
        {
            var xVelocity = m_moveSpeed * directionx;
            var yVelocity = m_moveSpeed * directiony;
            m_rigidbody.velocity = new Vector2(xVelocity, yVelocity);
            m_animationhandler.UpdateAnimator(new Vector2(xVelocity, yVelocity));
        }

        private void Awake()
        {
            m_inputReader.SetInputModeTOverworldGameplay();
        }

        void Start()
        {
            m_inputReader.SetInputModeTOverworldGameplay();
        }

        void Update()
        {
            Move(horizontalInput, verticalInput);
        }

        public void OnDeviceTypeChanged()
        {
            ActiveControllerChanged?.Invoke(m_playerInput.currentControlScheme);
        }
    }
}

