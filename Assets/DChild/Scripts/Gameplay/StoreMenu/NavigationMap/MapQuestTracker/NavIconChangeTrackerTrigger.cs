using DChild.Gameplay.Environment.Interractables;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.QuestHints.ChestMapTracker
{
    public class NavIconChangeTrackerTrigger : MonoBehaviour
    {
        [SerializeField]
        private DialogueSystemTrigger _Trigger;

        [SerializeField]
        private IButtonToInteract _Button;

        private void Awake()
        {
            _Button = GetComponentInParent<IButtonToInteract>();
            _Button.InteractionOptionChange += OnUse;
        }

        private void OnDisable()
        {
            _Button.InteractionOptionChange -= OnUse;
        }

        private void OnUse(object sender, EventActionArgs eventArgs)
        {
            _Trigger.OnUse();
        }
    }
}
