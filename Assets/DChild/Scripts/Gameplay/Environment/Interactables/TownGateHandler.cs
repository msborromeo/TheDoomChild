using DChild.Gameplay;
using DChild.Gameplay.Characters;
using DChild.Gameplay.Environment;
using DChild.Gameplay.Environment.Interractables;
using DChild.Gameplay.FastTravel;
using DChild.Gameplay.Systems;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.Gameplay.Environment.Interractables
{

    public class TownGateHandler : MonoBehaviour, IButtonToInteract, IInteractionRequirement
    {
        [SerializeField, VariablePopup(true)]
        private string m_serializationReference;
        [SerializeField, TabGroup("Reference")]
        private SpineRootAnimation m_animation;
        [SerializeField, TabGroup("Reference")]
        private LocationPoster m_poster;
        [SerializeField, TabGroup("Reference")]
        private SoulEssenceOffering m_soulOffering;
        [SerializeField]
        private bool m_animationFinished;

        [SerializeField, Spine.Unity.SpineAnimation, TabGroup("Animation")]
        private string m_closeIdle;
        [SerializeField, Spine.Unity.SpineAnimation, TabGroup("Animation")]
        private string m_openTransition;
        [SerializeField, Spine.Unity.SpineAnimation, TabGroup("Animation")]
        private string m_openIdle;
        [SerializeField]
        public Vector3 m_Offset;
        [SerializeField]
        public FastTravelData m_UIData;



        public event EventAction<EventActionArgs> InteractionOptionChange;

        public bool showPrompt => true;

        public string promptMessage => IsCurrentActiveState() ? "Town Portal" : m_soulOffering.promptMessage;

        public Vector3 promptPosition => transform.position + m_Offset;

        public string requirementMessage => m_soulOffering.requirementMessage;

        private void Start()
        {
            if (IsCurrentActiveState())
            {
                m_animation.SetAnimation(0, m_openIdle, true);
            }
            else
            {
                IdlePortal();
            }
        }

        private bool IsCurrentActiveState() => DialogueLua.GetVariable(m_serializationReference).asBool;

        public bool CanBeInteracted(Character character) => m_soulOffering.CanBeInteracted(character);

        [Button, HideInEditorMode]
        public void NearPortal()
        {
            if (IsCurrentActiveState() == false)
                return;

            m_animation.SetAnimation(0, m_openTransition, false);
            m_animation.AddAnimation(0, m_openIdle, true, 0);
        }
        [Button, HideInEditorMode]
        public void IdlePortal()
        {
            m_animation.SetAnimation(0, m_closeIdle, false);
        }

        private IEnumerator OpenAnimationRoutine()
        {
            m_animation.SetAnimation(0, m_openTransition, false);
            //yield return new WaitForSeconds(1.6f);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_openTransition);
            //m_animation.AddAnimation(0, m_openIdle, true, 0);
            InteractionOptionChange?.Invoke(this, EventActionArgs.Empty);
            //GameplaySystem.gamplayUIHandle.OpenFastTravel(m_poster.data.location);
            m_animationFinished = true;
            yield return null;
        }
        [Button, HideInEditorMode]
        public void Interact(Character character)
        {
            var wasPreviouslyActive = IsCurrentActiveState();
            DialogueLua.SetVariable(m_serializationReference, true);
            if (wasPreviouslyActive == false)
            {
                m_soulOffering.Interact(character);
                StartCoroutine(OpenAnimationRoutine());
              
            }
            if(wasPreviouslyActive == true)
            {
                GameplaySystem.gamplayUIHandle.OpenFastTravel(m_poster.data.location, m_UIData);
            }
            NearPortal();

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(promptPosition, 1f);
        }
    }

}
