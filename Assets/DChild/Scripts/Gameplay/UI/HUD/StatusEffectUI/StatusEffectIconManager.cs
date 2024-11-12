using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Combat.StatusAilment.UI
{
    public class StatusEffectIconManager : SerializedMonoBehaviour
    {
        [SerializeField]
        private LayoutGroup m_layoutGroup;
        [SerializeField]
        private Dictionary<StatusEffectType, StatusEffectIconData> m_statusEffectIconDataPair;

        [SerializeField]
        private List<StatusEffectIcon> m_icons;

        public void ShowIconFor(StatusEffectType type, StatusEffectReciever statusEffectReciever)
        {

            for (int i = 0; i < m_icons.Count; i++)
            {
                var icon = m_icons[i];
                if (icon.enabled == false)
                {
                    icon.Monitor(statusEffectReciever.GetInfo(type), m_statusEffectIconDataPair[type]);
                    StopAllCoroutines();
                    StartCoroutine(AutoLayoutIcons());
                    return;
                }
            }

            Debug.LogWarning("Not Enough Status Effect Icons");
        }

        public void HideIconFor(StatusEffectType type, StatusEffectReciever statusEffectReciever)
        {
            for (int i = 0; i < m_icons.Count; i++)
            {
                var icon = m_icons[i];
                if (icon.type == type)
                {
                    icon.Hide();
                    StopAllCoroutines();
                    StartCoroutine(AutoLayoutIcons());
                }
            }
        }

        public void HideAllIcons()
        {
            for (int i = 0; i < m_icons.Count; i++)
            {
                m_icons[i].Hide();
            }
        }

        private IEnumerator AutoLayoutIcons()
        {
            m_layoutGroup.enabled = false;
            yield return new WaitForSeconds(0.1f);
            m_layoutGroup.enabled = true;
        }

        private void Awake()
        {
            GetComponentsInChildren(true, m_icons);
            /*m_layoutGroup.enabled = false;*/
        }
    }

}