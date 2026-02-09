using DChild.Gameplay.Combat;
using DChild.Gameplay.Quests;
using Holysoft.Collections;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHauntingInstanceManager : MonoBehaviour
{
    [SerializeField]
    private ForceQuestUpdateHandle m_forceQuestUpdateHandle;
    [SerializeField, VariablePopup(true)]
    private string m_connectedVariable;
    [SerializeField, VariablePopup(true)]
    private string m_currentVariableTotal;
    [SerializeField, VariablePopup(true)]
    private string m_connectedTotalVariable;
    [SerializeField]
    private DialogueSystemTrigger m_questStartDialogueSystemTrigger;
    [SerializeField]
    private DialogueSystemTrigger m_questEndDialogueSystemTrigger;
    [SerializeField]
    private Flag instanceTracker;
    [SerializeField]
    private List<Damageable> m_objectDamageable;
    // Start is called before the first frame update

    private void OnObjectKilled(object sender, EventActionArgs eventArgs)
    {
        AddFlag();
    }


    [Button]
    public void AddFlag()
    {
        for (int i = 0; i < m_objectDamageable.Count; i++)
        {
            var currentFlag = (Flag)(1 << i);

            if (!m_objectDamageable[i].isAlive)
            {
                instanceTracker |= currentFlag;
            }
        }

        DialogueLua.SetVariable($"{m_connectedVariable}", (int)instanceTracker);
        DialogueLua.SetVariable($"{m_currentVariableTotal}", DialogueLua.GetVariable($"{m_currentVariableTotal}").AsInt + 1);

        //Set Seeds Quest active if Seeds count is less than 1
        if (DialogueLua.GetVariable($"{m_currentVariableTotal}").AsInt >= 1)
        {
            m_questStartDialogueSystemTrigger.OnUse();
        }

        //Set Seeds Quest as success when dead seeds is equal to total seeds and set Desecrate Statue Quest as active
        if (DialogueLua.GetVariable($"{m_currentVariableTotal}").AsInt >= DialogueLua.GetVariable($"{m_connectedTotalVariable}").AsInt)
        {
            m_questEndDialogueSystemTrigger.OnUse();
        }

        m_forceQuestUpdateHandle.SendQuestUpdate();
    }

    private void Awake()
    {
        for (int i = 0; i < m_objectDamageable.Count; i++)
        {
            m_objectDamageable[i].Destroyed += OnObjectKilled;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < m_objectDamageable.Count; i++)
        {
            m_objectDamageable[i].Destroyed -= OnObjectKilled;
        }
    }

    void Start()
    {
        instanceTracker = (Flag)(DialogueLua.GetVariable(m_connectedVariable).asInt);

        for (int i = 0; i < m_objectDamageable.Count; i++)
        {
            var currentFlag = (Flag)(1 << i);

            if (instanceTracker.HasFlag(currentFlag))
            {
                m_objectDamageable[i].gameObject.SetActive(false);
            }
        }

        DialogueLua.SetVariable(m_connectedVariable, (int)instanceTracker);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
