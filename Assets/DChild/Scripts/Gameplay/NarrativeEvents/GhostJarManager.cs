using DChild.Gameplay.Combat;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GhostJarManager : MonoBehaviour
{
    [SerializeField]
    private int m_destroyedCounter;
    [SerializeField]
    private int m_totalDestroyed;
    [SerializeField]
    private Damageable m_entity;
    [SerializeField]
    private UnityEvent m_afterfinaljar;
    [SerializeField]
    private UnityEvent m_Firstjar;
    [SerializeField, VariablePopup(true)]
    private string m_jarCounterDatabaseVariable;
    // Start is called before the first frame update
    private void OnEntityDestroyed(object sender, EventActionArgs eventArgs)
    {
        m_destroyedCounter++;
        DialogueLua.SetVariable(m_jarCounterDatabaseVariable, m_destroyedCounter);
        if (m_destroyedCounter == m_totalDestroyed)
        {
            m_afterfinaljar?.Invoke();
            return;
        }
        if(m_destroyedCounter==1)
        { 
            m_Firstjar?.Invoke();
        }

    }
    void Start()
    {
        m_entity.Destroyed += OnEntityDestroyed;
        m_destroyedCounter = DialogueLua.GetVariable(m_jarCounterDatabaseVariable).asInt;
    }

   
}
