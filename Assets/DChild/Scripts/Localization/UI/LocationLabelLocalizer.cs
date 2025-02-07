using DChild.Gameplay.Environment;
using TMPro;
using UnityEngine;
using I2.Loc;
using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;

namespace DChild.Localization
{
    [RequireComponent(typeof(ILocationLabelInjector))]
    public class LocationLabelLocalizer : MonoBehaviour
    {
        public enum Type
        {
            Term,
            Params
        }

        [SerializeField]
        private Type m_type;

        [SerializeField, ShowIf("@m_type == Type.Term")]
        private Localize m_localizer;

        [SerializeField, ShowIf("@m_type == Type.Params")]
        private LocalizationParamsManager m_paramsManager;
        [SerializeField, ShowIf("@m_type == Type.Params")]
        private string m_paramsVariable;

        private ILocationLabelInjector m_injector;
        private string m_currentTerm;

        private TextMeshProUGUI m_uGUI;
        private Location m_currentLocation;

        private void OnUpdate(TextMeshProUGUI uGUI, Location location)
        {
            m_uGUI = uGUI;
            m_currentLocation = location;
            var toTerm = LocalizationUtility.GetTermKey(location);
            switch (m_type)
            {
                case Type.Term:
                    m_localizer.SetTerm(toTerm);
                    break;
                case Type.Params:
                    m_paramsManager.SetParameterValue(m_paramsVariable, toTerm, true);
                    break;
            }
            m_currentTerm = toTerm;
        }

        private void Awake()
        {
            m_injector = GetComponent<ILocationLabelInjector>();
            m_injector.LocationLabelUpdated += OnUpdate;
        }

        private void OnDestroy()
        {
            m_injector.LocationLabelUpdated -= OnUpdate;
        }
    }
}