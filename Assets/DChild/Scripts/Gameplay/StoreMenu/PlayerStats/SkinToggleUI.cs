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

        public void SetSkinData(SkinData value) => m_attachedSkin = value;

        public void Display()
        {
            m_iconSlot.sprite = m_attachedSkin.icon;
        }

        public void Select()
        {
            OnToggleSelected?.Invoke(this, new PlayerSkinArgs(m_attachedSkin));
        }

    }
}