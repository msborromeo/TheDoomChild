using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Listeners;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.UI
{
    public class DialogueCustomSignalListener : SignalListener
    {
        public event Action DialogueStart;
        public event Action DialogueEnd;

        protected override void ProcessSignal(Signal signal)
        {
            base.ProcessSignal(signal);
            if (signal == null)
                return;
            if(signal.TryGetValueType(out System.Type type))
            {
                if(type == typeof(bool))
                {
                    signal.TryGetValue(out bool value);

                    if(value == true)
                    {
                        DialogueStart?.Invoke();
                        Debug.Log("Dialogue Start Event called");
                    }
                    
                    if(value == false)
                    {
                        DialogueEnd?.Invoke();
                        Debug.Log("Dialogue End Event called");
                    }
                }
            }
        }
    }
}

