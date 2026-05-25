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

        [Button]
        public void Display(CharacterCodexData[] characterUnits, CharacterCodexProgressTracker progressTracker)
        {
            Image[] spritePortraits = { m_firstModel, m_secondModel, m_thirdModel };

            for (int i = 0; i < spritePortraits.Length; i++)
            {
                spritePortraits[i].gameObject.SetActive(i < characterUnits.Length);
                
                if (i < characterUnits.Length)
                {
                    SetModelSprite(spritePortraits[i], characterUnits[i].infoImage);
                    SetModelOpacity(spritePortraits[i], progressTracker.HasInfoOf(characterUnits[i].id));
                }
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