using Doozy.Runtime.UIManager.Components;
using Holysoft.Collections;
using Holysoft.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI
{

    public class ArmyGroupIndexHandle : MonoBehaviour, IPageHandle
    {
        [SerializeField]
        private MoreGroupsClassLabel m_panelLabel;
        [SerializeField]
        private UIButton m_previousButton;
        [SerializeField]
        private UIButton m_nextButton;
        [SerializeField]
        private List<AttackingGroupSelectableOptionUI> m_selectableGroups;

        private List<IAttackingGroup> m_groups = new List<IAttackingGroup>();
        private List<IAttackingGroup> m_filteredGroups = new List<IAttackingGroup>();

        [SerializeField]
        private UIScrollbar m_scrollBar;

        private int m_startingIndex = 0;
        private int m_page;
        private const int m_maxRows = 8;
        private const int m_rowsPerColumn = 4;

        private int m_totalPages;
        private float m_scrollBarIncrements;
        private bool m_cyclePageGuard;

        public int currentPage => m_page;
        public event EventAction<EventActionArgs> PageChange;
        public event Action<IAttackingGroup> GroupSelected;

        private int m_availableGroupCount;

        public void SetGroups(
            DamageType damageType,
            List<IAttackingGroup> groups,
            int availableGroupCount)
        {
            m_panelLabel?.SetPanelLabel(damageType);
            m_groups = groups ?? new List<IAttackingGroup>();
            m_availableGroupCount = availableGroupCount;

            Initialize();
        }

        public void Select(AttackingGroupSelectableOptionUI selectable)
        {
            if (selectable == null || selectable.group == null)
                return;

            GroupSelected?.Invoke(selectable.group);
        }

        public void SetAvailableGroups(DamageType damageType, List<IAttackingGroup> groups)
        {
            m_panelLabel?.SetPanelLabel(damageType);
            m_groups = groups ?? new List<IAttackingGroup>();
            m_availableGroupCount = m_groups.Count;
            Initialize();
        }

        public void Initialize()
        {
            m_totalPages = Mathf.Max(1, GetTotalPages());
            m_scrollBar.numberOfSteps = m_totalPages;
            m_scrollBarIncrements = m_totalPages > 1 ? 1f / (m_totalPages - 1) : 0f;
            m_scrollBar.size = 1f / m_totalPages;
            m_scrollBar.value = 0;

            SetPage(0);
        }   

        public void Display(List<IAttackingGroup> attackingGroups)
        {
            for (int i = 0; i < m_maxRows; i++)
            {
                var selectableGroup = m_selectableGroups[i];

                if (i < attackingGroups.Count)
                {
                    int absoluteIndex = m_startingIndex + i;
                    bool isUsed = absoluteIndex >= m_availableGroupCount;

                    selectableGroup.SetSelectionIndex(absoluteIndex);
                    selectableGroup.Display(attackingGroups[i]);
                    selectableGroup.SetUsed(isUsed);
                    continue;
                }

                selectableGroup.Display(null);
            }
        }

        public int GetTotalPages()
        {
            return Mathf.CeilToInt(m_groups.Count / (float)m_maxRows);
        }

        public void SetPage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= m_totalPages)
                return;

            m_previousButton.interactable = pageIndex > 0;
            m_nextButton.interactable = pageIndex < (m_totalPages - 1);

            m_page = pageIndex;
            m_startingIndex = m_page * m_maxRows;

            int rangeCount = (m_startingIndex + m_maxRows) < m_groups.Count ? m_maxRows : (m_groups.Count - m_startingIndex);

            m_filteredGroups = m_groups.GetRange(m_startingIndex, rangeCount);

            Display(m_filteredGroups);

            RebuildRowNavigation();
            EnsureValidRowSelection();
        }

        private IEnumerator ForceSelectAvailableRowRoutine(bool selectLast)
        {
            yield return null;

            RebuildRowNavigation();
            SelectAvailableRow(selectLast);
        }


        public void NextPage()
        {
            m_cyclePageGuard = true;

            m_page++;
            SetPage(m_page);

            if (m_page != GetTotalPages() - 1)
            {
                m_scrollBar.value = m_scrollBarIncrements * m_page;

            }
            else
            {
                m_scrollBar.value = 1;
            }
            m_cyclePageGuard = false;
            StartCoroutine(ForceSelectAvailableRowRoutine(false));
        }

        public void PreviousPage()
        {
            m_cyclePageGuard = true;

            m_page--;
            SetPage(m_page);

            m_scrollBar.value = m_scrollBarIncrements * m_page;

            m_cyclePageGuard = false;
            StartCoroutine(ForceSelectAvailableRowRoutine(true));
        }

        public void HandleScroll()
        {
            if (m_cyclePageGuard || m_totalPages <= 1)
                return;

            int updatedPage = Mathf.FloorToInt(m_scrollBar.value / m_scrollBarIncrements);
            updatedPage = Mathf.Clamp(updatedPage, 0, m_totalPages - 1);

            if (m_page != updatedPage)
            {
                SetPage(updatedPage);
            }
            SelectAvailableRow(false);
        }

        private void RebuildRowNavigation()
        {
            int columnCount = Mathf.CeilToInt(m_selectableGroups.Count / (float)m_rowsPerColumn);

            for (int i = 0; i < m_selectableGroups.Count; i++)
            {
                var button = m_selectableGroups[i].selectable;
                if (button == null)
                    continue;

                int column = i / m_rowsPerColumn;
                var navigation = button.navigation;

                navigation.mode = Navigation.Mode.Explicit;
                navigation.wrapAround = false;
                navigation.selectOnUp = FindVerticalSelection(i, -1) ?? GetNavigable(m_previousButton);
                navigation.selectOnDown = FindVerticalSelection(i, 1) ?? GetNavigable(m_nextButton);
                navigation.selectOnLeft = column > 0 ? FindClosestSelectionInColumn(i, column - 1) : null;
                navigation.selectOnRight = column < columnCount - 1 ? FindClosestSelectionInColumn(i, column + 1) : null;

                button.navigation = navigation;
            }
        }

        private Selectable FindVerticalSelection(int sourceIndex, int direction)
        {
            int column = sourceIndex / m_rowsPerColumn;
            int columnStart = column * m_rowsPerColumn;
            int columnEnd = Mathf.Min(columnStart + m_rowsPerColumn, m_selectableGroups.Count);

            for (int i = sourceIndex + direction; i >= columnStart && i < columnEnd; i += direction)
            {
                var candidate = GetNavigable(m_selectableGroups[i].selectable);
                if (candidate != null)
                    return candidate;
            }

            return null;
        }

        private Selectable FindClosestSelectionInColumn(int sourceIndex, int targetColumn)
        {
            int sourceRow = sourceIndex % m_rowsPerColumn;
            int columnStart = targetColumn * m_rowsPerColumn;

            for (int distance = 0; distance < m_rowsPerColumn; distance++)
            {
                int rowAbove = sourceRow - distance;
                if (rowAbove >= 0)
                {
                    var candidate = GetRowSelection(columnStart + rowAbove);
                    if (candidate != null)
                        return candidate;
                }

                int rowBelow = sourceRow + distance;
                if (distance > 0 && rowBelow < m_rowsPerColumn)
                {
                    var candidate = GetRowSelection(columnStart + rowBelow);
                    if (candidate != null)
                        return candidate;
                }
            }

            return null;
        }

        private Selectable GetRowSelection(int index)
        {
            if (index < 0 || index >= m_selectableGroups.Count)
                return null;

            return GetNavigable(m_selectableGroups[index].selectable);
        }

        private static Selectable GetNavigable(Selectable selectable)
        {
            return selectable != null &&
                   selectable.gameObject.activeInHierarchy &&
                   selectable.IsInteractable()
                ? selectable
                : null;
        }

        private void EnsureValidRowSelection()
        {
            if (EventSystem.current == null)
                return;

            var selectedObject = EventSystem.current.currentSelectedGameObject;
            for (int i = 0; i < m_selectableGroups.Count; i++)
            {
                var button = m_selectableGroups[i].selectable;
                if (button == null || button.gameObject != selectedObject)
                    continue;

                if (GetNavigable(button) == null)
                    SelectNearestAvailableRow(i);

                return;
            }
        }

        private void SelectNearestAvailableRow(int sourceIndex)
        {
            for (int distance = 1; distance < m_selectableGroups.Count; distance++)
            {
                var previous = GetRowSelection(sourceIndex - distance);
                if (previous != null)
                {
                    previous.Select();
                    return;
                }

                var next = GetRowSelection(sourceIndex + distance);
                if (next != null)
                {
                    next.Select();
                    return;
                }
            }

            SelectPageControlFallback(false);
        }

        private void SelectAvailableRow(bool selectLast)
        {
            int index = selectLast ? m_selectableGroups.Count - 1 : 0;
            int end = selectLast ? -1 : m_selectableGroups.Count;
            int step = selectLast ? -1 : 1;

            for (; index != end; index += step)
            {
                var candidate = GetRowSelection(index);
                if (candidate == null)
                    continue;

                candidate.Select();
                return;
            }

            SelectPageControlFallback(selectLast);
        }

        private void SelectPageControlFallback(bool cameFromPreviousPage)
        {
            Selectable fallback = cameFromPreviousPage
                ? GetNavigable(m_nextButton) ?? GetNavigable(m_previousButton)
                : GetNavigable(m_previousButton) ?? GetNavigable(m_nextButton);

            if (fallback != null)
            {
                fallback.Select();
                return;
            }

            EventSystem.current?.SetSelectedGameObject(null);
        }

        private void Awake()
        {
            if (m_panelLabel == null)
            {
                m_panelLabel = GetComponentInChildren<MoreGroupsClassLabel>(true);
            }
        }
    }
}
