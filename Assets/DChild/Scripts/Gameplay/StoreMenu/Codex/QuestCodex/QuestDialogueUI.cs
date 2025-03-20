using DarkTonic.MasterAudio.Examples;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestDialogueUI : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI m_dialogueField;

        public void Display(Quest data)
        {
            //m_dialogueField.text = $"{data.characterName}: {data.dialogue}";
        }
    }
}