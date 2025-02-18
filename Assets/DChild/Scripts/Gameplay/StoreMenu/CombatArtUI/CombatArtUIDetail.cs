using DChild.Gameplay.Characters.Players;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using DChild.Localization;
using System;

namespace DChild.Gameplay.UI.CombatArts
{
    public class CombatArtUIDetail : MonoBehaviour , ICombatArtLocalizer
    {
        [SerializeField]
        private TextMeshProUGUI m_artNameLabel;
        [SerializeField]
        private VideoPlayer m_preview;
        [SerializeField]
        private TextMeshProUGUI m_descriptionLabel;
        [SerializeField]
        private TextMeshProUGUI m_costLabel;
        [SerializeField]
        private TextMeshProUGUI m_controlsLabel;

        public event Action<CombatArtData,int> localizeCombatArt;

        public void Display(CombatArtData data, int level)
        {
            if (data != null)
            {
                m_artNameLabel.text = data.combatArtName;
                m_controlsLabel.text = data.controls;
                if (level > 1)
                {
                    m_artNameLabel.text += $" {level}";
                }
                Display(data.GetCombatArtLevelData(level));
                localizeCombatArt?.Invoke(data,level);
            }
        }

        private void Display(CombatArtLevelData levelData)
        {
            StopAllCoroutines();
            StartCoroutine(DisplayPreview(levelData.preview));
            m_descriptionLabel.text = levelData.description;
            m_costLabel.text = levelData.cost.ToString();
        }

        private IEnumerator DisplayPreview(VideoClip clip)
        {
            m_preview.Stop();
            yield return null;
            m_preview.clip = clip;
            m_preview.Play();
        }
    }

}