using DChild.Codex.Tutorial;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace DChild.Gameplay.Narrative
{
    public class TutorialEntryUI : MonoBehaviour
    {
        [SerializeField] private Image m_entryImage;
        [BoxGroup("VIDEO"), SerializeField] private VideoPlayer m_videoPlayer;
        [BoxGroup("VIDEO"), SerializeField] private RawImage m_videoTexture;
        [SerializeField] private TextMeshProUGUI m_entryDescription;

        public void Display(TutorialCodexData info)
        {
            Reset();
            if (info == null)
                return;


            m_entryDescription.text = info.description;
            m_entryImage.enabled = true;
            m_entryImage.sprite = info.infoImage;

            //m_entryDescription.text = info.instructions;
            //switch (info.displayType)
            //{
            //    case TutorialEntry.DisplayType.Image:
            //        m_entryImage.enabled = true;
            //        m_entryImage.sprite = info.attachmentImage;
            //        break;
            //    case TutorialEntry.DisplayType.Video:
            //        m_videoPlayer.enabled = true;
            //        m_videoTexture.enabled = true;
            //        m_videoPlayer.clip = info.attachmentVideo;
            //        m_videoPlayer.Play();
            //        break;
            //    default:
            //        break;
            //}
        }

        private void Reset()
        {
            m_entryDescription.text = "";

            m_entryImage.sprite = null;
            m_entryImage.enabled = false;

            m_videoPlayer.clip = null;
            m_videoPlayer.enabled = false;
            m_videoTexture.enabled = false;
        }
    }
}
