using Dchild.Localization;
using DChild.Menu.Bestiary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DChild.Menu.Codex.Bestiary
{
    public class BestiaryCodexIndexButton : CodexIndexButton<BestiaryData, ICodexIndexInfo>, ICodexBestiaryLocalizer
    {
        public event Action<BestiaryData> OnBestiaryDataChanged;

        event Action<BestiaryData> ICodexBestiaryLocalizer.localizeBestiaryData
        {
            add
            {
                OnBestiaryDataChanged += value;
            }
            remove
            {
                OnBestiaryDataChanged -= value;
            }

        }

        public override void SetData(BestiaryData data)
        {
            base.SetData(data);
            OnBestiaryDataChanged?.Invoke(data);
        }
    }
}