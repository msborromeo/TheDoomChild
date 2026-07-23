using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.UI
{
    public class BlacksmithRequirementUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_currentCount;

        [SerializeField] private Image m_background;
        [SerializeField] private Image m_requirementIcon;

        [BoxGroup("Sprites"), SerializeField] private Sprite m_missingSprite;
        [BoxGroup("Sprites"), SerializeField] private Sprite m_insufficientSprite;
        [BoxGroup("Sprites"), SerializeField] private Sprite m_completeSprite;

        public void UpdateBackground( int current, int required)
        {
            if (current <= 0)
                m_background.sprite = current <= 0
                    ? m_missingSprite
                    : current < required
                        ? m_insufficientSprite
                        : m_completeSprite;
        }

        public void SetIcon(Sprite value) => m_requirementIcon.sprite = value;
        public void SetLabel(ItemData currentItem, int current, int required)
        {
            m_currentCount.text = $"{current} of {required}";
        }

        public void SetDynamicVisuals(ItemData item, int inventoryQuantity, int required)
        {
            SetIcon(item.icon);
            SetLabel(item, inventoryQuantity, required);
            UpdateBackground(inventoryQuantity, required);
        }    

    }

}
