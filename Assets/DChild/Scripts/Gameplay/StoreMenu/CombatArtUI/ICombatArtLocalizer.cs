using System;
using DChild.Gameplay.Characters.Players;

namespace DChild.Localization
{
    public interface ICombatArtLocalizer
    {
        event Action<CombatArtData,int> localizeCombatArt;
    }
}
