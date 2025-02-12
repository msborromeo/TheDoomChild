using TMPro;
using System;
using DChild.Gameplay.UI.PrimarySkills;

namespace DChild.Localization
{
    public interface IPrimarySkillLocalizer
    {
        event Action<PrimarySkillSelectable> localizePrimarySkill;
    }
}
