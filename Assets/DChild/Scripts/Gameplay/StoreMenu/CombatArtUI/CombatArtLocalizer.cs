using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;
using DChild.Gameplay.Characters.Players;
using System;

namespace DChild.Localization
{
    [RequireComponent(typeof(ICombatArtLocalizer))]
    public class CombatArtLocalizer : MonoBehaviour
    {
        [SerializeField]
        private Localize m_localizeDescriptionLabel;

        [SerializeField]
        private Localize m_localizeControlLabel;

        [SerializeField]
        private Localize m_localizeNameLabel;

        private ICombatArtLocalizer m_Injector;

        public Action CombatArtsInstructionsLocalized;

        private void Awake()
        {
            m_Injector = GetComponent<ICombatArtLocalizer>();
            m_Injector.localizeCombatArt += onUpdate;
        }

        private void OnDisable()
        {
            m_Injector.localizeCombatArt -= onUpdate;
        }

        private void onUpdate(CombatArtData combatArt,int level)
        {
            m_localizeDescriptionLabel.SetTerm(LocalizationUtility.GetTermKey(combatArt, LocalizationUtility.CombatArtField.Description) + level.ToString());
            m_localizeControlLabel.SetTerm(LocalizationUtility.GetTermKey(combatArt, LocalizationUtility.CombatArtField.Controls));
            m_localizeNameLabel.SetTerm(LocalizationUtility.GetTermKey(combatArt, LocalizationUtility.CombatArtField.Name));
            CombatArtsInstructionsLocalized?.Invoke();
        }
    }
}

