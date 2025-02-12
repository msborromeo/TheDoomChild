using DChild.Gameplay.Characters.Players.SoulSkills;
using System;
using TMPro;

namespace DChild.Localization
{
    public interface ISoulSkillLocalizer 
    {
        event Action<TextMeshProUGUI, TextMeshProUGUI, SoulSkill> soulSkillLocalize;
    }
}
