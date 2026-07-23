using DChild.Codex.Characters;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Codex.ArmyTroops
{
    public class ArmyTroopsModelsUI : MonoBehaviour
    {
        [SerializeField] private Image m_firstModel;
        [SerializeField] private Image m_secondModel;
        [SerializeField] private Image m_thirdModel;

        public void Display(CharacterCodexData[] characterUnits, CharacterCodexProgressTracker tracker, bool debugReveal = false)
        {
            Image[] spritePortraits = characterUnits.Length < 3
                ? new Image[] { m_firstModel, m_thirdModel }
                : new Image[] { m_firstModel, m_secondModel, m_thirdModel };

            m_secondModel.gameObject.SetActive(characterUnits.Length > 2);

            int displayCount = Mathf.Min(spritePortraits.Length, characterUnits.Length);

            for (int i = 0; i < displayCount; i++)
            {
                var unit = characterUnits[i];
                var portrait = spritePortraits[i];

                portrait.gameObject.SetActive(true);

                SetModelSprite(portrait, unit.infoImage);
                SetModelOpacity(portrait, debugReveal || tracker.HasInfoOf(unit.id));
            }

            for (int i = displayCount; i < spritePortraits.Length; i++)
            {
                spritePortraits[i].gameObject.SetActive(false);
            }
        }

        private void SetModelOpacity(Image model, bool isUnlocked)
        {
            model.color = isUnlocked ? Color.white : Color.black;
        }


        private void SetModelSprite(Image model, Sprite value)
        {
            model.sprite = value;
        }
    }
}