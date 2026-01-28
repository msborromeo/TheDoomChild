using DChild.Gameplay.Characters.Player.Skins;
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

        public void Display(SkinData data)
        {
            m_iconSlot.gameObject.SetActive(data != null);
            
            if(data == null || data.icon == null)
                return;

            AttachSkinData(data);
            m_iconSlot.sprite = m_attachedSkin.icon;
        }

        public void Select() => OnToggleSelected?.Invoke(this, new PlayerSkinArgs(m_attachedSkin));

    }
}