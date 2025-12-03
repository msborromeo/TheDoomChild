using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.ArmyBattle.Recruitment
{

public class Morden_Recruit_NameChanger : MonoBehaviour
{
    public List<GameObject> _list;
    [SerializeField]
    private DialogueDatabase _database;
    public List<GameObject> _Reference;

    public Transform Holder;

    [Button]
    void SetupForMorden()
    {
        foreach(GameObject pref in _list)
        {
            pref.name = (String)("NPC_Interactable_"+ pref.name.Replace("Quest_Recruit", ""));
            for (int x = 0;x < pref.transform.childCount ;x++)
            {
                GameObject child = pref.transform.GetChild(x).gameObject;
                if(child.name.Contains("QuestEntry"))
                {
                    child.name = "NPC";
                    GameObject childOfChild = child.transform.GetChild(0).gameObject;
                    if (childOfChild.name.Contains("_Base"))
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

    [Button]
    void SetupActor()
    {
        foreach (GameObject pref in _list)
        {
            for (int x = 0; x < pref.transform.childCount; x++)
            {
                GameObject child = pref.transform.GetChild(x).gameObject;

                GameObject childOfChild = child.transform.GetChild(0).gameObject;
                DialogueActor c; 
                childOfChild.TryGetComponent<DialogueActor>(out c);
                if(c!=null)
                {
                    c.actor = pref.name;
                }else
                {
                    string namez = pref.name.Replace("NPC_Interactable_", "");
                    namez = namez.Replace(" Variant","");
                    childOfChild.AddComponent<DialogueActor>().actor = namez;
                }
            }
        }
    }

    [Button]
    void SetTransform()
    {
        
        foreach(GameObject pref in _list)
        {
            foreach(GameObject reference in _Reference)
            {
                var refname = reference.name.Replace("_Base Variant","");
                if (pref.name.Contains(refname))
                {
                    pref.transform.position = reference.transform.position;
                    pref.transform.localScale = reference.transform.localScale;
                    break;
                }
                else
                {
                    pref.transform.position = Vector3.zero;
                }    
            }
        }
    }

    [Button]
    void AddToExtraDatabase()
    {
        foreach(GameObject pref in _list)
        {

            //dialogueDatabase.Add(_database);
        }
    }
    [Button]
    void Getmissing()
    {
        foreach (GameObject pref in _list)
        {
            QuestStateListener listener;
            if(!pref.TryGetComponent<QuestStateListener>(out listener))
            {
                pref.transform.SetParent(Holder);
            }
        }
    }

    [Button]
    void RemoveExtraThings()
    {
        foreach(GameObject pref in _list)
        {
            for (int x = 0; x < pref.transform.childCount; x++)
            {
                GameObject child = pref.transform.GetChild(x).gameObject;
                for(int y = 0; y < child.transform.childCount; y++)
                {
                    GameObject childofchild = child.transform.GetChild(y).gameObject;
                    for(int z = 0; z < childofchild.transform.childCount; z++)
                    {
                        GameObject ccc = childofchild.transform.GetChild(z).gameObject;
                        if (ccc.TryGetComponent<DialogueSystemTrigger>(out var dialogue))
                        {
                            //DestroyImmediate(ccc);
                            ccc.SetActive(false);
                        }
                    }
                    
                }
            }
        }
    }
}

}
