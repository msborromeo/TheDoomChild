using DChild;
using DChild.Gameplay.Characters;
using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Pooling;
using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundStabTwoTentacle : PoolableObject
{
    public float lifespan;
    public bool isOnPlayableGround = false;

    [SerializeField]
    private GameObject[] m_multipleGroundStabWithPattern;
    [SerializeField]
    private GameObject[] m_multipleGroundStabWithPatternAncticipation;
    [SerializeField]
    private GameObject[] safeZones;
    [SerializeField]
    private Collider2D m_hitbox;
    [SerializeField]
    private float m_tentacleStabAnimationSpeedMultiplier;

    [SerializeField, TabGroup("Reference")]
    protected SpineRootAnimation[] m_animation;
    [SerializeField]
    private SkeletonAnimation m_skeletonAnimation;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_attackAnimation;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_retractAnimation;

    



    public IEnumerator StartAttackRoutine()
    {
        //m_animation.SetAnimation(0, m_anticipationStartAnimation, false).TimeScale = m_tentacleStabAnimationSpeedMultiplier;
        //yield return new WaitForAnimationComplete(m_animation.animationState, m_anticipationStartAnimation);

        //m_animation.SetAnimation(0, m_attackAnimation, false).TimeScale = m_tentacleStabAnimationSpeedMultiplier;
        for (int i = 0; i < m_animation.Length; i++)
        {
            m_animation[i].SetAnimation(0, m_attackAnimation, false);
        }
        yield return new WaitForSeconds(1.25f);
        if (isOnPlayableGround)
            m_hitbox.enabled = true;
        yield return new WaitForAnimationComplete(m_animation[0].animationState, m_attackAnimation);

        
        var randomShit = Random.Range(1, 3);
        if (randomShit == 1)
        {
            m_multipleGroundStabWithPatternAncticipation[0].SetActive(true);
            yield return new WaitForSeconds(1f);
            m_multipleGroundStabWithPatternAncticipation[0].SetActive(false);
            m_multipleGroundStabWithPattern[0].SetActive(true);

        }
        else
        {
            m_multipleGroundStabWithPatternAncticipation[1].SetActive(true);
            yield return new WaitForSeconds(1f);
            m_multipleGroundStabWithPatternAncticipation[1].SetActive(false);
            m_multipleGroundStabWithPattern[1].SetActive(true);
        }
        if (FindObjectOfType<ObstacleChecker>().monolithSlamObstacleList != null)
            FindObjectOfType<ObstacleChecker>().ClearMonoliths();

        yield return TentacleStay();
    }

    public IEnumerator TentacleStay()
    {
        //InitializeSafeZone();
        //m_animation.SetAnimation(0, m_stayAnimation, false);
        yield return new WaitForSeconds(lifespan);
        yield return Retract();
    }

    public IEnumerator Retract()
    {
       // RemoveSafeZones();
        m_hitbox.enabled = false;
        for (int i = 0; i < m_multipleGroundStabWithPattern.Length; i++)
        {
            m_multipleGroundStabWithPattern[i].SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < m_animation.Length; i++)
        {
            m_animation[i].SetAnimation(0, m_retractAnimation, false);
        }
        yield return new WaitForAnimationComplete(m_animation[0].animationState, m_retractAnimation);
        // DestroyInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        // m_hitbox.enabled = false;
        //StartCoroutine(StabRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeSafeZone()
    {
        if (isOnPlayableGround)
        {
            int randomSafeZone = Random.Range(0, safeZones.Length);

            GameObject safezone = safeZones[randomSafeZone];
            safezone.SetActive(true);
        }
    }

    private void RemoveSafeZones()
    {
        foreach (GameObject safeZone in safeZones)
        {
            safeZone.SetActive(false);
        }
    }
}
