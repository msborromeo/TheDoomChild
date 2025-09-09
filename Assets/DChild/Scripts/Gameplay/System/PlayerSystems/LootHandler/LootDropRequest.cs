using DChild.Gameplay.Items;
using UnityEngine;

namespace DChild.Gameplay.Systems
{
    public struct LootDropRequest
    {
        public GameObject loot { get; }
        public int count;
        public Vector2 location { get; }

        public ItemData data { get; }

        public LootDropRequest(GameObject loot, int count, Vector2 location,ItemData data) : this()
        {
            this.loot = loot;
            this.count = count;
            this.location = location;
            this.data = data;
        }


    }
}