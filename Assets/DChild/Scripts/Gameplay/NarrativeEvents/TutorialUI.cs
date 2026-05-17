using DChild.Codex.Tutorial;
using Doozy.Runtime.Signals;
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

        [BoxGroup("Navigation Buttons"), SerializeField] private GameObject m_prevButton;
        [BoxGroup("Navigation Buttons"), SerializeField] private GameObject m_nextButton;
        [BoxGroup("Navigation Buttons"), SerializeField] private GameObject m_backButton;

        private TutorialCodexData[] m_entryInfos;
        private List<Image> m_bullets = new();
        private int pageIndex = 0;


        [Button]
        public void SetEntry(TutorialData data)
        {
            if (data == null)
                return;

            Reset();

            m_entryTitle.text = data.entryTitle;
            m_entryInfos = data.entrySections;
            SetupBullets();
            Display();
        }

        private void SetupBullets()
        {
            ResetBullets();

            for (int i = 0; i < m_entryInfos.Length; i++)
            {
                var bullet = Instantiate(m_bulletPoint, m_bulletSection.transform).gameObject;
                Image bulletImage = bullet.GetComponent<Image>();

                bulletImage.color = i == 0 ? new Color32(253, 215, 32, 255) : new Color32(28, 50, 58, 255);
                AddBullet(bulletImage, i);
            }
        }

        private void AddBullet(Image bullet, int number)
        {
            bullet.name = $"Image - SectionBullet ({number + 1})";
            m_bullets.Add(bullet);
        }

        private void UpdateUIElements()
        {
            m_bullets[pageIndex].color = new Color32(253, 215, 32, 255);
            m_prevButton.gameObject.SetActive(pageIndex > 0);
            m_nextButton.gameObject.SetActive(pageIndex < m_entryInfos.Length - 1);

            if (m_nextButton.gameObject.activeSelf == false)
                m_backButton.SetActive(true);

        }

        public void Display()
        {
            UpdateUIElements();
            m_entryUI.Display(m_entryInfos[pageIndex]);
        }

        public void Previous()
        {
            m_bullets[pageIndex].color = new Color32(28, 50, 58, 255);
            pageIndex--;
            Display();
        }

        public void Next()
        {
            m_bullets[pageIndex].color = new Color32(28, 50, 58, 255);
            pageIndex++;
            Display();
        }

        private void ResetBullets()
        {
            if (m_bullets == null) return;

            for (int i = 0; i < m_bullets.Count; i++)
            {
                if (m_bullets[i] != null)
                {
                    Destroy(m_bullets[i].gameObject);
                }
            }

            m_bullets.Clear();
        }

        private void Reset()
        {
            m_backButton.SetActive(false);
            pageIndex = 0;
            m_entryTitle.text = "";
            m_entryInfos = null;

            ResetBullets();
        }
    }
}
