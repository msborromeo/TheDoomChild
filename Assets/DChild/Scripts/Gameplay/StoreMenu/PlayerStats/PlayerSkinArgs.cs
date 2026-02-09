using DChild.Gameplay.Characters.Player.Skins;
using Holysoft.Event;

namespace DChild.Gameplay.UI.PlayerStats
{
    public class PlayerSkinArgs : EventActionArgs
    {
        private SkinData m_data;
        public SkinData data => m_data;
    
        public PlayerSkinArgs(SkinData data)
        {
            m_data = data;
        }
    }
}