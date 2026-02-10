using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Environment;
using DChild.Gameplay.Environment.Interractables;
using DChild.Gameplay.Systems.Serialization;
using DChild.Menu;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using UnityEngine;

namespace DChild.Gameplay.Systems
{
    [RequireComponent(typeof(LocationPoster))]
    public class LocationSwitcher : SerializedMonoBehaviour, IButtonToInteract
    {
        private static Type previousSwtichType;
        private static Action<Character> RemoveInfluenceOfPreviousSwitch;

        [SerializeField]
        private LocationData m_destination;

        [SerializeField]
        private ISwitchHandle m_handle;

        private LocationPoster m_poster;

        public event EventAction<EventActionArgs> InteractionOptionChange;

        public bool showPrompt => m_handle.needsButtonInteraction;

        public Vector3 promptPosition => m_handle.promptPosition;

        public string promptMessage => m_handle.prompMessage;

        // for testing
        public LocationData locationData => m_destination;

        private bool m_AntiMashBarrier = false;
        public void Interact(Character character)
        {
            if (GameSystem.gamePaused == true)
                return;
            GameplaySystem.gamplayUIHandle.TogglePause(false);

            if (m_AntiMashBarrier)
            {
                return;
            }
            if (m_handle.isDebugSwitchHandle)
            {
                m_AntiMashBarrier = true;
                m_handle.DoSceneTransition(character, TransitionType.Enter);
            }
            else
            {
                m_AntiMashBarrier = true;
                StartCoroutine(DoTransition(character, TransitionType.Enter));
            }
        }

        [Button]
        public void ForceActivation()
        {

            if (m_handle.needsButtonInteraction)
            {
                Interact(GameplaySystem.playerManager.player.character);
            }
            else
            {
                GoToDestination(GameplaySystem.playerManager.player.character);
            }
        }

        private IEnumerator DoTransition(Character character, TransitionType type)
        {
            m_handle.DoSceneTransition(character, type);
            if (type == TransitionType.Enter)
            {
                previousSwtichType = m_handle.GetType();
                RemoveInfluenceOfPreviousSwitch = m_handle.RemoveInfluenceFrom;

                GameplaySystem.playerManager.ReturnPlayerToOrginalScene();
                GameplaySystem.campaignSerializer.UpdateData(SerializationScope.Zone);

                yield return new WaitForSeconds(m_handle.transitionDelay);

                m_handle.DoSceneTransition(character, TransitionType.PostEnter);

                LoadingHandle.SetLoadType(LoadingHandle.LoadType.Smart);
                Cache<LoadZoneFunctionHandle> cacheLoadZoneHandle = Cache<LoadZoneFunctionHandle>.Claim();
                //cacheLoadZoneHandle.Value.Initialize(m_destination, character, cacheLoadZoneHandle);

                var WorldTypeVar = FindObjectOfType<WorldTypeManager>();
                if (WorldTypeVar.CurrentWorldType != WorldTypeVar.GetLocationWorldType(m_destination.location))
                {
                    GameplaySystem.campaignSerializer.UpdateData(SerializationScope.Player);
                }
                WorldTypeVar.SetCurrentWorldType(m_destination.location);

                switch (WorldTypeVar.CurrentWorldType)
                {
                    case WorldType.Underworld:
                        cacheLoadZoneHandle.Value.Initialize(m_destination, character, cacheLoadZoneHandle);
                        GameSystem.LoadZone(GameMode.Underworld, locationData.sceneInfo, true, cacheLoadZoneHandle.Value.CallLocationArriveEvent);
                        break;
                    case WorldType.Overworld:
                        cacheLoadZoneHandle.Value.Initialize(m_destination, null, cacheLoadZoneHandle);
                        GameSystem.LoadZone(GameMode.Overworld, locationData.sceneInfo, true, cacheLoadZoneHandle.Value.CallLocationArriveEvent);
                        break;
                    case WorldType.ArmyBattle:
                        cacheLoadZoneHandle.Value.Initialize(m_destination, null, cacheLoadZoneHandle);
                        GameSystem.LoadZone(GameMode.ArmyBattle, locationData.sceneInfo, true, cacheLoadZoneHandle.Value.CallLocationArriveEvent);
                        break;
                }
                GameplaySystem.ClearCaches();
                DialogueManager.SetDialogueSystemInput(false);
            }
            else if (type == TransitionType.Exit)
            {
                //character.transform.position = m_poster.data.position;
                GameplaySystem.gamplayUIHandle.TogglePause(true);

                LoadingHandle.LoadingDone += OnLoadingDone;

                yield return new WaitForSeconds(m_handle.transitionDelay);

                m_handle.DoSceneTransition(character, TransitionType.PostExit);
                var damageable = character.GetComponent<IDamageable>();
                damageable.SetHitboxActive(true);
                character.GetComponent<Rigidbody2D>().WakeUp();
                DialogueManager.SetDialogueSystemInput(true);
            }
        }

        private void OnLoadingDone(object sender, EventActionArgs eventArgs)
        {
            GameplaySystem.playerManager.StopCharacterControlOverride();
            LoadingHandle.LoadingDone -= OnLoadingDone;
        }

        public void GoToDestination(Character character)
        {
            var damageable = character.GetComponent<IDamageable>();
            damageable?.SetHitboxActive(false);

            var controller = GameplaySystem.playerManager.OverrideCharacterControls();
            StartCoroutine(DoTransition(character, TransitionType.Enter));
        }

        public void OnArrival(object sender, CharacterEventArgs eventArgs)
        {
            if (previousSwtichType != m_handle.GetType())
            {
                RemoveInfluenceOfPreviousSwitch?.Invoke(eventArgs.character);
                RemoveInfluenceOfPreviousSwitch = null;
            }

            StartCoroutine(DoTransition(eventArgs.character, TransitionType.Exit));
            Debug.LogError("Exit");
        }

        private void Awake()
        {
            m_poster = GetComponent<LocationPoster>();
            m_poster.data.OnArrival += OnArrival;
            Debug.Log($"{m_poster.name} is Logged", this);
            m_handle.SetLocationDataReference(m_destination);
        }

        private void OnDisable()
        {
            m_poster.data.OnArrival -= OnArrival;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_handle.needsButtonInteraction)
                return;

            if (GameSystem.gamePaused == true)
                return;

            if (!collision.TryGetComponent(out Hitbox hitbox))
                return;


            GameplaySystem.gamplayUIHandle.TogglePause(false);
            Character character = collision.GetComponentInParent<Character>();

            if (character != null)
            {
                GoToDestination(character);
            }
        }

        private void OnDestroy()
        {
            m_poster.data.OnArrival -= OnArrival;
        }

        private void OnDrawGizmosSelected()
        {
            if (showPrompt)
            {
                var position = promptPosition;
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(position, 1f);
            }
        }
    }
}
