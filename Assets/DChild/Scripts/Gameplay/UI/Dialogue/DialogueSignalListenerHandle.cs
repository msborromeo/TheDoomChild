using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.UI
{
    public class DialogueSignalListenerHandle : MonoBehaviour
    {
        [SerializeField]
        private DialogueCustomSignalListener m_customDialogeSignalListener;

        [SerializeField]
        private UnityEvent OnDialogueStartEvent;

        [SerializeField]
        private UnityEvent OnDialogueEndEvent;

        private void OnEnable()
        {
            m_customDialogeSignalListener.DialogueStart += OnDialogueStart;
            m_customDialogeSignalListener.DialogueEnd += OnDialogueEnd;
        }

        private void OnDisable()
        {
            m_customDialogeSignalListener.DialogueStart -= OnDialogueStart;
            m_customDialogeSignalListener.DialogueEnd -= OnDialogueEnd;
        }

        private void OnDialogueEnd()
        {
            OnDialogueStartEvent?.Invoke();
        }

        private void OnDialogueStart()
        {
            OnDialogueEndEvent?.Invoke();
        }
    }
}

