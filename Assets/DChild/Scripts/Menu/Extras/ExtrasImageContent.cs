using DChild.Menu.Extras;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Extras
{
    public class ExtrasImageContent : MonoBehaviour
    {
        [SerializeField]
        private SpriteList m_list;
        [SerializeField]
        private ExtrasImageShowcase m_showcase;
        private readonly HashSet<ExtrasItem> m_items = new HashSet<ExtrasItem>();
        private readonly List<ExtrasItem> m_buffer = new List<ExtrasItem>();
        private readonly List<ExtrasItem> m_removed = new List<ExtrasItem>();

        private void Awake() => Initialize(m_showcase);

        internal void Initialize(ExtrasImageShowcase fallback)
        {
            if (m_showcase == null) m_showcase = fallback;
            m_buffer.Clear();
            GetComponentsInChildren(true, m_buffer);
            m_removed.Clear();
            foreach (var item in m_items)
                if (item == null || !m_buffer.Contains(item)) m_removed.Add(item);
            foreach (var item in m_removed)
            {
                if (item != null) item.Selected -= OnItemSelected;
                m_items.Remove(item);
            }
            for (int i = 0; i < m_buffer.Count; i++)
            {
                var item = m_buffer[i];
                item.SetIndex(i);
                if (!m_items.Add(item)) continue;
                item.Selected += OnItemSelected;
                var image = item.GetComponent<Image>();
                if (CanShow(item) && image != null && image.sprite == null)
                {
                    image.sprite = m_list.GetSprite(i);
                    image.preserveAspect = true;
                }
            }
        }

        internal bool CanShow(ExtrasItem item) => m_list != null && item != null &&
            item.itemIndex >= 0 && item.itemIndex < m_list.count;

        private void OnItemSelected(object sender, ItemSelectedEventArgs eventArgs)
        {
            if (m_showcase != null && CanShow(sender as ExtrasItem))
                m_showcase.Showcase(m_list, eventArgs.itemIndex);
        }

        private void OnDestroy()
        {
            foreach (var item in m_items) if (item != null) item.Selected -= OnItemSelected;
            m_items.Clear();
        }
    }
}
