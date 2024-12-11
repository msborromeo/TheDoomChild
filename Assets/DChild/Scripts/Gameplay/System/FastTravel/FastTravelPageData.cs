using DChild.Gameplay.Environment;
using UnityEngine;

namespace DChild.Gameplay.FastTravel
{
    [CreateAssetMenu(fileName = "FastTravelPageData", menuName = "DChild/Gameplay/Fast Travel/FastTravel Page Data")]
    public class FastTravelPageData : ScriptableObject
    {
        [SerializeField]
        private Location m_location;
        [SerializeField] 
        private Sprite m_tabIcon;
        [SerializeField]
        private FastTravelData[] m_underworldTravelDatas;
        [SerializeField]
        private FastTravelData m_overworldTravelData;

        public Location location => m_location;
        public Sprite tabIcon => m_tabIcon;
        public FastTravelData overworldTravelData => m_overworldTravelData;
        public int count => m_underworldTravelDatas.Length;
        public FastTravelData GetUnderworldTravelData(int index) => m_underworldTravelDatas[index];
    }
}
