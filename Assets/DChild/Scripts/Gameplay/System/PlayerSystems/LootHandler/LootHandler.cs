using DChild.Gameplay.Items;
using DChild.Gameplay.Pooling;
using Holysoft;
using Holysoft.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DChild.Gameplay.Systems
{
    public class LootHandler : MonoBehaviour, ILootHandler, IGameplaySystemModule
    {
        [SerializeField, Min(1)]
        private int m_maxLootSpawnPerFrame;
        [SerializeField]
        private RangeFloat m_popVelocityX;
        [SerializeField]
        private RangeFloat m_popVelocityY;

        // Adjust this if there are still lag spikes when dropping loots VV
        float m_requestInterval=0.15f;
        //     ^^
        float m_Timer;

        private List<LootDropRequest> m_requests;
        private Loot m_cachedLoot;

        public void DropLoot(LootDropRequest request)
        {
            m_requests.Add(request);
            enabled = true;
        }

        public void ClearRequests()
        {
            m_requests.Clear();
            enabled = false;
        }

        private void HandleRequests()
        {
            var availableInstanceSpawnCount = m_maxLootSpawnPerFrame;
            do
            {
                var request = m_requests[0];
                availableInstanceSpawnCount -= HandleRequest(ref request, availableInstanceSpawnCount);
                if (request.count == 0)
                {
                    m_requests.RemoveAt(0);
                    if (m_requests.Count == 0)
                    {
                        break;
                    }
                }
                else
                {
                    m_requests[0] = request;
                }
            } while (availableInstanceSpawnCount > 0);
        }

        private int HandleRequest(ref LootDropRequest request, int maxInstance)
        {
            var requestCount = request.count;
            int instanceToCreate = requestCount >= maxInstance ? maxInstance : requestCount;
            var pool = GameSystem.poolManager.GetPool<PoolableObjectPool>();
            for (int i = 0; i < instanceToCreate; i++)
            {
                m_cachedLoot = pool.GetOrCreateItem(request.loot).GetComponent<Loot>();
                m_cachedLoot.SpawnAt(request.location, Quaternion.identity);
                m_cachedLoot.Pop(GetRandomPopVelocity());
                if (m_cachedLoot.TryGetComponent<ItemLootChanger>(out ItemLootChanger lootItem))
                {
                    lootItem.SetData(request.data);
                }
            }
            request.count -= instanceToCreate;
            m_cachedLoot = null;
            return instanceToCreate;
        }

        private Vector2 GetRandomPopVelocity()
        {
            var xVelocity = transform.right * MathfExt.RandomSign() * m_popVelocityX.GenerateRandomValue();
            var yVelocity = transform.up * m_popVelocityY.GenerateRandomValue();
            return xVelocity + yVelocity;
        }

        private void Awake()
        {
            m_requests = new List<LootDropRequest>();
            enabled = false;
        }

        private void Update()
        {
            if(m_Timer<=0)
            {
                HandleRequests();
                m_Timer = m_requestInterval;
            }
            m_Timer -= Time.deltaTime;
            if (m_requests.Count <= 0)
            {
                enabled = false;
            }
        }
    }
}