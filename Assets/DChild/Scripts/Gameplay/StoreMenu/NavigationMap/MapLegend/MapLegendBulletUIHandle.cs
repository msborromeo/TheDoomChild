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
        private int pageIndex = 0;

        public void SetupBullets()
        {
            ResetBullets();
            AddBullet(m_bulletPoint, 0);
            for (int i = 1; i < (m_legendUI.listUI.legendEntries.Count / m_maxEntriesPerSection) + 1; i++)
            {
                var bullet = Instantiate(m_bulletPoint, m_bulletSection.transform).gameObject;
                Image bulletImage = bullet.GetComponent<Image>();

                bulletImage.color = new Color32(53, 52, 53, 255);
                AddBullet(bulletImage, i);
            }

            HighlightActiveBullet();
        }

        private void HighlightActiveBullet() => m_bullets[pageIndex].color = new Color32(253, 215, 32, 255);

        private void AddBullet(Image bullet, int number)
        {
            bullet.name = $"Image - BulletPoint ({number + 1})";
            m_bullets.Add(bullet);
        }

        //public void Previous()
        //{
        //    m_bullets[pageIndex].color = new Color32(28, 50, 58, 255);
        //    pageIndex--;
        //    HighlightActiveBullet();
        //}

        public void Next()
        {
            m_bullets[pageIndex].color = new Color32(28, 50, 58, 255);
            pageIndex = pageIndex != (m_bullets.Count - 1) ? pageIndex++ : 0;
            HighlightActiveBullet();
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
