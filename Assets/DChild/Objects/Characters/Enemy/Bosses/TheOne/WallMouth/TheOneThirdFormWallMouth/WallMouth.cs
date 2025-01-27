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
    public bool stop;

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
                yield return new WaitForSeconds(5f);
                m_spine.SetAnimation(0, m_afterAttackChargeLoop, false);
                yield return new WaitForAnimationComplete(m_spine.animationState, m_afterAttackChargeLoop);
                m_spine.SetAnimation(0, m_attackLoop, true);
                yield return new WaitForSeconds(10f);
            }
        }
        m_spine.SetAnimation(0, m_idle, true);
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
        m_spine.SetAnimation(0, m_idle, true);
    }

    void Update()
    {
        
    }
}
