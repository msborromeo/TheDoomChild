using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morden_Recruit_NameChanger : MonoBehaviour
{
    public List<GameObject> _list;
    [SerializeField]
    private DialogueDatabase _database;

    [Button]
    void SetupForMorden()
    {
        foreach(GameObject pref in _list)
        {
            pref.name = pref.name.Replace("Quest_Recruit_","NPC_Interactable_");
            for (int x = 0;x < pref.transform.childCount ;x++)
            {
                GameObject child = pref.transform.GetChild(x).gameObject;
                if(child.name.Contains("QuestEntry"))
                {
                    child.name = "Morden_NPC";
                    GameObject childOfChild = child.transform.GetChild(0).gameObject;
                    if (childOfChild.name.Contains("_Recruitable"))
                    {
                        Debug.LogError("AAAA");
                        childOfChild.TryGetComponent<DialogueSystemTrigger>(out DialogueSystemTrigger trigger);
                        if(trigger != null)
                        {
                            trigger.selectedDatabase = _database;
                            trigger.conversation = "New Conversation 1";
                        }
                        DialogueActor c = childOfChild.AddComponent<DialogueActor>();
                        c.actor = pref.name;
                    }
                }
                    
            }
        }
    }
}
