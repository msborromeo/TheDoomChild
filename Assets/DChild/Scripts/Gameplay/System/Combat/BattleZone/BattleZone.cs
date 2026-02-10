using DarkTonic.MasterAudio;
using DChild.Gameplay.Characters.AI;
using DChild.Gameplay.Combat.BattleZoneComponents;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.Gameplay.Combat
{
    public class BattleZone : MonoBehaviour
    {
        [SerializeField, ListDrawerSettings(NumberOfItemsPerPage = 1), TabGroup("Waves")]
        private WaveInfo[] m_waves;
        [SerializeField, TabGroup("Battle Over")]
        private UnityEvent m_onBattleOver;

        private SpawnHandle m_spawnHandle;

        [ShowInInspector, ReadOnly, HideInEditorMode]
        private int m_entityCount;
        [ShowInInspector, ReadOnly, HideInEditorMode]
        private int m_waveIndex;
        [ShowInInspector, ReadOnly, HideInEditorMode]
        private bool m_spawnEnded;
        private bool m_noMoreWaves;



        private void OnSpawn(object sender, EventActionArgs<GameObject> eventArgs)
        {
            m_entityCount++;
            var isntance = eventArgs.info;
			if(isntance.GetComponent<Damageable>()){
				isntance.GetComponent<Damageable>().Destroyed += OnEntityDestroyed;
			}
			else{
				isntance.GetComponentInChildren<Damageable>().Destroyed += OnEntityDestroyed;
			}
            
            if (isntance.TryGetComponent(out ICombatAIBrain brain))
            {
                var player = GameplaySystem.playerManager.player;
                brain.SetTarget(player.damageableModule, player.character);
            }
            if (isntance.TryGetComponent(out IBattleZoneAIBrain battleZoneBrain))
            {
                battleZoneBrain.SwitchToBattleZoneAI();
            }
        }

        private void OnEntityDestroyed(object sender, EventActionArgs eventArgs)
        {
            m_entityCount--;
            if (m_spawnEnded)
            {
                if (m_entityCount <= m_waves[m_waveIndex].numberOfEnemiesToNextWave)
                {
                    NextWave();
                }

                if (m_noMoreWaves && m_entityCount == 0)
                {
                    m_onBattleOver?.Invoke();
                }
            }
            ((Damageable)sender).Destroyed -= OnEntityDestroyed;
        }

        private void OnSpawnEnd(object sender, EventActionArgs eventArgs)
        {
            m_spawnEnded = true;
            if (m_entityCount <= m_waves[m_waveIndex].numberOfEnemiesToNextWave)
            {
                NextWave();
            }
        }

        private void NextWave()
        {
            if (m_waveIndex < m_waves.Length - 1)
            {
                m_waveIndex++;
                var waveInfo = m_waves[m_waveIndex];
                
                m_spawnHandle.Initialize(waveInfo.spawnInfo, waveInfo.waveStartDelay);
                m_spawnEnded = false;
            }
            else
            {
                m_noMoreWaves = true;
                enabled = false;
            }
        }

        private void Awake()
        {
            m_spawnHandle = new SpawnHandle();
            m_waveIndex = 0;            
            var waveInfo = m_waves[m_waveIndex];
            m_spawnHandle.Initialize(waveInfo.spawnInfo, waveInfo.waveStartDelay);

            m_spawnHandle.EntitiesFinishSpawning += OnSpawnEnd;
            m_spawnHandle.EntitySpawned += OnSpawn;
            enabled = false;
            m_noMoreWaves = false;
        }

        private void OnDisable()
        {
            m_spawnHandle.EntitiesFinishSpawning -= OnSpawnEnd;
            m_spawnHandle.EntitySpawned -= OnSpawn;
        }



        public void Update()
        {
            m_spawnHandle.Update(this, GameplaySystem.time.deltaTime);
        }
    }
}