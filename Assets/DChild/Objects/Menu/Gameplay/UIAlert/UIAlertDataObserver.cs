using DChild.Gameplay;
using DChild.Gameplay.UI;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{
    public abstract class UIAlertDataObserver : UIAlertElement
    {
        [SerializeField] private bool m_willUpdateObservables;

        [SerializeField,ValueDropdown("GetAllObservables",IsUniqueList = true)]
        private UIAlertIconBase[] m_toObserve;

        protected UIAlertManager UIAlertManager => GameplaySystem.gamplayUIHandle.alertManager;
        
        private IEnumerable GetAllObservables()
        {
            var rootParent = transform.root;

            Func<Transform, string> getPath = null;
            getPath = x => (x ? getPath(x.parent) + "/" + x.gameObject.name : "");
            return rootParent.GetComponentsInChildren<UIAlertIconBase>(true).Select(x => new ValueDropdownItem(getPath(x.transform), x));
        }
        
        public void UpdateState()
        {
            hasAlert = HasAlert();
            if(m_willUpdateObservables)
            {
                foreach (var alertIcon in m_toObserve)
                {
                    alertIcon.UpdateState();
                }
            }
        }

        private void OnAlertRenderedUseless(object sender, EventActionArgs eventArgs)
        {
            UpdateState();
        }

        private void Awake()
        {
            for (int i = 0; i < m_toObserve.Length; i++)
            {
                m_toObserve[i].RenderedUseless += OnAlertRenderedUseless;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_toObserve.Length; i++)
            {
                m_toObserve[i].RenderedUseless -= OnAlertRenderedUseless;
            }
        }

    }
}
