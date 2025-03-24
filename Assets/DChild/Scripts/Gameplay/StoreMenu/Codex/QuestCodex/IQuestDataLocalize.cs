using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using System;

namespace DChild.Localization
{
    public interface IQuestDataLocalize
    {
        event Action<QuestEntry, int> LocalizeEntry;
    }
}

