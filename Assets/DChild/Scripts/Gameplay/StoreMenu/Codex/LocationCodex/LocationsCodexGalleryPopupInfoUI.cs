using DChild.Codex.LocationCodex;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace DChild.Menu.Codex.Locations
{
    public class LocationsCodexGalleryPopupInfoUI : CodexGalleryPopupInfoUI<LocationCodexData>
    {
        [SerializeField] private TextMeshProUGUI m_locationName;
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
            gameObject.SetActive(m_showDataOf != null);

            if (m_showDataOf == null)
            {
                m_locationName.text = "";
                SetImage(m_locationImage, null);
                m_description.text = "";
                return;
            }

            m_locationName.text = m_showDataOf.indexName;
            SetImage(m_locationImage, m_showDataOf.infoImage);
            m_description.text = m_showDataOf.description;
        }

    }
}

