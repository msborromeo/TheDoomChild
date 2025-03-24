using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay.Pooling;
using DChild.Gameplay.Characters.AI;
using Sirenix.OdinInspector;
using Holysoft.Event;
using DChild.Gameplay.Combat;

namespace DChild.Gameplay.Characters.Enemies
{
    public class ChasingGroundTentacleAttack : MonoBehaviour, IEyeBossAttacks
    {
        public bool m_showOldTentacleVariables;

        [SerializeField,ShowIf("m_showOldTentacleVariables")]
        private GameObject m_groundChaseTentaclesOne;
        [SerializeField, ShowIf("m_showOldTentacleVariables")]
        private GameObject m_groundChaseTentaclesTwo;
        [SerializeField, ShowIf("m_showOldTentacleVariables")]
        private float m_tentacleEmergeInterval;
        [SerializeField, ShowIf("m_showOldTentacleVariables")]
        private float m_timeBeforeTentacleRetract;
        [SerializeField, ShowIf("m_showOldTentacleVariables")]
        private float m_chasingGroundTentacleAnimationSpeedMultiplier;
        [SerializeField, ShowIf("m_showOldTentacleVariables")]
        private StateHandle<AttackStyle> m_currentAttackState;
        [SerializeField]
        private float m_blastColliderTimer;
        [SerializeField]
        private List<ChasingGroundTentacle> m_singleGroundTentacle;
        [SerializeField]
        private List<Attacker> m_attacker;
       

        public event EventAction<EventActionArgs> AttackStart;
        public event EventAction<EventActionArgs> AttackDone;

        public event EventAction<EventActionArgs> HasDamageTarget;
        
        private void Start()
        {
            for(int i = 0; i < m_groundChaseTentaclesOne.transform.childCount; i++)
            {
                GameObject spawnPoint = m_groundChaseTentaclesOne.transform.GetChild(i).gameObject;
                spawnPoint.transform.GetChild(0).GetComponent<ChasingGroundTentacle>().chasingGroundTentacleAnimationSpeedMultiplier = m_chasingGroundTentacleAnimationSpeedMultiplier;
            }

            for (int i = 0; i < m_groundChaseTentaclesTwo.transform.childCount; i++)
            {
                GameObject spawnPoint = m_groundChaseTentaclesTwo.transform.GetChild(i).gameObject;
                spawnPoint.transform.GetChild(0).GetComponent<ChasingGroundTentacle>().chasingGroundTentacleAnimationSpeedMultiplier = m_chasingGroundTentacleAnimationSpeedMultiplier;
            }
        }

        private enum AttackStyle
        {
            Chase,
            GardenVariationOne,
            GardenVariationTwo,
        }

        public IEnumerator ExecuteAttack()
        {
          //  AttackStart?.Invoke(this, EventActionArgs.Empty);
            var rollAttack = Random.Range(1, 4);

            switch (rollAttack)
            {
                case 1:
                    m_currentAttackState.SetState(AttackStyle.Chase);
                    for (int i = 0; i < m_groundChaseTentaclesOne.transform.childCount; i++)
                    {
                        GameObject spawnPoint = m_groundChaseTentaclesOne.transform.GetChild(i).gameObject;
                        spawnPoint.transform.GetChild(0).GetComponent<ChasingGroundTentacle>().ErectTentacle();
                        yield return new WaitForSeconds(m_tentacleEmergeInterval);
                    }

                    yield return new WaitForSeconds(m_timeBeforeTentacleRetract);

                    for (int c = 0; c < m_groundChaseTentaclesOne.transform.childCount; c++)
                    {
                        GameObject spawnPoint = m_groundChaseTentaclesOne.transform.GetChild(c).gameObject;
                        spawnPoint.transform.GetChild(0).GetComponent<ChasingGroundTentacle>().RetractTentacle();
                    }                    
                    break;
                case 2:
                    m_currentAttackState.SetState(AttackStyle.GardenVariationOne);
                    for (int i = 0; i < m_groundChaseTentaclesOne.transform.childCount; i++)
                    {
                        GameObject spawnPoint = m_groundChaseTentaclesOne.transform.GetChild(i).gameObject;
                        spawnPoint.transform.GetChild(0).GetComponent<ChasingGroundTentacle>().ErectTentacle();
                    }

                    yield return new WaitForSeconds(m_timeBeforeTentacleRetract);

                    for (int c = 0; c < m_groundChaseTentaclesOne.transform.childCount; c++)
                    {
                        GameObject spawnPoint = m_groundChaseTentaclesOne.transform.GetChild(c).gameObject;
                        spawnPoint.transform.GetChild(0).GetComponent<ChasingGroundTentacle>().RetractTentacle();
                    }
                    break;
                case 3:
                    m_currentAttackState.SetState(AttackStyle.GardenVariationTwo);
                    for (int i = 0; i < m_groundChaseTentaclesTwo.transform.childCount-1; i++)
                    {
                        GameObject spawnPoint = m_groundChaseTentaclesOne.transform.GetChild(i).gameObject;
                        spawnPoint.transform.GetChild(0).GetComponent<ChasingGroundTentacle>().ErectTentacle();
                    }

                    yield return new WaitForSeconds(m_timeBeforeTentacleRetract);

                    for (int c = 0; c < m_groundChaseTentaclesOne.transform.childCount-1; c++)
                    {
                        GameObject spawnPoint = m_groundChaseTentaclesOne.transform.GetChild(c).gameObject;
                        spawnPoint.transform.GetChild(0).GetComponent<ChasingGroundTentacle>().RetractTentacle();
                    }
                    break;
                default:
                    break;
            }

          //  AttackDone?.Invoke(this, EventActionArgs.Empty);
        }

