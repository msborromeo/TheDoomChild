using DChild.Gameplay.Characters.Player.Skins;
using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.UI.PlayerStats
{
    public class PlayerSkinsUIManager : MonoBehaviour
    {
        [SerializeField, BoxGroup("NAVIGATION")] private PlayerSkinsNavigationUI m_navigationUI;

        [SerializeField] private TextMeshProUGUI m_skinNameLabel;

        [SerializeField] private MeshRenderer m_currentSkinModel;

        private PlayerSkinHandle m_skinHandle;
        public PlayerSkinHandle skinHandle => m_skinHandle;


        public void Initialize()
        {
            SyncPlayerDataToUI();

            ApplySkinGraphic(m_skinHandle.currentSkin);
            m_navigationUI.SetFullSkinList(m_skinHandle.fullSkinList);
            m_navigationUI.UpdateVisibleSkinSlots(m_skinHandle.acquiredSkins);
        }

        public void OnCurrentSkinUpdated(object sender, PlayerSkinArgs eventArgs)
        {
            var skinData = eventArgs.data;

            ApplySkinGraphic(skinData);
            m_skinHandle.ApplySkin(skinData);
        }

        private void UpdateUI(SkinData data)
        {
            m_skinNameLabel.text = data.name;

        }

        private void SyncPlayerDataToUI() => m_skinHandle = GameplaySystem.playerManager.player.skinHandle;

        [Button]
        private void ApplySkinGraphic(SkinData data) => m_currentSkinModel.material = data.atlasOverrides.material;

        private void Awake() => m_navigationUI.OnCurrentSkinUpdated += OnCurrentSkinUpdated;

        private void OnDisable() => m_navigationUI.OnCurrentSkinUpdated -= OnCurrentSkinUpdated;
    }
}