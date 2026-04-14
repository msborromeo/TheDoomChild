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
            OnDialogueEndEvent?.Invoke();
            Debug.Log("Dialogue Ended");
        }

        private void OnDialogueStart()
        {
            OnDialogueStartEvent?.Invoke();
            Debug.Log("Dialogue Started");
        }
    }
}

