using DChild.Gameplay.Characters.Players;
using DChild.Temp;
using Doozy.Runtime.UIManager.Animators;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using DChild.Localization;
using System;
using System.Collections;

namespace DChild.Gameplay.UI
{
    public class InteractablePrompt : MonoBehaviour ,IPromptLocalizer 
    {
        [SerializeField]
        private InteractableDetector m_detector;
        [SerializeField]
        private RectTransform m_prompt;
        [SerializeField, TabGroup("Valid Prompt")]
        private Canvas m_validPrompt;
        [SerializeField, TabGroup("Valid Prompt")]
        private TextMeshProUGUI m_promptMessage;
        [SerializeField, TabGroup("Invalid Prompt")]
        private Canvas m_invalidPrompt;
        [SerializeField, TabGroup("Invalid Prompt")]
        private TextMeshProUGUI m_requirementMessage;
        private UIContainerUIAnimator m_animator;
        private Vector3 m_showStartPosition;

        private Vector3 m_newPromptPosition;

        public event Action<string> LocalizeText;

        private void OnInteractableDetected(object sender, DetectedInteractableEventArgs eventArgs)
        {
            StopAllCoroutines();
            StartCoroutine(DisplayPromptRoutine(eventArgs));
        }

        private IEnumerator DisplayPromptRoutine(DetectedInteractableEventArgs eventArgs)
        {
            //update visibility state before proceeding below
            yield return new WaitForEndOfFrame();

            GameplaySystem.gamplayUIHandle.ShowInteractionPrompt(false);
            if (eventArgs.interactable?.showPrompt ?? false)
            {
                m_newPromptPosition = eventArgs.interactable.promptPosition;
                MoveToNewPromptPosition();

                var move = m_animator.showAnimation.Move;
                move.fromCustomValue = m_newPromptPosition + m_showStartPosition;
                move.toCustomValue = m_newPromptPosition;

                move.UpdateValues();
                m_animator.showAnimation.UpdateValues();

                m_validPrompt.enabled = eventArgs.showInteractionButton;
                m_invalidPrompt.enabled = !eventArgs.showInteractionButton;
                m_promptMessage.text = eventArgs.message;
                m_requirementMessage.text = eventArgs.message;
                yield return new WaitForEndOfFrame();
                GameplaySystem.gamplayUIHandle.ShowInteractionPrompt(true);
                LocalizeText?.Invoke(eventArgs.message);
            }
        }

        public void MoveToNewPromptPosition()
        {
            m_prompt.transform.position = m_newPromptPosition;
        }

        private void Awake()
        {
            if (m_detector)
            {
                m_detector.InteractableDetected += OnInteractableDetected;
            }
            m_animator = m_prompt?.GetComponent<UIContainerUIAnimator>();
            m_showStartPosition = m_animator.showAnimation.Move.fromCustomValue;
        }

        private void OnDisable()
        {
            m_detector.InteractableDetected -= OnInteractableDetected;
        }
    }
}