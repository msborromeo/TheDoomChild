using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Narrative
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_entryTitle;
        [SerializeField] private TutorialEntryUI m_entryUI;
        [SerializeField] private HorizontalLayoutGroup m_bulletSection;
        [SerializeField] private Image m_bulletPoint;

        private TutorialEntry[] m_entryInfos;
        private List<GameObject> m_bullets = new List<GameObject>();
        private int pageIndex;

        [ShowInInspector, BoxGroup("TEST DATA"), SerializeField] private TutorialData m_testData;

        [Button]
        public void SetEntry(TutorialData data)
        {
            pageIndex = 0;
            m_entryTitle.text = data.entryTitle;
            m_entryInfos = data.entrySections;
            AddBullet(m_bulletPoint.gameObject, 0);


            m_bullets.Add(m_bulletPoint.gameObject);
            for (int i = 1; i < m_entryInfos.Length; i++)
            {
                var bullet = Instantiate(m_bulletPoint, m_bulletSection.transform).gameObject;
                AddBullet(bullet, i);
            }
            Display();

            //line below for debugging only
            gameObject.GetComponent<UIView>().Show();
        }

        private void AddBullet(GameObject bullet, int number)
        {
            bullet.name = $"Image - SectionBullet ({number})";
            m_bullets.Add(bullet);
        }

        public void Display()
        {
            m_entryUI.Display(m_entryInfos[pageIndex]);
        }

        public void Previous()
        {
            if (pageIndex < 1)
                return;
            pageIndex--;

            Display();
        }

        public void Next()
        {
            if (pageIndex == m_entryInfos.Length - 1)
                return;
            pageIndex++;

            Display();
        }

    }
}
