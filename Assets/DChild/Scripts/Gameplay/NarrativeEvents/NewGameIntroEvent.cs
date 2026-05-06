using Cinemachine;
using DChild.Gameplay.Systems;
using DChild.Serialization;
using Doozy.Runtime.UIManager.Containers;
using PixelCrushers.DialogueSystem;
using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace DChild.Gameplay.Narrative
{
    public class NewGameIntroEvent : MonoBehaviour, ISerializableComponent
    {

        [System.Serializable]
        public struct SaveData : ISaveData
        {
            [SerializeField]
            private bool m_isDone;

            public SaveData(bool isDone)
            {
                m_isDone = isDone;
            }

            public bool isDone => m_isDone;

            public ISaveData ProduceCopy() => new SaveData(m_isDone);

        }

        [SerializeField]
        private Transform m_playerStartPosition;
        [SerializeField]
        private CinemachineVirtualCamera m_cameraToDisable;
        [SerializeField]
        private UIContainer m_wakeUpPrompt;
        [SerializeField]
        private AnimationReferenceAsset m_playerStandAnimation;
        [SerializeField]
        private AnimationReferenceAsset m_playerLyingDownAnimation;
        [SerializeField]
        private DialogueSystemTrigger m_afterWakeupDialogue;
        [SerializeField]
        private GameObject m_storePickupSequence;
        [SerializeField]
        private ExtraDatabases m_database;
        [SerializeField]
        private InputActionReference m_wakeUpInput;
        [SerializeField]
        private UnityEvent m_introStartEvent;


        private PlayerInput m_playerInput;

        private bool m_isDone;
        bool hasPressedPrompt = false;

        public static event Action NewGameIntroStarted;
        public static event Action NewGamePlayerWokeUp;
        public static event Action NewGameIntroPromptPressed;
        public static event Action NewGameIntroFinished;

        private void OnInputPerformed(InputAction.CallbackContext context)
        {
            hasPressedPrompt = true;
        }

        public ISaveData Save()
        {
            return new SaveData(m_isDone);
        }

        public void Load(ISaveData data)
        {
            m_isDone = ((SaveData)data).isDone;
            if (m_isDone == false)
            {
                m_introStartEvent?.Invoke();
            }
        }

        public void Initialize()
        {
            m_database.OnUse();
            m_storePickupSequence.SetActive(false);
            var WorldTypeThigy = FindObjectOfType<WorldTypeManager>();
            WorldTypeThigy.SetCurrentWorldType(Environment.Location.City_Of_The_Dead);
           // GameplaySystem.playerManager.player.GetComponentInChildren<PlayerInput>().actions.FindActionMap("Gameplay").Disable();
            m_introStartEvent?.Invoke();
            NewGameIntroStarted?.Invoke();
        }

        public void TransferPlayerToStartPosition()
        {
            var player = GameplaySystem.playerManager.player.character;
            player.transform.position = m_playerStartPosition.position;

            var skeleton = GameplaySystem.playerManager.player.character.GetComponentInChildren<SkeletonAnimation>();
            var lyingDownAnimation = skeleton.state.SetAnimation(0, m_playerLyingDownAnimation, true);
        }

        public void PromptPlayerToStand()
        {
            StopAllCoroutines();
            StartCoroutine(PromptPlayerToStandRoutine());
        }

        public void SetStorePickupSequence(bool startSequence)
        {
            m_storePickupSequence.SetActive(startSequence);
        }

        public void ForceCinematicState()
        {
            BaseGameplaySystem.gamplayUIHandle.ToggleCinematicMode(true);
        }

        public void InvokeGameIntroFinished()
        {
            NewGameIntroFinished?.Invoke();
        }

        public void InvokePlayerWokeUp()
        {
            NewGamePlayerWokeUp?.Invoke();
        }

        public void ToggleCinematicControls(bool value)
        {
            BaseGameplaySystem.ToggleCinematicControls(value);
        }

        public void EndEvent()
        {
            m_isDone = true;
        }

        private IEnumerator PromptPlayerToStandRoutine()
        {
            GameplaySystem.playerManager.OverrideCharacterControls();
            var skeleton = GameplaySystem.playerManager.player.character.GetComponentInChildren<SkeletonAnimation>();
            yield return null;
            yield return GameplaySystem.playerManager.PlayerActionChange(PlayerInputFindActionMap);
            //GameplaySystem.playerManager.player.GetComponentInChildren<PlayerInput>().actions.FindActionMap("Gameplay").Enable();
            m_wakeUpPrompt.Show();

            yield return WakeupPromptRoutine();

            var standAnimation = skeleton.state.SetAnimation(0, m_playerStandAnimation, false);

            yield return new WaitForSeconds(m_playerStandAnimation.Animation.Duration);

            Debug.Log("Wake Up Animation Completed");
            GameplaySystem.playerManager.StopCharacterControlOverride();
            Debug.Log("Returned Player Controlls");
            m_afterWakeupDialogue.OnUse();
            Debug.Log("Wake Up Dialogue Initialized");
            SetStorePickupSequence(true);
            yield return null;
        }

        private IEnumerator WakeupPromptRoutine()
        {
            hasPressedPrompt = false;
            while (hasPressedPrompt == false)
            {
                yield return null;
            }

            m_wakeUpInput.action.performed -= OnInputPerformed;
            m_wakeUpPrompt.Hide();
            m_cameraToDisable.enabled = false;
        }

        private void PlayerInputFindActionMap(PlayerInput playerInput)
        {
            var action = playerInput.actions.FindAction(m_wakeUpInput.action.name);
            action.Enable();
            action.performed += OnInputPerformed;
        }

        private void Start()
        {
            
        }
    }

}
