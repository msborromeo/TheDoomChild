using DChild.Codex.Tutorial;
using DChild.Gameplay.UI;
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
        [SerializeField] private SetTextToTextBox m_inputDescriptionPanel;

        public void Display(TutorialCodexData info)
        {
            Reset();
            if (info == null)
                return;

            m_entryImage.enabled = true;
            m_entryImage.sprite = info.infoImage;

            switch (info.numberOfActions)
            {
                case 0:
                    m_inputDescriptionPanel.SetText(info.description);
                    break;
                case 1:
                    m_inputDescriptionPanel.SetText(info.description, info.actionConfiguration1);
                    break;
                case 2:
                    m_inputDescriptionPanel.SetText(info.description, info.actionConfiguration1, info.actionConfiguration2);
                    break;
                case 3:
                    m_inputDescriptionPanel.SetText(info.description, info.actionConfiguration1, info.actionConfiguration2, info.actionConfiguration3);
                    break;

                default:
                    break;
            }
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
            //m_inputDescriptionPanel.GetComponent<TextMeshProUGUI>().text = "";
            m_inputDescriptionPanel.SetText(string.Empty);

            m_entryImage.sprite = null;
            m_entryImage.enabled = false;

            m_videoPlayer.clip = null;
            m_videoPlayer.enabled = false;
            m_videoTexture.enabled = false;
        }
    }
}
