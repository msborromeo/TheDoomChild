using Dchild.Localization;
using DChild.Menu.Bestiary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DChild.Menu.Codex.Bestiary
{
    public class BestiaryCodexIndexButton : CodexIndexButton<BestiaryData, ICodexIndexInfo> , ICodexBestiaryLocalizer
    {
        public event Action<BestiaryData> localizeBestiaryData;

        public override void SetData(BestiaryData data)
        {
            base.SetData(data);
            localizeBestiaryData?.Invoke(data);

        }
    }


}