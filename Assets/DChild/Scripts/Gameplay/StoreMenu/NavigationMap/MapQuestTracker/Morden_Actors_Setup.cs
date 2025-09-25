using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;

public class Morden_Actors_Setup : MonoBehaviour
{
    [SerializeField]
    private DialogueDatabase _database;

    public List<GameObject> _list;

    [Button]
    void GenerateActors()
    {   
        Template te = new Template();
        foreach (GameObject go in _list)
        {
            Actor x = te.CreateActor(te.GetNextActorID(_database),GetName(go),false);
            _database.actors.Add(x);
        }
    }

    string GetName(GameObject x)
    {
        string ActorName;
        string z = x.name;
        ActorName = z.Replace("Quest_Recruit_","Morden_");
        return ActorName;
    }
}
