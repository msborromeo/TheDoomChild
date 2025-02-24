using DChild;
using DChild.Gameplay.Characters;
using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMouth : MonoBehaviour
{
    [SerializeField]
    private SpineRootAnimation m_spine;

    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_afterAttack;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_afterAttackChargeLoop;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_afterAttackChargeLoop2;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_attackLoop;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_idle;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_spawnStart;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_waitForInitialize;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_toGrow;

    [SerializeField]
    private GameObject m_laser;
    public EventAction<EventActionArgs> OnActivate;
    [SerializeField]
    private bool m_noAnticipation;
    [SerializeField]
    private bool m_isLeanDroSkytown;
    public bool stop;
    [SerializeField]
    private float m_idleLoopValue;
    [SerializeField]
    private float m_attackLoopValue;
    [SerializeField]
    private bool m_isCeilingLoopValueStephenIndiPagTanduga;
    public void InitializeField(SpineRootAnimation spineRoot)
    {
        m_spine = spineRoot;
    }
    private IEnumerator Attack()
    {
        while (!stop)
        {
            if (m_noAnticipation)
            {
                m_spine.SetAnimation(0, m_attackLoop, true);
                yield return null;
                //yield return new WaitForSeconds(5f);
            }
            else
            {
                m_spine.SetAnimation(0, m_idle, true);
                yield return new WaitForSeconds(m_idleLoopValue);
                m_spine.SetAnimation(0, m_afterAttackChargeLoop, false);
                yield return new WaitForAnimationComplete(m_spine.animationState, m_afterAttackChargeLoop);
                m_spine.SetAnimation(0, m_attackLoop, true);
                yield return new WaitForSeconds(m_attackLoopValue);
            }
        }
        m_spine.SetAnimation(0, m_idle, true);
    }
    public IEnumerator AttackSkyTown()
    {
        OnActivate?.Invoke(this, EventActionArgs.Empty);
        if (m_noAnticipation)
            {
                m_spine.SetAnimation(0, m_attackLoop, true);
                yield return null;
                //yield return new WaitForSeconds(5f);
            }
            else
            {
                
                m_spine.SetAnimation(0, m_toGrow, false);
                yield return new WaitForAnimationComplete(m_spine.animationState, m_toGrow);
                m_spine.SetAnimation(0, m_afterAttackChargeLoop, true);
                yield return new WaitForSeconds(m_idleLoopValue);
            m_laser.GetComponent<TheOneMiniLevelLaser>().stop = true;
            m_spine.SetAnimation(0, m_attackLoop, true);
            if (m_isCeilingLoopValueStephenIndiPagTanduga)
            {
                yield return new WaitForSeconds(6f);
            }
            else
            {
                yield return new WaitForSeconds(m_attackLoopValue);
            }
               
            }
        
        m_spine.SetAnimation(0, m_afterAttack, false);
        yield return new WaitForAnimationComplete(m_spine.animationState, m_afterAttack);
    }

    public void StartSkyTown()
    {
        StartCoroutine(AttackSkyTown());
        OnActivate?.Invoke(this, EventActionArgs.Empty);
    }
    public void StartMovement()
    {
        StartCoroutine(Attack());
        OnActivate?.Invoke(this, EventActionArgs.Empty);
    }
    private void Awake()
    {
        m_laser.GetComponent<TheOneMiniLevelLaser>().m_wallMouth = this.gameObject;
    }
    void Start()
    {
        if (m_isLeanDroSkytown)
        {
            m_spine.SetAnimation(0, m_waitForInitialize, true);
        }
        else
        {
            m_spine.SetAnimation(0, m_idle, true);
        }
        
    }

    void Update()
    {
        
    }
}
