using DarkTonic.MasterAudio.Examples;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Quest.UI
{
    public class QuestDialogueUI : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI m_dialogueField;

        public void Display(DialogueData data)
        {
            m_dialogueField.text = $"{data.characterName}: {data.dialogue}";
        }
    }
}