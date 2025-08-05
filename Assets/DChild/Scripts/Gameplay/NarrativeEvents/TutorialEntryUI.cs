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
            m_entryDescription.text = info.instructions;
            m_entryImage.sprite = info.attachmentImage!= null ? info.attachmentImage : null;

            if(info.attachmentVideo != null)
            {
                m_videoPlayer.clip = info.attachmentVideo;
                m_videoPlayer.isLooping = true;
                m_videoPlayer.Play();
            }
        }
    }
}
