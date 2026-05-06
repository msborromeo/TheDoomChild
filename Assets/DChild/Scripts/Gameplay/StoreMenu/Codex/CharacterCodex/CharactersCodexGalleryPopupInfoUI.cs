using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Codex.Characters
{
    public class CharactersCodexGalleryPopupInfoUI : CodexGalleryPopupInfoUI<CharacterCodexData>
    {
        [SerializeField]
        private TextMeshProUGUI m_alphabetName;
        [SerializeField]
        private TextMeshProUGUI m_baybayinName;
        [SerializeField]
        private Image m_creatureImage;
        [SerializeField]
        private TextMeshProUGUI m_description;

        protected override void UpdateInfo()
        {
            if (m_showDataOf == null)
            {
                SetImage(m_creatureImage, null);
                m_description.text = "";
            }
            else
            {
                SetImage(m_creatureImage, m_showDataOf.infoImage);
                m_description.text = m_showDataOf.description;

            }
        }

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
    }


}
