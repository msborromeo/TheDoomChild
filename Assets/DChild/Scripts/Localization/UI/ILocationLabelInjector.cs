using DChild.Gameplay.Environment;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace DChild.Localization
{
    public interface ILocationLabelInjector
    {
        event Action<TextMeshProUGUI, Location> LocationLabelUpdated;
    }
}