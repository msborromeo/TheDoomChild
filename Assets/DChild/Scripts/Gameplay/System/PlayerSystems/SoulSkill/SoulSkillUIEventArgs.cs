using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DChild.Gameplay.SoulSkills.UI
{
    public class SoulSkillUIEventArgs : IEventActionArgs
    {
        public SoulSkillUI soulskillUI { get; private set; }
        public SoulSkillUIEventArgs(SoulSkillUI soulskillUI)
        {
            this.soulskillUI = soulskillUI;
        }
    }
}
