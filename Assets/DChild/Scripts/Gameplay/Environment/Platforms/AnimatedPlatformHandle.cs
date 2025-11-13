using DChild.Gameplay.Environment;
using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPlatformHandle : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_skeletonList = new List<GameObject>();
    [SerializeField]
    private List<SkeletonAnimation> m_skeletonAnimationList = new List<SkeletonAnimation>();
    [SerializeField, Spine.Unity.SpineAnimation]
    private string m_reappearAnimation;
    [SerializeField, Spine.Unity.SpineAnimation]
    private string m_disappearAnimation;

#if UNITY_EDITOR
    [Button]
    public void GetAllPlatforms()
    {
        var platforms = GetComponentsInChildren<DisappearingPlatform>();

        for(int x = 0; x < platforms.Length; x++)
        {
            m_skeletonList.Add(platforms[x].gameObject);
            m_skeletonAnimationList.Add(platforms[x].gameObject.GetComponentInChildren<SkeletonAnimation>());
        }
    }
#endif

    public void PlayReappearAnimation()
    {
        if(m_skeletonAnimationList == null)
        {
            return;
        }
        for(int x = 0;x < m_skeletonAnimationList.Count; x++)
        {
            m_skeletonAnimationList[x].state.SetAnimation(0, m_reappearAnimation, false);
        }
    }

    public void PlayDiappearAnimation()
    {
        if(m_skeletonAnimationList == null)
        {
            return;
        }
        for (int x = 0; x < m_skeletonList.Count; x++)
        {
            m_skeletonAnimationList[x].state.SetAnimation(0, m_disappearAnimation, false);
        }
    }

    void Start()
    {
    }

    void Update()
    {
        
    }
}
