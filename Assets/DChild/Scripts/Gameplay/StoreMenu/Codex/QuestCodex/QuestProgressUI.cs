using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Quest.UI
{

    public class QuestProgressUI : MonoBehaviour
    {

        [BoxGroup("TMP Fields"), SerializeField] private TextMeshProUGUI m_questOrder;
        [BoxGroup("TMP Fields"), SerializeField] private TextMeshProUGUI m_questName;
        [BoxGroup("TMP Fields"), SerializeField] private TextMeshProUGUI m_questStatus;

        //[BoxGroup("Placeholder Values"), SerializeField] private int m_order;
        //[BoxGroup("Placeholder Values"), SerializeField] private string m_name;
        //[BoxGroup("Placeholder Values"), SerializeField] private QuestStatus m_status;

        [Button(ButtonSizes.Large)]
        public void Display(QuestProgressData quest)
        {
            m_questOrder.text = $"{toRomanNumeral(quest.sequence)}";
            m_questName.text = quest.sectionName;
            m_questStatus.text = $"{quest.status}".Replace("_", " ");
        }

        private static string toRomanNumeral(int number)
        {
            if (number > 0 && number < 11)
            {
                var romanNumerals = new (int value, string numeral)[]
                {
                    (10, "X"),
                    (9, "IX"),
                    (5, "V"),
                    (4, "IV"),
                    (1, "I")
                };

                var result = new System.Text.StringBuilder();

                foreach (var (value, numeral) in romanNumerals)
                {
                    while (number >= value)
                    {
                        result.Append(numeral);
                        number -= value;
                    }
                }

                return result.ToString();
            }
            return "N/A";
        }
    }
}