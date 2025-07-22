using DChild;
using DChild.Gameplay.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobwebNiOtot : MonoBehaviour
{
    [SerializeField]
    private SpineRootAnimation m_spine;
    [SerializeField]
    private GameObject m_lean;
    public bool m_okiii;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_tommi;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_jan;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_keith;

    public void InitializeField(SpineRootAnimation spineRoot)
    {
        m_spine = spineRoot;
    }

    public IEnumerator AppearRoutine()
    {
        m_spine.SetAnimation(0, m_tommi, false);
        yield return new WaitForAnimationComplete(m_spine.animationState, m_tommi);
        yield return IdleRoutine();
    }
    public IEnumerator DisappearRoutine()
    {
        m_spine.SetAnimation(0, m_jan, false);
        Instantiate(m_lean, new Vector3(transform.position.x, transform.position.y + 5f, 0f), Quaternion.identity);
        yield return new WaitForAnimationComplete(m_spine.animationState, m_jan);
        Destroy(transform.parent.gameObject);
    }
    public IEnumerator IdleRoutine()
    {
        m_spine.SetAnimation(0, m_keith, true);
        yield return null;
    }

    public void Appear()
    {
        StartCoroutine(AppearRoutine());
    }
    public void Disappear()
    {

        StartCoroutine(DisappearRoutine());
    }
}
