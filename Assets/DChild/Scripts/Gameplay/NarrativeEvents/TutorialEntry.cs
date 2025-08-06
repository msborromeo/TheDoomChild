using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Video;

namespace DChild.Gameplay.Narrative
{
    [CreateAssetMenu(fileName = "TutorialEntryData", menuName = "DChild/Gameplay/Narrative/Tutorial/Entry Data")]

    public class TutorialEntry : ScriptableObject
    {
        public enum DisplayType
        { Image, Video }
        
        [SerializeField] private DisplayType m_displayType;
        public DisplayType displayType => m_displayType;

        [SerializeField, ShowIf("@m_displayType == DisplayType.Video")] private VideoClip m_attachmentVideo;
        public VideoClip attachmentVideo => m_attachmentVideo;
        [SerializeField, ShowIf("@m_displayType == DisplayType.Image")] private Sprite m_attachmentImage;
        public Sprite attachmentImage => m_attachmentImage;

        [SerializeField, TextArea]
        private string m_instructions;
        public string instructions => m_instructions;

    }
}
