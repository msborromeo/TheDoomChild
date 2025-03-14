using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DChild.Codex.Quest.UI
{


    public class QuestTypeToggleUI : MonoBehaviour
    {

        [SerializeField] private List<QuestButtonUI> m_questButtons;
        [SerializeField] private List<SampleDummyQuestData> m_questList;


        //[Button(ButtonSizes.Large)]
        public void Display()
        {
            for (int i = 0; i < m_questButtons.Count; i++)
            {
                if (i < m_questList.Count)
                {
                    m_questButtons[i].Display(m_questList[i]);
                    continue;
                }
            }
        }
    }
}