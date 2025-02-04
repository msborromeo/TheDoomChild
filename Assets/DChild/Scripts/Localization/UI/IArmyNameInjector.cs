using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DChild.Gameplay.ArmyBattle;

namespace DChild.Localization
{
    public interface IArmyNameInjector
    {
        event Action<TextMeshProUGUI, ArmyOverviewData> nameUpdate;
    }
}
