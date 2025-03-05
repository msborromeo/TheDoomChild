using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DChild.Menu.Codex;
using DChild.Menu.Bestiary;

namespace Dchild.Localization
{
    public interface ICodexBestiaryLocalizer
    {
        event Action<BestiaryData> localizeBestiaryData;
    }
}


