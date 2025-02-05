using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay.Pooling;
using System.Linq;
using DChild.Gameplay.Characters.AI;
using Holysoft.Event;

namespace DChild.Gameplay.Characters.Enemies
{
    public class MonolithSlamAttack : MonoBehaviour, IEyeBossAttacks
    {
        [SerializeField]
        private MonolithSlam m_monolith;
        [SerializeField]
        private Transform m_monolithSlamHeight;
        [SerializeField]
        private int m_numOfMonoliths;
        [SerializeField]
        private float m_timeBeforeSmash;
        [SerializeField]
        private float m_spawnIntervalForMonoliths;
        [SerializeField]
        private float m_spawnOffset;
        [SerializeField]
        private List<float> m_monolithsSpawnedXPositions = new List<float>();
        private ObstacleChecker m_obstacleChecker;

        public List<GameObject> m_PatternOneTentacleSpawn = new List<GameObject>();
        public List<GameObject> m_monolithsSpawned = new List<GameObject>();
        public List<GameObject> monolithsToDestroy;
        public List<GameObject> monolithsToActuallyKeep;
        private bool m_leftToRightSequence;

        public event EventAction<EventActionArgs> AttackStart;
        public event EventAction<EventActionArgs> AttackDone;

        public IEnumerator ExecuteAttack()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator ExecuteAttack(Vector2 PlayerPosition)
        {
            throw new System.NotImplementedException();
        }
        
        public IEnumerator PhaseOneMonolithSlam()
        {
            m_PatternOneTentacleSpawn = m_PatternOneTentacleSpawn.OrderBy(x => Random.value).ToList();

            // Instantiate all monoliths
            foreach (var item in m_PatternOneTentacleSpawn)
            {
                yield return InstantiateAndKeepMonolith(item);
            }

            // Wait before deciding which to keep or destroy
            yield return new WaitForSeconds(2f);

            // Decide how many to keep (1 or 2 randomly)
            int monolithsToKeep = Random.Range(1, 3);

            // Select the monoliths to keep and the rest to destroy
            monolithsToDestroy = m_monolithsSpawned.Skip(monolithsToKeep).ToList();
            monolithsToActuallyKeep = m_monolithsSpawned.Take(monolithsToKeep).ToList();

            var allMonoliths = new List<GameObject>(monolithsToActuallyKeep);
            allMonoliths.AddRange(monolithsToDestroy);
            allMonoliths = allMonoliths.OrderBy(x => Random.value).ToList();

            foreach (var item in allMonoliths)
            {
                if (monolithsToActuallyKeep.Contains(item))
                {
                    item.GetComponent<MonolithSlam>().AttackKeepMonolith();
                }
                else
                {
                    item.GetComponent<MonolithSlam>().AttackDestroyMonolith();
                    m_monolithsSpawned.Remove(item); // Now safe to remove
                }

                yield return new WaitForSeconds(1f);
            }
            // Process "keep" group
            // foreach (var item in monolithsToActuallyKeep)
            // {
            //     item.GetComponent<MonolithSlam>().AttackKeepMonolith();
            //     yield return new WaitForSeconds(1f);
            // }

            // //// Wait between processing groups
            //// yield return new WaitForSeconds(1f);

            // // Process "destroy" group
            // foreach (var item in monolithsToDestroy)
            // {
            //     item.GetComponent<MonolithSlam>().AttackDestroyMonolith();
            //     m_monolithsSpawned.Remove(item); // Now safe to remove
            //     yield return new WaitForSeconds(1f);
            // }

            for (int i = 0; i < m_monolithsSpawned.Count; i++)
            {
               // m_monolithsSpawned[i].GetComponent<MonolithSlam>().enabled = false;
            }
            


            #region stupid code
            //  m_monolithsSpawned.Clear();

            //while (counter < m_numOfMonoliths)
            //{
            //   // yield return SetUpMonoliths(Target);
            //    counter++;
            //}

            //Organize Monoliths to drop in correct order of left to right or right to left
            //if (m_leftToRightSequence)
            //    OrganizeMonolithsSpawnedInDescendingOrder();
            //else
            //    OrganizeMonolithsSpawnedInAscendingOrder();


            //Pick a monolith to keep as platform
            //if (m_monolithsSpawned.Count > 1)
            //{
            //    int rollMonolithToKeep = Random.Range(0, m_monolithsSpawned.Count);

            //    m_monolithsSpawned[rollMonolithToKeep].gameObject.GetComponent<MonolithSlam>().keepMonolith = true;
            //    m_obstacleChecker.AddMonolithToList(m_monolithsSpawned[rollMonolithToKeep]);
            //}

            ////Anticipation time before smashing monoliths
            //yield return new WaitForSeconds(2f);

            ////Set smashMonolith true in each monolith to trigger smash
            //foreach (PoolableObject monolith in m_monolithsSpawned)
            //{
            //    if(monolith != null)
            //        monolith.GetComponent<MonolithSlam>().TriggerSmash();
            //    yield return new WaitForSeconds(m_timeBeforeSmash);
            //}

            //m_monolithsSpawned.Clear();
            //m_monolithsSpawnedXPositions.Clear();

            //AttackDone?.Invoke(this, EventActionArgs.Empty);
            #endregion

            yield return null;
        }
        
