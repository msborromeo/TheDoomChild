using UnityEngine;

namespace DChild.Gameplay.Combat.StatusAilment.UI
{
    [CreateAssetMenu(fileName = "StatusEffectIconData", menuName = "DChild/Gameplay/Combat/Inflictions/Status Effect Icon Data")]
    public class StatusEffectIconData : ScriptableObject
    {
        [SerializeField]
        private Sprite m_activeIcon;
        [SerializeField]
        private Sprite m_runningDurationIcon;

        public Sprite activeIcon => m_activeIcon;
        public Sprite runningDurationIcon => m_runningDurationIcon;
    }

}