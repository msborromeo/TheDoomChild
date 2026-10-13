using System;
using System.Collections;
using System.Collections.Generic;
using Holysoft.Event;
using Holysoft.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DChild.Menu.UI
{
    public class SettingsSliderVisual : MonoBehaviour, ISelectableUI, IHighlightableUIEvents, ISelectHandler, IDeselectHandler
    {
        public event EventAction<SelectedUIEventArgs> UISelected;
        public event EventAction<SelectedUIEventArgs> UIDeselected;
        public event EventAction<SelectedUIEventArgs> UIHighlight;
        public event EventAction<SelectedUIEventArgs> UINormalize;
        private bool m_isDragged;
        private bool m_isPointerOnUI;
        private bool m_hasFocus;

        public void OnSelect(BaseEventData eventData)
        {
            m_hasFocus = true;
            Highlight();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            m_hasFocus = false;
            Normalize();
        }

        private void OnDisable()
        {
            bool wasDragged = m_isDragged;
            m_isDragged = false;
            m_hasFocus = false;
            m_isPointerOnUI = false;
            if (wasDragged) Deselect();
            Normalize();
        }

        public void Deselect()
        {
            UIDeselected?.Invoke(this, new SelectedUIEventArgs(this));
        }

        public void Highlight()
        {
            UIHighlight?.Invoke(this, new SelectedUIEventArgs(this));
        }

        public void Normalize()
        {
            if (m_isDragged || m_hasFocus)
                return;
            UINormalize?.Invoke(this, new SelectedUIEventArgs(this));
        }

        public void Select()
        {
            UISelected?.Invoke(this, new SelectedUIEventArgs(this));
        }

        public void OnPointerEnter()
        {
            m_isPointerOnUI = true;
        }

        public void OnPointerExit()
        {
            m_isPointerOnUI = false;
        }

        public void OnBeginDrag()
        {
            m_isDragged = true;
            Select();
        }

        public void OnEndDrag()
        {
            m_isDragged = false;
            Deselect();
            if (m_isPointerOnUI == false && m_hasFocus == false)
            {
                UINormalize?.Invoke(this, new SelectedUIEventArgs(this));
            }
        }
    }

}
