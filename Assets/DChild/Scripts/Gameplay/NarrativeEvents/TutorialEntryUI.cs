using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace DChild.Gameplay.Narrative
{
    public class TutorialEntryUI : MonoBehaviour
    {
        [SerializeField] private Image m_entryImage;
        [SerializeField] private VideoPlayer m_videoPlayer;
        [SerializeField] private TextMeshProUGUI m_entryDescription;

        public void Display(TutorialEntry info)
        {
            if (info != null)
                Reset();

            m_entryDescription.text = info.instructions;
            switch (info.displayType)
            {
                case TutorialEntry.DisplayType.Image:
                    m_entryImage.sprite = info.attachmentImage;
                    break;
                case TutorialEntry.DisplayType.Video:
                    m_videoPlayer.clip = info.attachmentVideo;
                    m_videoPlayer.isLooping = true;
                    m_videoPlayer.Play();
                    break;
                default:
                    break;
            }
        }

        private void Reset()
        {
            m_entryDescription.text = "";
            m_videoPlayer.clip = null;
            m_entryImage.sprite = null;
        }
    }
}
