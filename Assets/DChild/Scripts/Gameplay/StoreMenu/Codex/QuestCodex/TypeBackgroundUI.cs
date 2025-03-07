using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Codex.Quest.UI
{
    public class TypeBackgroundUI : MonoBehaviour
    {
        [SerializeField] private Sprite m_mainQuest;
        [SerializeField] private Sprite m_sideQuest;
        [SerializeField] private Image m_targetButton;

        public void SetBackground(bool isMain) => m_targetButton.sprite = isMain ? m_mainQuest : m_sideQuest;
    }
}