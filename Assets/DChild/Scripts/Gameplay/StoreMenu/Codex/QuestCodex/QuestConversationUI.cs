using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestConversationUI : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI m_objectivePanel;
        [SerializeField] private List<QuestDialogueUI> m_dialogueList;


        public void Display(Quest data)
        {
            //m_objectivePanel.text = data.objective;
            //int count = data.dialogues.Count;
            //for (int i = 0; i < m_dialogueList.Count; i++)
            //{
            //    var ui = m_dialogueList[i];
            //    ui.gameObject.SetActive(i < count);

            //    if (i < count)
            //        ui.Display(data.dialogues[i]);
            //}
        }
    }
}