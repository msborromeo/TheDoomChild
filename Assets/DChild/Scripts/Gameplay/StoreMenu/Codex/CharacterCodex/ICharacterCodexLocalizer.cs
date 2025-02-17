using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DChild.Menu.Codex;
using DChild.Codex.Characters;

namespace DChild.Localization
{
    public interface ICharacterCodexLocalizer
    {
        event Action<CharacterCodexData> localizeCharacterData;
    }
}
