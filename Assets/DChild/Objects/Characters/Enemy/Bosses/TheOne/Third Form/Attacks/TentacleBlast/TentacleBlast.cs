using Holysoft.Event;
using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Enemies
{
    public class TentacleBlast : MonoBehaviour
    {
        [SerializeField, TabGroup("Reference")]
        protected SpineRootAnimation m_animation;

        [SerializeField, TabGroup("Reference")]
        protected TheOneThirdFormLaserLauncher m_laserLunch;

        [SerializeField]
        private SpineEventListener m_spineListener;
        [Title("Events")]
        [SerializeField, SpineEvent]
        private string m_startCharge;
        public string startCharge => m_startCharge;
        [Title("Events")]
        [SerializeField, SpineEvent]
        private string m_endCharge;
        public string endCharge => m_endCharge;
        [Title("Events")]
        [SerializeField, SpineEvent]
        private string m_beamStart;
        public string beamStart => m_beamStart;
        [SerializeField]
        private SkeletonAnimation m_skeletonAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_initializeAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_despawnAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_mouthBlastAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_spawnAnimation;

        private GameObject m_tentacleBlastLaser;

        [SerializeField, BoxGroup("Laser")]
        private LaserLauncher m_launcher;

        [SerializeField]
        private Animator m_anim;

        public event EventAction<EventActionArgs> AttackStart;
        public event EventAction<EventActionArgs> AttackDone;

        public bool isDoneTentacleAttack = false;

       /* private void BeamStartCollider()
        {
            m_laserLunch.UpdateEdgeCollider();
        }*/
        private void EndChargeFX()
        {
            m_anim.SetTrigger("TentacleBlastDissipation");
        }
        private void ChargeStartFX()
        {
            StartCoroutine(m_laserLunch.LaserLogic());
            m_anim.SetTrigger("TentacleBlastAnticipation");
           // m_anim.SetTrigger("TentacleBlastDissipation");
        }

        private IEnumerator EmergeTentacle()
        {
            isDoneTentacleAttack = false;
            m_animation.SetAnimation(0, m_spawnAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_spawnAnimation);

        }

        private IEnumerator DespawnTentacle()
        {
            m_animation.SetAnimation(0, m_despawnAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_despawnAnimation);
            isDoneTentacleAttack = true;
        }

        private IEnumerator ShootTentacleBeam()
        {
            m_animation.SetAnimation(0, m_mouthBlastAnimation, false);
      
            yield return new WaitForAnimationComplete(m_animation.animationState, m_mouthBlastAnimation);
            //m_launcher.SetBeam(false);
        }

        public IEnumerator TentacleBlastAttack()
        {
            //AttackStart?.Invoke(this, EventActionArgs.Empty);
            yield return EmergeTentacle();
            yield return ShootTentacleBeam();
            yield return DespawnTentacle();
          //  AttackDone?.Invoke(this, EventActionArgs.Empty);
        }

        // Start is called before the first frame update
        void Start()
        {
            //m_tentacleOriginalPosition = m_tentacleEntity.transform.position;
            //m_tentacleBlastLaser.SetActive(false);
            m_spineListener.Subscribe(m_endCharge, EndChargeFX);
            m_spineListener.Subscribe(m_startCharge, ChargeStartFX);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        [Button]
        private void ShootBlast()
        {
            StartCoroutine(TentacleBlastAttack());
        }
    }
}

