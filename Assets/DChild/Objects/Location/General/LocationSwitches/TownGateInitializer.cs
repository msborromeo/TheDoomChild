using DChild.Gameplay.FastTravel;
using DChild.Gameplay.Systems;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownGateInitializer : MonoBehaviour
{
    [SerializeField, TabGroup("Reference")]
    private LocationSwitcher m_switcher;
    [SerializeField, TabGroup("Reference")]
    private LocationPoster m_Poster;
    public FastTravelHandle fastTravel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    public void TriggerPortal()
    {
        Debug.Log("Test, On a Portal");
    }
    [Button]
    public void LeavePortal()
    {
        Debug.Log("Test, leaving a portal");
    }

    
}
