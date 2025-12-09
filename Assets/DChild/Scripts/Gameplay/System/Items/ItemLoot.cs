using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Systems;
using Sirenix.OdinInspector;
using UnityEngine;
using DChild.Gameplay.Pooling;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
namespace DChild.Gameplay.Items
{
    public class ItemLoot : Loot
    {
        [SerializeField, OnValueChanged("OnDataChange")]
        private ItemData m_data;
        [SerializeField]
        private VFXSpawner m_pickupVfx;

#if UNITY_EDITOR
        [SerializeField]
        private bool m_HideLoot = true;
        [SerializeField,HideIf("m_HideLoot")]
        private SpriteRenderer m_spriteRenderer;

        private void OnDataChange()
        {
            if(!m_HideLoot)
            {
                m_spriteRenderer.sprite = m_data.icon;
                gameObject.name = m_data.itemName.Replace(" ", string.Empty) + "Loot";
            }
        }

        [Button, HideInPrefabInstances]
        private void CreateLootReference()
        {
            var lootReference = ScriptableObject.CreateInstance<LootReference>();
            lootReference.Initialize(gameObject);

            var prefabPath = AssetDatabase.GetAssetPath(gameObject);
            var directory = Directory.GetParent(prefabPath);
            var path = $"{directory}\\{gameObject.name.Replace("Loot", string.Empty)}LootReference.asset";
            if (AssetDatabase.LoadAssetAtPath<LootReference>(path) == null)
            {
                AssetDatabase.CreateAsset(lootReference, path);
                AssetDatabase.SaveAssets();
            }
        }
#endif

        public void SetData(ItemData data)
        {
            m_data = data;
#if UNITY_EDITOR
            OnDataChange();
#endif
        }

        public override void PickUp(IPlayer player)
        {
            m_pickedBy = player;
        }

        protected override void ApplyPickUp()
        {
            base.ApplyPickUp();
            if (m_pickedBy.inventory.HasSpaceFor(m_data))
            {
                m_pickedBy.inventory.AddItem(m_data);
                SendNotification();
            }
            else if (m_data is UsableItemData)
            {
                m_pickedBy.inventory.AddItem(m_data);
                SendNotification();
                //((UsableItemData)m_data).Use(m_pickedBy);
            }
            if(m_pickupVfx!=null)
            {
                m_pickupVfx.Spawn();
            }
            DisableEnvironmentCollider();
        }

        private void SendNotification()
        {
            LootList item = new LootList();
            item.Add(m_data, 1);
            //Notify UI of loot chest content
            GameplaySystem.gamplayUIHandle.notificationManager.QueueNotification(item);
        }
    }
}
