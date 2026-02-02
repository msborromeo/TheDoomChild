using DChild.Gameplay.Characters.Player.Skins;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.UI.PlayerStats
{
    public class SkinToggleUI : MonoBehaviour
    {
        [SerializeField] private Image m_iconSlot;

        private SkinData m_attachedSkin;

        public event EventAction<PlayerSkinArgs> OnToggleSelected;

        public void AttachSkinData(SkinData value) => m_attachedSkin = value;

        private void SetInteractablity(bool value)
        {
            gameObject.GetComponent<UIToggle>().interactable = value;
        }

        public void Display(SkinData data)
        {
            var hasData = data != null;
            
            m_iconSlot.gameObject.SetActive(hasData);
            SetInteractablity(hasData);

            if(!hasData || data.icon == null)
                return;

            AttachSkinData(data);
            m_iconSlot.sprite = m_attachedSkin.icon;
        }

        public void Select() => OnToggleSelected?.Invoke(this, new PlayerSkinArgs(m_attachedSkin));

    }
}