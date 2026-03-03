using DChild.Gameplay.Characters.Players;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using DChild.Localization;
using System;

namespace DChild.Gameplay.UI.CombatArts
{
    public class CombatArtUIDetail : MonoBehaviour, ICombatArtLocalizer
    {
        [SerializeField]
        private TextMeshProUGUI m_artNameLabel;
        [SerializeField]
        private VideoPlayer m_preview;
        [SerializeField]
        private TextMeshProUGUI m_descriptionLabel;
        [SerializeField]
        private TextMeshProUGUI m_requiredArtLabel;
        [SerializeField]
        private TextMeshProUGUI m_costLabel;
        [SerializeField]
        private TextMeshProUGUI m_controlsLabel;
        [SerializeField]
        private SetTextToTextBox m_controlsPromptSetter;
        [SerializeField]
        private CombatArtLocalizer m_combatArtLocalizer;

        public event Action<CombatArtData, int> localizeCombatArt;

        public void Display(CombatArtData data, int level)
        {
            switch (data.numberOfActions)
            {
                case 1:
                    m_controlsPromptSetter.SetText(data.controls, data.action);
                    break;
                case 2:
                    m_controlsPromptSetter.SetText(data.controls, data.action, data.action2);
                    break;
                case 3:
                    m_controlsPromptSetter.SetText(data.controls, data.action, data.action2, data.action3);
                    break;
                case 4:
                    break;
                default:
                    m_controlsPromptSetter.SetText(data.controls, data.action);
                    break;
            }
            if (data == null)
                return;

            m_artNameLabel.text =$"{ data.combatArtName} {(level > 1 ? $"{level}" : "")}"
            ;
            m_controlsLabel.text = data.controls;
            if (level > 1)
            {
                m_artNameLabel.text += $" {level}";
            }

            Display(data.GetCombatArtLevelData(level), level);
            localizeCombatArt?.Invoke(data, level);
        }

        private void Display(CombatArtLevelData levelData, int combatArtLevel)
        {
            if (levelData == null) return;

            StopAllCoroutines();
            StartCoroutine(DisplayPreview(levelData.preview));
            m_descriptionLabel.text = levelData.description;
            m_costLabel.text = levelData.cost.ToString();

            var art = levelData.requiredCombatArt;
            m_requiredArtLabel.text = levelData.requiredCombatArt != null
                ? art.combatArtName
                : "None";
        }

        private IEnumerator DisplayPreview(VideoClip clip)
        {
            m_preview.Stop();
            yield return null;
            m_preview.clip = clip;
            m_preview.Play();
        }

        private void OnCombatArtsLocalized()
        {
            m_controlsPromptSetter.SetText(m_controlsLabel.text);
        }

        private void Start()
        {
            m_combatArtLocalizer.CombatArtsInstructionsLocalized += OnCombatArtsLocalized;
        }

        private void OnDestroy()
        {
            m_combatArtLocalizer.CombatArtsInstructionsLocalized -= OnCombatArtsLocalized;
        }
    }

}