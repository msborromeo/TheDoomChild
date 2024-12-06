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
        private FastTravelData[] m_datas;

        public Location location => m_location;
        public int count => m_datas.Length;
        public FastTravelData GetData(int index) => m_datas[index];
    }
}
