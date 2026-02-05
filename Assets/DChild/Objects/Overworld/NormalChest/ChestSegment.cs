using Sirenix.OdinInspector;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using System;
using Holysoft.Event;
using Holysoft.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif
/*public struct ChestSegmentChangeEvent : IEventActionArgs
{
    public ChestSegmentChangeEvent(string varName, Chest openState)
    {
        this.varName = varName;
        this.openState = openState;
    }

    public string varName { get; }
    public Chest openState { get; }
}*/
public class ChestSegment : MonoBehaviour
{

    [SerializeField, VariablePopup(true), HideInPrefabAssets]
    private string m_varName;
    [SerializeField, ListDrawerSettings(ShowIndexLabels = true, HideRemoveButton = true, HideAddButton = true), TabGroup("Triggers")]
    private ChestTrigger m_chestTrigger;

    /*[SerializeField, DisableInPlayMode, HideInEditorMode, EnumToggleButtons(), TabGroup("State")]
    private Chest m_currentState;*/

    public string varName => m_varName;
    /*public event EventAction<ChestSegmentChangeEvent> SegmentUpdate;*/
    // Start is called before the first frame update
    /*public void SetStateAs(Chest state)
    {
        m_currentState = state;
    }*/
    /*private void OnStateChange(object sender, ChestStateChangeEvent eventArgs)
    {
        var flag = eventArgs.index;
        if (eventArgs.isOpened)
        {
            m_currentState |= flag;
            Save();
        }
        else
        {
            m_currentState &= flag;
        }

        //SegmentUpdate?.Invoke(this, new ChestSegmentChangeEvent(m_varName, m_currentState));
    }*/
    private void Save()
    {
        DialogueLua.SetVariable(m_varName, true);
    }
    /*private void Load()
    {
        DialogueLua.GetVariable(m_varName);
    }*/
    /*public void Save()
    {
        GetComponent<ChestTrigger>().RevealValueChange += RevealValueChange;
        //TriggerValueChanged?.Invoke(this, eventArgs);
    }

    private void RevealValueChange(object sender, ChestStateChangeEvent eventArgs)
    {
        DialogueLua.SetVariable(m_varName, m_currentState);
    }*/
    private void Awake()
    {
        //Load();
        m_chestTrigger.GetComponent<ChestTrigger>().OnChestOpened += OnChestOpened;
        /*Load();
        for (int i = 0; i < m_list.Length; i++)
        {
            var trigger = m_list[i];
            trigger.SetIndex(i);
            trigger.RevealValueChange += OnStateChange;
        }*/
    }

    private void OnDisable()
    {
        m_chestTrigger.GetComponent<ChestTrigger>().OnChestOpened -= OnChestOpened;
    }

    private void OnChestOpened(object sender, EventActionArgs eventArgs)
    {
        Save();
    }
}
