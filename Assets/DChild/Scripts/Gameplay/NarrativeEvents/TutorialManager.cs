using Holysoft.Event;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Narrative
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField]
        private TutorialData m_tutorial;

        [SerializeField, BoxGroup("UI")]
        private TutorialUI m_tutorialUI;


        private void OnSelected(object sender, EventActionArgs eventArgs)
        {
            var info = (TutorialEntry)sender;
        }

        private void Start()
        {
            //m_infos = GetComponentsInChildren<TutorialInfo>();
            //for (int i = 0; i < m_infos.Length; i++)
            //{
            //    //m_infos[i].Selected += OnSelected;
            //}
        }

        private void OnDestroy()
        {
            //    for (int i = 0; i < m_infos.Length; i++)
            //    {
            //      m_infos[i].Selected -= OnSelected;
            //    }
        }
    }
}
