using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Localization
{
    public interface IPromptLocalizer
    {
        event Action<string> LocalizeText;
    }
}
