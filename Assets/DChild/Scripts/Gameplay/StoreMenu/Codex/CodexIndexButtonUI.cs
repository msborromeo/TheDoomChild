using DChild.Gameplay.Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DChild.Menu.Codex
{

    public class CodexIndexButtonUI : MonoBehaviour
    {
        [SerializeField] private ScrollRect m_parentView;
        [SerializeField] private RectTransform m_buttonRect;
        
        [SerializeField] private CodexPageHandler m_pageHandler;
        [SerializeField] private CodexPage m_codexPage;

        public void UpdateCurrentPage() => m_pageHandler.SetCurrentPage(m_codexPage);
        public void UpdateScrollToElement()
        {
            if (!m_parentView || !m_buttonRect) return;

            RectTransform content = m_parentView.content;
            RectTransform viewport = m_parentView.viewport;

            float halfWidth = m_buttonRect.rect.width * 0.5f;
            float viewportCenter = viewport.InverseTransformPoint(m_buttonRect.position).x;
            float contentCenter = content.InverseTransformPoint(m_buttonRect.position).x;

            float leftEdge = viewportCenter - halfWidth;
            float rightEdge = viewportCenter + halfWidth;

            if (leftEdge < viewport.rect.xMin || rightEdge > viewport.rect.xMax)
            {
                float scrollableWidth = content.rect.width - viewport.rect.width;
                if (scrollableWidth <= 0) return;

                float targetContentX = (leftEdge < viewport.rect.xMin)
                    ? (contentCenter - halfWidth)
                    : (contentCenter + halfWidth - viewport.rect.width);

                m_parentView.horizontalNormalizedPosition = Mathf.Clamp01(targetContentX / scrollableWidth);
            }
        }
    }
}