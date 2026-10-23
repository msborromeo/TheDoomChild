using UnityEngine;

using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.EquipmentSystem;
using DChild.Menu.Codex;
using Doozy.Runtime.UIManager.Components;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class ActiveEquipmentInfoUI : MonoBehaviour
    {
        [SerializeField] private ActiveEquipmentSkillRowUI[] m_rows;
        [SerializeField] private RectTransform m_viewport;
        [SerializeField] private VerticalLayoutGroup m_listLayout;
        [SerializeField] private CodexScrollNavigationHandle m_navigation;
        [SerializeField] private UIScrollbar m_scrollbar;
        [SerializeField] private TextMeshProUGUI m_emptyLabel;

        private readonly List<SoulSkill> m_skills = new();
        private readonly List<float> m_rowHeights = new();
        private readonly List<int> m_pageStarts = new();
        private PlayerSoulEquipmentHandle m_equipmentHandle;
        private Action m_requestRefresh;
        private bool m_isOpen;
        private bool m_needsInitialRefresh;
        private int m_currentPageIndex;
        private Vector2 m_lastAvailableSize;

        public void Initialize(PlayerSoulEquipmentHandle equipmentHandle, Action requestRefresh)
        {
            Hide();
            m_equipmentHandle = equipmentHandle;
            m_requestRefresh = requestRefresh;
            m_isOpen = true;
            m_needsInitialRefresh = true;
            m_navigation.OnCurrentPageChange += DisplayPage;

            ClearRows();
            m_emptyLabel.gameObject.SetActive(equipmentHandle == null);
            Refresh(true);
        }

        public void Hide()
        {
            if (m_navigation != null)
                m_navigation.OnCurrentPageChange -= DisplayPage;
            m_equipmentHandle = null;
            m_requestRefresh = null;
            m_isOpen = false;
        }

        internal void RefreshFromEquipment()
        {
            if (m_isOpen)
                Refresh(m_needsInitialRefresh);
        }

        private List<SoulSkill> CollectWornSkills()
        {
            var uniqueSkills = new Dictionary<int, SoulSkill>();
            if (m_equipmentHandle != null)
            {
                for (int slot = 0; slot < (int)SoulSlot._COUNT; slot++)
                {
                    if (!m_equipmentHandle.TryGetEquippedSoulEquipment((SoulSlot)slot, out var item))
                        continue;

                    var skills = item != null ? item.soulEquipment?.soulSkillList : null;
                    if (skills == null)
                        continue;

                    foreach (var skill in skills)
                    {
                        if (skill != null && !uniqueSkills.ContainsKey(skill.id))
                            uniqueSkills.Add(skill.id, skill);
                    }
                }
            }

            var result = new List<SoulSkill>(uniqueSkills.Values);
            result.Sort((left, right) =>
            {
                int nameOrder = string.Compare(left.name, right.name, StringComparison.OrdinalIgnoreCase);
                return nameOrder != 0 ? nameOrder : left.id.CompareTo(right.id);
            });
            return result;
        }

        private void Refresh(bool force)
        {
            var skills = CollectWornSkills();
            var listRect = (RectTransform)m_listLayout.transform;
            var availableSize = new Vector2(listRect.rect.width - m_listLayout.padding.horizontal,
                m_viewport.rect.height - m_listLayout.padding.vertical);
            bool changed = skills.Count != m_skills.Count;
            for (int i = 0; !changed && i < skills.Count; i++)
                changed = skills[i] != m_skills[i];

            if (!force && !changed && availableSize == m_lastAvailableSize)
                return;

            if (availableSize.x <= 0f || availableSize.y <= 0f)
                return;

            m_needsInitialRefresh = false;
            m_lastAvailableSize = availableSize;
            m_skills.Clear();
            m_skills.AddRange(skills);
            m_rowHeights.Clear();
            m_pageStarts.Clear();
            int rowsOnPage = 0;
            float usedHeight = 0f;
            foreach (var skill in m_skills)
            {
                float rowHeight = m_rows[0].MeasureHeight(skill, availableSize.x);
                float spacing = rowsOnPage > 0 ? m_listLayout.spacing : 0f;
                if (rowsOnPage > 0 && (rowsOnPage == m_rows.Length ||
                    usedHeight + spacing + rowHeight > availableSize.y))
                {
                    rowsOnPage = 0;
                    usedHeight = 0f;
                    spacing = 0f;
                }

                if (rowsOnPage == 0)
                    m_pageStarts.Add(m_rowHeights.Count);
                m_rowHeights.Add(rowHeight);
                usedHeight += spacing + rowHeight;
                rowsOnPage++;
            }

            int pageIndex = force || changed ? 0 :
                Mathf.Clamp(m_currentPageIndex, 0, Mathf.Max(0, m_pageStarts.Count - 1));
            m_navigation.SetupScroll(m_pageStarts.Count, 1);
            m_scrollbar.interactable = m_pageStarts.Count > 1;
            m_emptyLabel.gameObject.SetActive(m_skills.Count == 0);
            if (pageIndex > 0)
            {
                m_scrollbar.SetValueWithoutNotify(pageIndex / (float)(m_pageStarts.Count - 1));
                m_navigation.HandleScroll();
            }
            else
                DisplayPage(0);
        }

        private void DisplayPage(int pageIndex)
        {
            m_currentPageIndex = pageIndex;
            ClearRows();
            if (pageIndex < 0 || pageIndex >= m_pageStarts.Count)
                return;

            int start = m_pageStarts[pageIndex];
            int end = pageIndex + 1 < m_pageStarts.Count ? m_pageStarts[pageIndex + 1] : m_skills.Count;
            for (int index = start; index < end; index++)
            {
                var row = m_rows[index - start];
                row.gameObject.SetActive(true);
                row.Display(m_skills[index], m_rowHeights[index]);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)m_listLayout.transform);
        }

        private void ClearRows()
        {
            foreach (var row in m_rows)
            {
                row.Clear();
                row.gameObject.SetActive(false);
            }
        }

        private void OnRectTransformDimensionsChange()
        {
            if (m_isOpen)
                m_requestRefresh?.Invoke();
        }

        private void OnDisable() => Hide();
        private void OnDestroy() => Hide();
    }
}
