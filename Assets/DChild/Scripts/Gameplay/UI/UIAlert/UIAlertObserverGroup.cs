using Sirenix.OdinInspector;
using System.Linq;
using System;
using UnityEngine;
using System.Collections;

namespace DChild.Gameplay.UI.Alerts
{
    public class UIAlertObserverGroup: MonoBehaviour
    {
        [SerializeField, ValueDropdown("GetAllDataObservables", IsUniqueList = true)]
        private UIAlertDataObserver[] m_dataObservables;
        [SerializeField, ValueDropdown("GetAllIconObservables", IsUniqueList = true)]
        private UIAlertIconObserver[] m_iconObservables;

        public void UpdateObserverStates()
        {
            for (int i = 0; i < m_dataObservables.Length; i++)
            {
                m_dataObservables[i].UpdateState();
            }

            for (int i = 0; i < m_iconObservables.Length; i++)
            {
                m_iconObservables[i].UpdateState();
            }
        }

        private IEnumerable GetAllDataObservables()
        {
            var rootParent = transform.root;

            Func<Transform, string> getPath = null;
            getPath = x => (x ? getPath(x.parent) + "/" + x.gameObject.name : "");
            return rootParent.GetComponentsInChildren<UIAlertDataObserver>().Select(x => new ValueDropdownItem(getPath(x.transform), x));
        }

        private IEnumerable GetAllIconObservables()
        {
            var rootParent = transform.root;

            Func<Transform, string> getPath = null;
            getPath = x => (x ? getPath(x.parent) + "/" + x.gameObject.name : "");
            return rootParent.GetComponentsInChildren<UIAlertIconObserver>().Select(x => new ValueDropdownItem(getPath(x.transform), x));
        }
    }
}