        public IEnumerator ExecuteAttack(Vector2 PlayerPosition)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator DelayTentacleSpawn()
        {
            
            for (int i = 0; i < m_singleGroundTentacle.Count; i++)
            {
                m_singleGroundTentacle[i].GetComponentInChildren<CapsuleCollider2D>().enabled = true;
                m_singleGroundTentacle[i].GetComponentInChildren<Attacker>().TargetDamaged += ChasingGroundTentacleAttack_TargetDamaged;
                m_singleGroundTentacle[i].ErectTentacle();
                yield return new WaitForSeconds(m_blastColliderTimer);
                m_singleGroundTentacle[i].GetComponentInChildren<CapsuleCollider2D>().enabled = false;
                m_singleGroundTentacle[i].GetComponentInChildren<Attacker>().TargetDamaged -= ChasingGroundTentacleAttack_TargetDamaged;
                // yield return null;
            }

        }

        private void ChasingGroundTentacleAttack_TargetDamaged(object sender, CombatConclusionEventArgs eventArgs)
        {
            HasDamageTarget?.Invoke(this, EventActionArgs.Empty);
            Debug.Log("Got hit by chasing ground blast");
        }

        public IEnumerator DelayTentacleSpawnReverse()
        {
            for (int i = m_singleGroundTentacle.Count - 1; i >= 0; i--)
            {
                m_singleGroundTentacle[i].GetComponentInChildren<CapsuleCollider2D>().enabled = true;
                m_singleGroundTentacle[i].GetComponentInChildren<Attacker>().TargetDamaged += ChasingGroundTentacleAttack_TargetDamaged;
                m_singleGroundTentacle[i].ErectTentacle();
                yield return new WaitForSeconds(m_blastColliderTimer);
                m_singleGroundTentacle[i].GetComponentInChildren<Attacker>().TargetDamaged -= ChasingGroundTentacleAttack_TargetDamaged;

                m_singleGroundTentacle[i].GetComponentInChildren<CapsuleCollider2D>().enabled = false;
            }
        }
        [Button]
        public void TentacleGroundSpikeReverse()
        {
            StartCoroutine(DelayTentacleSpawnReverse());
        }
        [Button]
        public void TentacleGroundSpikes()
        {
            StartCoroutine(DelayTentacleSpawn());
        }
        [Button]
        private void GardenAttack()
        {
            for (int i = 0; i < m_groundChaseTentaclesOne.transform.childCount; i++)
            {
                //m_groundChaseTentaclesOne.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<ChasingGroundTentacle>().ErectTentacle();
                GameObject spawnPoint = m_groundChaseTentaclesOne.transform.GetChild(i).gameObject;
                spawnPoint.transform.GetChild(0).GetComponent<ChasingGroundTentacle>().ErectTentacle();
            }

            for (int c = 0; c < m_groundChaseTentaclesOne.transform.childCount; c++)
            {
                m_groundChaseTentaclesOne.transform.GetChild(c).gameObject.transform.GetChild(c).GetComponent<ChasingGroundTentacle>().ErectTentacle();
            }
        }

        public IEnumerator ExecuteAttack(AITargetInfo Target)
        {
            throw new System.NotImplementedException();
        }
    }
}

