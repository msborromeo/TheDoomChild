using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventoryConditionalSpritesUI : MonoBehaviour
    {
        [BoxGroup("SPRITES"),SerializeField] private Sprite[] m_healthSprites;
        [BoxGroup("SPRITES"), SerializeField] private Sprite[] m_shadowSprites;
        [BoxGroup("SPRITES"), SerializeField] private Sprite[] m_keystoneSprites;
        
        [BoxGroup("ITEMS TO AVOID DISPLAY"), SerializeField] private ItemData[] m_itemDataList;

        // Returning the direct reference
        public Sprite[] HealthShardSprites => m_healthSprites;
        public Sprite[] ShadowShardSprites => m_shadowSprites;
        public Sprite[] KeystoneSprites => m_keystoneSprites;

        public ItemData[] ItemsToAvoid => m_itemDataList;
    }
}