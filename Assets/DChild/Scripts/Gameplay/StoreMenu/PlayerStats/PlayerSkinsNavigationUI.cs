using DChild.Gameplay.Characters.Player.Skins;
using DChild.Gameplay.Items;
using Holysoft.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.UI.PlayerStats
{
    public class PlayerSkinsNavigationUI : MonoBehaviour
    {
        [SerializeField] private List<SkinToggleUI> m_skinSlotToggles;
        [SerializeField] private TextMeshProUGUI m_skinIndex;


        private FullSkinList m_fullSkinList;
        private List<SkinData> m_acquiredSkins;
        private int[] m_skinIDs;

        private List<SkinData> m_ownedSkinsData = new List<SkinData>();
        private SkinData m_currentSkin;

        private int m_navigationIndex = 0;

        public EventAction<PlayerSkinArgs> OnCurrentSkinUpdated;

        #region Setters
        public void SetAcquiredSkins(List<SkinData> value) => m_acquiredSkins = value;
        public void SetCurrentSkin(SkinData value) => m_currentSkin = value;
        public void SetFullSkinList(FullSkinList value) => m_fullSkinList = value;
        #endregion

        #region Data Display
        public void InitializeMenu(List<SkinData> acquiredSkins)
        {
            m_skinIDs = m_fullSkinList.GetIDs();
            m_ownedSkinsData.Clear();

            foreach (int id in m_skinIDs)
            {
                var owned = acquiredSkins.Find(s => s.id == id);
                if (owned != null)
                    m_ownedSkinsData.Add(m_fullSkinList.GetInfo(id));
            }

            if (m_currentSkin != null)
            {
                int foundIndex = m_ownedSkinsData.FindIndex(s => s.id == m_currentSkin.id);

                if (foundIndex != -1)
                    m_navigationIndex = foundIndex;
            }

            RefreshUISlots();
        }

        private void RefreshUISlots()
        {
            int middleIndex = m_skinSlotToggles.Count / 2;

            for (int i = 0; i < m_skinSlotToggles.Count; i++)
            {
                int dataIndex = m_navigationIndex + (i - middleIndex);

                if (dataIndex < 0 || dataIndex >= m_ownedSkinsData.Count)
                {
                    m_skinSlotToggles[i].Display(null);
                    continue;
                }
                m_skinSlotToggles[i].Display(m_ownedSkinsData[dataIndex]);
            }

            if (m_skinIndex != null)
                m_skinIndex.text = $"{m_navigationIndex + 1} / 12";
        }
        #endregion

        #region Navigation
        public void Previous()
        {
            if (m_navigationIndex <= 0)
                return;

            m_navigationIndex = (m_navigationIndex - 1 + m_skinIDs.Length) % m_skinIDs.Length;
            RefreshUISlots();
        }

        public void Next()
        {
            if (m_navigationIndex >= m_ownedSkinsData.Count - 1)
                return;

            m_navigationIndex = (m_navigationIndex + 1) % m_skinIDs.Length;
            RefreshUISlots();
        }
        #endregion

        #region Toggle Select Event Handling
        private void OnToggleSelect(object sender, PlayerSkinArgs eventArgs)
        {
            var selectedSkin = eventArgs.data;

            if (selectedSkin != null)
                OnCurrentSkinUpdated?.Invoke(sender, eventArgs);
        }


        private void ToggleSkinSlotSubscription(bool toggle)
        {
            foreach (var skinToggle in m_skinSlotToggles)
            {
                if (toggle)
                {
                    skinToggle.OnToggleSelected += OnToggleSelect;
                    continue;
                }
                skinToggle.OnToggleSelected -= OnToggleSelect;
            }
        }

        private void Awake() => ToggleSkinSlotSubscription(true);

        private void OnDisable() => ToggleSkinSlotSubscription(false);
        #endregion
    }
}