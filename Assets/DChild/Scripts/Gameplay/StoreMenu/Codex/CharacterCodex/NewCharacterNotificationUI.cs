using DChild.Gameplay;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Codex.Characters
{
    public class NewCharacterNotificationUI : NotificationUI
    {
        [SerializeField] private Image m_characterPortrait;
        [SerializeField] private TextMeshProUGUI m_characterName;

        [Button]
        public void Show(CharacterCodexData data)
        {
            m_characterPortrait.sprite = data.characterType == CharacterType.NPC
                    ? data.infoImage
                    : data.armyData.icon;
            m_characterName.text = data.characterName;
        }
    }

}

