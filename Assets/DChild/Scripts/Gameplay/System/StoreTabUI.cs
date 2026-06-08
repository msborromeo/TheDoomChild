using UnityEngine;

namespace DChild.Gameplay.Systems
{
    public class StoreTabUI : MonoBehaviour
    {
        [SerializeField] private StoreNavigator m_navigator;
        [SerializeField] private StorePage m_page;

        public void UpdateStorePage()
        {
            m_navigator.OnStoreTabClicked.Invoke(m_page);
            m_navigator.SetPage(m_page);
        }
    }
}