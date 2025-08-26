using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.UI
{
    [CreateAssetMenu(fileName = "Dialogue Log Data", menuName = "Pixel Crushers/Dialogue System/Dialogue Log Data")]
    public class DialogueLogData : ScriptableObject
    {
        [SerializeField]
        private DialogueDatabase m_dialogueDatabase;
        [SerializeField,ValueDropdown("GetAllValidConversationIDs"), OnValueChanged("RefreshEntryIDs")]
        private int m_conversationID;
        [SerializeField, ValueDropdown("GetAllValidEntryIDs")]
        private int m_entryID;

        [SerializeField, ReadOnly]
        private List<string> m_dialogueList;
        public List<string> dialogueList => m_dialogueList;

        [Button]
        public void PopulateDialogueList()
        {
            m_dialogueList.Clear();

            DialogueEntry currentEntry = m_dialogueDatabase.GetDialogueEntry(m_conversationID, m_entryID);
            string currentActor = m_dialogueDatabase.GetActor(currentEntry.ActorID).LocalizedName;

            string filteredFirstText = FilterDialogueTags(currentEntry.DialogueText);
            m_dialogueList.Add($"{currentActor}: {filteredFirstText}");

            bool hasOutGoingLink = currentEntry.outgoingLinks.Count > 0;

            while (hasOutGoingLink)
            {
                var link = currentEntry.outgoingLinks[0];
                DialogueEntry linkedEntry = m_dialogueDatabase.GetDialogueEntry(m_conversationID, link.destinationDialogueID);

                currentEntry = linkedEntry;

                currentActor = m_dialogueDatabase.GetActor(currentEntry.ActorID).LocalizedName;
                string dialogueText = currentEntry.DialogueText;
                string filteredText = FilterDialogueTags(dialogueText);
                m_dialogueList.Add($"{currentActor}: {filteredText}");
                hasOutGoingLink = currentEntry.outgoingLinks.Count > 0;
            }
        }

        private string FilterDialogueTags(string dialogue)
        {
            var filteredDialogue = dialogue;

            if (dialogue.Contains('<') || dialogue.Contains('>'))
            {
                var lessThanIndex = dialogue.IndexOf('<');
                var greaterThanIndex = dialogue.IndexOf('>');

               for(int i = lessThanIndex; i < greaterThanIndex; i++)
                {
                    dialogue.Remove(i);
                }
            }

            if(dialogue.Contains('[') || dialogue.Contains(']'))
            {
                var leftBracketIndex = dialogue.IndexOf("[");
                var rightBracketIndex = dialogue.IndexOf("]");

                /*
                 * Assumption here is from leftbracket '[' there sill always be 5 characters before the variable name, nameley "[var="
                 * So we get variable name starting at leftbracketindex + 5, then the rest of the indexes are assumed to be characters in 
                 * the variable name. So we get the substring only up to right index - 1 to leave out the bracket ']'
                */
                string variableName = dialogue.Substring(leftBracketIndex + 5, ((rightBracketIndex - 1) - (leftBracketIndex + 4)));
                string tag = dialogue.Substring(leftBracketIndex, rightBracketIndex + 1);

                var variable = m_dialogueDatabase.GetVariable(variableName);

                string variableValue = variable.InitialValue + " ";

                if (tag.Contains("pic"))
                {
                    filteredDialogue = dialogue.Replace(tag, "");
                }
                else
                {
                    filteredDialogue = dialogue.Replace(tag, variableValue);
                }

            }

            return filteredDialogue;
        }

        private IEnumerable GetAllValidConversationIDs()
        {
            ValueDropdownList<int> valueDropdownItems = new ValueDropdownList<int>();

            for(int i = 0; i < m_dialogueDatabase.conversations.Count; i++)
            {
                valueDropdownItems.Add(m_dialogueDatabase.conversations[i].Title.ToString(), m_dialogueDatabase.conversations[i].id);
            }

            return valueDropdownItems;
        }

        private IEnumerable GetAllValidEntryIDs()
        {
            ValueDropdownList<int> valueDropdownItems = new ValueDropdownList<int>();

            var entryIDCount = m_dialogueDatabase.GetConversation(m_conversationID).dialogueEntries.Count;

            for (int i = 0; i < entryIDCount; i++)
            {
                valueDropdownItems.Add(m_dialogueDatabase.GetConversation(m_conversationID).GetDialogueEntry(i).DialogueText,
                    m_dialogueDatabase.GetConversation(m_conversationID).GetDialogueEntry(i).id);
            }

            Debug.Log(entryIDCount);

            return valueDropdownItems;
        }

        public void RefreshEntryIDs()
        {
            GetAllValidConversationIDs();
        }
    }
}

