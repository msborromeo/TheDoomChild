using DChild.Codex.Tutorial;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.NavigationMap.MapLegend
{
    public class MapLegendUI : MonoBehaviour
    {

        [SerializeField] private MapLegendListUI m_listUI;
        public MapLegendListUI listUI => m_listUI;
        
        [SerializeField] private MapLegendBulletUIHandle m_bulletHandle;
        public MapLegendBulletUIHandle bulletHandle=> m_bulletHandle;

        [Button]
        public void Initialize()
        {
            m_bulletHandle.SetupBullets();
        }
    }
}
