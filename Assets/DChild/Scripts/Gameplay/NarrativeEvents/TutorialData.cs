using UnityEngine;
using UnityEngine.Video;

namespace DChild.Gameplay.Narrative
{
    [CreateAssetMenu(fileName = "TutorialData", menuName = "DChild/Gameplay/Narrative/Tutorial Data")]

    public class TutorialData : ScriptableObject
    {
        [SerializeField] private string m_entryTitle;
        [SerializeField] private TutorialEntry[] m_entrySections;
        public string entryTitle => m_entryTitle;
        public TutorialEntry[] entrySections => m_entrySections;
    }
}
