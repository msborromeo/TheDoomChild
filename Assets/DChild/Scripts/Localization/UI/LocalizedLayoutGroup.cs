using UnityEngine;
using I2.Loc;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections;

namespace DChild.Localization
{
    [RequireComponent(typeof(LayoutGroup))]
    public class LocalizedLayoutGroup : MonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly]
        private Localize[] m_toConnect;
        private LayoutGroup m_layoutGroup;
        private ContentSizeFitter m_contentFitter;

        private void EnableLayoutGroup()
        {
            StopAllCoroutines();
            StartCoroutine(QuickToggleLayoutGroup());
        }

        private void SetContentFitterActive(bool active)
        {
            if (m_contentFitter == null)
                return;

            m_contentFitter.enabled = active;
        }

        private IEnumerator QuickToggleLayoutGroup()
        {
            SetContentFitterActive(true);
            m_layoutGroup.enabled = true;
            yield return new WaitForSecondsRealtime(0.5f);
            SetContentFitterActive(false);
            m_layoutGroup.enabled = false;
        }

        private void Awake()
        {
            m_layoutGroup = GetComponent<LayoutGroup>();
            m_contentFitter = GetComponent<ContentSizeFitter>();
            foreach (var localize in m_toConnect)
            {
                localize.LocalizeEvent.AddListener(EnableLayoutGroup);
            }
            StopAllCoroutines();
            StartCoroutine(QuickToggleLayoutGroup());
        }

        private void OnDestroy()
        {
            foreach (var localize in m_toConnect)
            {
                localize.LocalizeEvent.RemoveListener(EnableLayoutGroup);
            }
        }
    }
}
