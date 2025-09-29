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
            go.name = go.name.Replace(" Variant", "");
            Actor x = te.CreateActor(te.GetNextActorID(_database),go.name,false);
            _database.actors.Add(x);
        }
    }

    string GetName(GameObject x)
    {
        //string ActorName;
        x.name = x.name.Replace(" Variant", "");
        string z = ("NPC_Interactable_"+ x.name);
        //ActorName = z.Replace(" Variant","");
        return z;
    }
}
