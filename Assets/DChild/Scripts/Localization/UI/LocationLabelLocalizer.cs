using DChild.Gameplay.Environment;
using TMPro;
using UnityEngine;
using I2.Loc;
using Sirenix.OdinInspector;

namespace DChild.Localization
{
    [RequireComponent(typeof(ILocationLabelInjector))]
    public class LocationLabelLocalizer : MonoBehaviour
    {
        public enum Type
        {
            Direct,
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


        private void OnUpdate(TextMeshProUGUI uGUI, Location location)
        {
            var toTerm = "Location/" + location.ToString().Replace('_', ' ');
            switch (m_type)
            {
                case Type.Direct:
                    uGUI.text = LocalizationManager.GetTermTranslation(toTerm);
                    break;
                case Type.Term:
                    m_localizer.mTerm = toTerm;
                    m_localizer.OnLocalize(true);
                    break;
                case Type.Params:
                    m_paramsManager.SetParameterValue(m_paramsVariable, toTerm, true);
                    break;
            }

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