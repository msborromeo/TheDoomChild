using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Systems;
using DChild.Gameplay.Systems.Serialization;
using DChild.Menu;
using Doozy.Runtime.UIManager.Components;
using UnityEditor;
using UnityEngine;

namespace DChild.Gameplay.FastTravel
{
    public class FastTravelLocationTab : MonoBehaviour
    {
        [SerializeField]
        private FastTravelPageData m_locationList;
        public FastTravelPageData locationList => m_locationList;        
    }
}