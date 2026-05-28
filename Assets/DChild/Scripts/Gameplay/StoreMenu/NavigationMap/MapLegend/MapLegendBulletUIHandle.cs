using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.NavigationMap.MapLegend
{
    public class MapLegendBulletUIHandle : MonoBehaviour
    {
        [SerializeField] private MapLegendUI m_legendUI;
        [SerializeField] private Image m_bulletPoint;
        [SerializeField] private HorizontalLayoutGroup m_bulletSection;
        [SerializeField] private int m_maxEntriesPerSection;

        private List<Image> m_bullets = new();
        private int m_pageIndex = 0;

        public Action<int> OnPageChange;

        public void SetupBullets()
        {
            ResetBullets();

            int totalEntries = m_legendUI.listUI.legendEntries.Count;
            var pageLimit = m_legendUI.listUI.entryLimit;

            if (totalEntries == 0) return;

            int totalPages = (totalEntries + pageLimit - 1) / pageLimit;

            for (int i = 0; i < totalPages; i++)
            {
                var bullet = Instantiate(m_bulletPoint, m_bulletSection.transform).gameObject;
                Image bulletImage = bullet.GetComponent<Image>();

                bulletImage.color = new Color32(53, 52, 53, 255);
                AddBullet(bulletImage, i);
            }
            HighlightActiveBullet();
        }

        private void HighlightActiveBullet() => m_bullets[m_pageIndex].color = new Color32(253, 215, 32, 255);

        private void AddBullet(Image bullet, int number)
        {
            bullet.name = $"Image - BulletPoint ({number + 1})";
            m_bullets.Add(bullet);
        }

        [Button]
        public void Next()
        {
            if (m_bullets == null || m_bullets.Count == 0) return;

            m_bullets[m_pageIndex].color = new Color32(28, 50, 58, 255);
            m_pageIndex = m_pageIndex != (m_bullets.Count - 1) ? m_pageIndex + 1 : 0;
            HighlightActiveBullet();

            OnPageChange.Invoke(m_pageIndex);
        }

        private void ResetBullets()
        {
            if (m_bullets == null) return;

            for (int i = 0; i < m_bullets.Count; i++)
                if (m_bullets[i] != null)
                    Destroy(m_bullets[i].gameObject);

            m_bullets.Clear();
        }
    }
}
