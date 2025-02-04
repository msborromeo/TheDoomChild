using UnityEngine;

namespace DChild.Gameplay.ArmyBattle
{
    [CreateAssetMenu(fileName = "ArmyOverviewData", menuName = "DChild/Gameplay/Army/Army Overview")]
    public class ArmyOverviewData : ScriptableObject
    {
        [SerializeField]
        private string m_name;
        [SerializeField] 
        private Sprite m_icon;
        [SerializeField]
        private bool m_localizeName;

        public string name => m_name;
        public Sprite icon => m_icon;
        public bool localize => m_localizeName;
    }
}

