using DChild.Codex.Tutorial;
using DChild.Gameplay.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Video;

namespace DChild.Gameplay.Narrative
{
    [CreateAssetMenu(fileName = "TutorialData", menuName = "DChild/Gameplay/Narrative/Tutorial Data")]

    public class TutorialData : ScriptableObject
    {
        [SerializeField] private string m_entryTitle;
        [SerializeField] private TutorialCodexData[] m_entrySections;
        public string entryTitle => m_entryTitle;
        public TutorialCodexData[] entrySections => m_entrySections;

        [SerializeField, MinValue(1), MaxValue(4)]
        private int m_numberOfActions = 1;

        [SerializeField]
        private InputActionConfiguration m_actionConfiguration1;
        [SerializeField, ShowIf("@m_numberOfActions > 1")]
        private InputActionConfiguration m_actionConfiguration2;
        [SerializeField, ShowIf("@m_numberOfActions > 2")]
        private InputActionConfiguration m_actionConfiguration3;
        [SerializeField, ShowIf("@m_numberOfActions > 3")]
        private InputActionConfiguration m_actionConfiguration4;
    }
}