        public void RemoveMonolithFromList(GameObject item)
        {
            m_monolithsSpawned.Remove(item);
        }

        private void Awake()
        {
            m_obstacleChecker = FindObjectOfType<ObstacleChecker>();
        }

        private IEnumerator InstantiateAndKeepMonolith(GameObject monolith)
        {
            monolith.SetActive(true);
            monolith.GetComponent<MonolithSlam>().EmergeRoutine();
            m_monolithsSpawned.Add(monolith.gameObject);
            yield return new WaitForSeconds(1f);
        }

        #region brain dead code
        //public IEnumerator SetUpMonoliths(AITargetInfo Target)
        //{          
        //    if (m_monolithsSpawnedXPositions.Contains(Target.position.x))
        //    {
        //        int randomRoll = Random.Range(0, 2);
        //        if (randomRoll == 0)
        //        {
        //            InstantiateMonolith(new Vector2(m_monolithsSpawnedXPositions[m_monolithsSpawnedXPositions.Count - 1] + m_spawnOffset, Target.position.y), m_monolith.gameObject);
        //        }
        //        else
        //        {
        //            InstantiateMonolith(new Vector2(m_monolithsSpawnedXPositions[m_monolithsSpawnedXPositions.Count - 1] + m_spawnOffset, Target.position.y), m_monolith.gameObject);
        //        }
        //    }
        //    else
        //    {
        //        InstantiateMonolith(new Vector2(Target.position.x, Target.position.y), m_monolith.gameObject);
        //    }

        //    //InstantiateMonolith(new Vector2(Target.position.x, Target.position.y), m_monolith.gameObject);
        //    yield return new WaitForSeconds(m_spawnIntervalForMonoliths);
        //}

        //    private void InstantiateMonolith(Vector2 spawnPosition, GameObject monolith)
        // {
        //    var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(monolith, gameObject.scene);
        //    instance.SpawnAt(new Vector2(spawnPosition.x, m_monolithSlamHeight.position.y), Quaternion.identity);
        //    m_monolithsSpawnedXPositions.Add(instance.transform.position.x);
        //  //  m_monolithsSpawned.Add(instance); 
        //}

        #endregion


        public void OrganizeMonolithsSpawnedInDescendingOrder()
        {
            m_monolithsSpawned = m_monolithsSpawned.OrderByDescending(x => x.transform.position.x).ToList();
        }

        public void OrganizeMonolithsSpawnedInAscendingOrder()
        {
            m_monolithsSpawned = m_monolithsSpawned.OrderByDescending(x => x.transform.position.x).ToList();
            m_monolithsSpawned.Reverse();
        }

        public IEnumerator ExecuteAttack(AITargetInfo Target)
        {
            throw new System.NotImplementedException();
        }
    }
}

