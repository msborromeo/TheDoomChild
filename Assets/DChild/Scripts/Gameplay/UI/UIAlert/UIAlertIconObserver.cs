using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Linq;
using System;
using UnityEngine;
using System.Collections;

namespace DChild.Gameplay.UI.Alerts
{

    public class UIAlertIconObserver : UIAlertIconBase
    {
        [SerializeField, ValueDropdown("GetAllObservables", IsUniqueList = true)]
        private UIAlertElement[] m_toObserve;

        public override bool HasAlert()
        {
            for (int i = 0; i < m_toObserve.Length; i++)
            {
                if (m_toObserve[i].HasAlert())
                    return true;
            }

            return false;
        }

        private IEnumerable GetAllObservables()
        {
            var rootParent = transform.root;

            Func<Transform, string> getPath = null;
            getPath = x => (x ? getPath(x.parent) + "/" + x.gameObject.name : "");
            return rootParent.GetComponentsInChildren<UIAlertElement>(true).Select(x => new ValueDropdownItem(getPath(x.transform), x));
        }

        private void OnStateChange(object sender, EventActionArgs eventArgs)
        {
            UpdateState();
        }

        private void Awake()
        {
            for (int i = 0; i < m_toObserve.Length; i++)
            {
                m_toObserve[i].StateChange += OnStateChange;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_toObserve.Length; i++)
            {
                m_toObserve[i].StateChange -= OnStateChange;
            }
        }
    }
}
