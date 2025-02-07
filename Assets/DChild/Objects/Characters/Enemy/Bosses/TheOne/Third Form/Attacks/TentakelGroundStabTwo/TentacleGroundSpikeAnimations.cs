using DChild;
using DChild.Gameplay.Characters;
using DChild.Gameplay.Characters.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGroundSpikeAnimations : MonoBehaviour
{
    [SerializeField]
    private SpineRootAnimation m_spine;
    [SerializeField]
    private Collider2D m_selfCollider;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_initializeAnim;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private List<string> m_variationsAnim;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_retractAnim;
    // Update is called once per frame


    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(RandomAmimation());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void StartRetractAnimation()
    {
        StartCoroutine(RetractAnim());
    }
    public IEnumerator RandomAmimation()
    {
        var randomFucker = Random.Range(0, 3);
        m_spine.SetAnimation(0, m_variationsAnim[randomFucker], false);
        yield return new WaitForAnimationComplete(m_spine.animationState, m_variationsAnim[randomFucker]);
        m_selfCollider.enabled = true;
    }
    public IEnumerator RetractAnim()
    {
        m_spine.SetAnimation(0, m_retractAnim, false);
        yield return new WaitForAnimationComplete(m_spine.animationState, m_retractAnim);
        m_selfCollider.enabled = false;
    }
    
}
