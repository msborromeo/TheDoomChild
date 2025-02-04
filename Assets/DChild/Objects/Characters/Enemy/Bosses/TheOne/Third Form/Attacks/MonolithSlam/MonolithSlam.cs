using DChild.Gameplay.Pooling;
using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Enemies
{
    public class MonolithSlam : PoolableObject
    {
        [SerializeField, TabGroup("Reference")]
        protected SpineRootAnimation m_animation;

        [SerializeField, TabGroup("Reference")]
        private SpineEventListener m_spineLister;
        [SerializeField, TabGroup("Reference")]
        private Transform m_positionDestroyFX;
        [SerializeField, TabGroup("FX ni ha bilatibay")]
        private ParticleFX m_impactFX;
        [SerializeField, TabGroup("FX ni ha bilatibay")]
        private GameObject m_impactFXGO;

        [SerializeField, SpineEvent, TabGroup("Animation Events ni ha bilatibay")]
        private string m_AboAngBataan;
        [SerializeField, SpineEvent, TabGroup("Animation Events ni ha bilatibay")]
        private string m_WasakAngBataan;
        [SerializeField]
        private SkeletonAnimation m_skeletonAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_anticipationLoopAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_attackDestroyAftermathAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_attackPlatformAftermathAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_emergeAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_platformDestroyAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_platformPersistAnimation;

        [SerializeField]
        private BoxCollider2D m_impactCollider;
        [SerializeField]
        private BoxCollider2D m_obstacleCollider;
        [SerializeField]
        private RaySensor m_playerSensor;
        [SerializeField]
        private bool m_playerHit;
        private Coroutine myCoroutine;
        private bool m_smashMonolith;
        public bool removeMonolithOnGround;
        public bool keepMonolith;
        public bool monolithGrounded;

        // Start is called before the first frame update
        void Start()
        {
            enabled = true; 
            m_obstacleCollider.enabled = false;
            m_smashMonolith = false;
            keepMonolith = false;
            m_playerHit = false;
            m_playerSensor.enabled = false;
           // StartCoroutine(EmergeTentacle());
            m_spineLister.Subscribe(m_WasakAngBataan, DestroyFX);
            m_spineLister.Subscribe(m_AboAngBataan, OffImpactCollider);
            gameObject.SetActive(false);

        }
        public void EmergeRoutine()
        {
            StartCoroutine(EmergeTentacle());
        }
        private void OnEnable()
        {
           
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
        // Update is called once per frame
        void Update()
        {
            #region BraindeadCode
            //if (m_smashMonolith)
            //{
            //    StartCoroutine(Smash());
            //    m_smashMonolith = false;
            //}

            //if (!monolithGrounded)
            //{
            //    if (keepMonolith)
            //    {
            //        if (m_playerSensor.isDetecting)
            //        {
            //            m_playerHit = true;
            //        }

            //        if (m_playerHit)
            //        {
            //            StartCoroutine(DestroyMonolith());
            //        }
            //    }
            //}            
            #endregion

        }



        private void DestroyFX()
        {
            m_impactFX.Play();
           
            Debug.Log("WEWEEWEW");
        }
        public void OffImpactCollider()
        {
           // m_impactCollider.enabled = false;
           // m_obstacleCollider.enabled = true;
        }
        public void RemoveTentacle()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
        public void SpawnShatterFX()
        {
            var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_impactFXGO, gameObject.scene);
            instance.SpawnAt(new Vector2(transform.position.x, transform.position.y + 10f), Quaternion.identity);
            instance.GetComponent<ParticleFX>().Play();
        }
        public IEnumerator EmergeTentacle()
        {
            m_impactCollider.enabled = false;
            m_obstacleCollider.enabled = false;
            m_animation.SetAnimation(0, m_emergeAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_emergeAnimation);
            yield return AnticipationLoop();
        }

        private IEnumerator AnticipationLoop()
        {
            m_animation.SetAnimation(0, m_anticipationLoopAnimation, true).TimeScale = 0.5f;
            yield return null;
        }

        private IEnumerator DestroyMonolith()
        {
            m_animation.SetAnimation(0, m_platformDestroyAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_platformDestroyAnimation);
            monolithGrounded = true;
            m_impactCollider.enabled = false;
            m_obstacleCollider.enabled = false;
            DestroyInstance();
        }

        private IEnumerator DoAttackWithMonolithPersist()
        {
            m_impactCollider.enabled = true;
            m_animation.SetAnimation(0, m_attackPlatformAftermathAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_attackPlatformAftermathAnimation);
            //FindObjectOfType<ObstacleChecker>().RemoveMonolithAtIndex(0);       
            m_animation.SetAnimation(0, m_platformPersistAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_platformPersistAnimation);
            m_impactCollider.enabled = false;
            m_obstacleCollider.enabled = true;
            monolithGrounded = true;     
        }

        private IEnumerator DoAttackWithoutMonolithPersist()
        {
            m_impactCollider.enabled = true;
            m_animation.SetAnimation(0, m_attackDestroyAftermathAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_attackDestroyAftermathAnimation);
            m_impactCollider.enabled = false;
            m_obstacleCollider.enabled = false;
        }

        [Button]
        public void AttackKeepMonolith()
        {
            Debug.Log("KEPT MONOLITH");
            StartCoroutine(DoAttackWithMonolithPersist());
        }

        [Button]
        public void AttackDestroyMonolith()
        {
            Debug.Log("Destroy MONOLITH");
            StartCoroutine(DoAttackWithoutMonolithPersist());
        }

        private IEnumerator Smash()
        {
            m_playerSensor.enabled = true;
            m_impactCollider.enabled = true;
            if (keepMonolith)
            {
                AttackKeepMonolith();
            }
            else if(!keepMonolith)
            {
                AttackDestroyMonolith();
            }
            yield return null;
        }

        private void OnDestroy()
        {
            if (FindObjectOfType<ObstacleChecker>().monolithSlamObstacleList != null)
                FindObjectOfType<ObstacleChecker>().monolithSlamObstacleList.Remove(this);
        }

        public void TriggerSmash()
        {
            m_smashMonolith = true;
        }
    }
}

