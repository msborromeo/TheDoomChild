using TMPro;
using System;
using DChild.Gameplay.Characters.Players;

namespace DChild.Localization
{
    public interface IPrimarySkillLocalizer
    {
        event Action<PrimarySkillData> localizePrimarySkill;
    }
}
