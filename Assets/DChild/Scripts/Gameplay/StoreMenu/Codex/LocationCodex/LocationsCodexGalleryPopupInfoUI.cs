using DChild.Codex.LocationCodex;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace DChild.Menu.Codex.Locations
{
    public class LocationsCodexGalleryPopupInfoUI : CodexGalleryPopupInfoUI<LocationCodexData>
    {
        [SerializeField] private TextMeshProUGUI m_locationName;
        [SerializeField] private Image m_panelBackground;
        [SerializeField] private Image m_locationImage;
        [SerializeField] private TextMeshProUGUI m_description;

        private void SetImage(Image image, Sprite sprite)
        {
            if (sprite == null)
            {
                image.color = Color.clear;
                image.sprite = sprite;
            }
            else
            {
                image.color = Color.white;
                image.sprite = sprite;
            }
        }

        protected override void UpdateInfo()
        {
            if (m_showDataOf == null)
            {
                m_locationName.text = "";
                m_panelBackground.color = Color.clear;
                SetImage(m_locationImage, null);
                m_description.text = "";
                return;
            }

            m_locationName.text = m_showDataOf.indexName;
            m_panelBackground.color = Color.white;
            SetImage(m_locationImage, m_showDataOf.infoImage);
            m_description.text = m_showDataOf.description;
        }

    }
}

