using UnityEngine;
using UnityEngine.Video;

namespace DChild.Gameplay.Narrative
{
    [CreateAssetMenu(fileName = "TutorialEntryData", menuName = "DChild/Gameplay/Narrative/Tutorial/Entry Data")]

    public class TutorialEntry : ScriptableObject
    {
        [SerializeField] private VideoClip m_attachmentVideo;
        public VideoClip attachmentVideo => m_attachmentVideo;
        [SerializeField] private Sprite m_attachmentImage;
        public Sprite attachmentImage => m_attachmentImage;

        [SerializeField, TextArea]
        private string m_instructions;
        public string instructions => m_instructions;

    }
}
