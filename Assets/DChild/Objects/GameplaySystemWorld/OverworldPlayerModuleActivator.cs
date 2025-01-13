using DChild.Gameplay.Characters.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayerModuleActivator : PlayerModuleActivator
{
    public override void SetModuleLock(PrimarySkill module, bool isUnlocked)
    {
        if (isUnlocked)
        {
            m_unlockedSkills |= module;
        }
        else
        {
            m_unlockedSkills &= ~module;
        }
    }
}
