using UnityEngine;

namespace DChild.Gameplay.ArmyBattle
{
    [CreateAssetMenu(fileName = "ArmyOverviewData", menuName = "DChild/Gameplay/Army/Army Overview")]
    public class ArmyOverviewData : ScriptableObject
    {
        [SerializeField]
        private int m_id;
        [SerializeField]
        private string m_name;
        [SerializeField]
        private Sprite m_icon;

        public int ID => m_id;
        public string name => m_name;
        public Sprite icon => m_icon;
    }
}

