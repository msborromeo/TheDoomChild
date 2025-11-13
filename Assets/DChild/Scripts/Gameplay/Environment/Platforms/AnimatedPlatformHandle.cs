using DChild.Gameplay.Environment;
using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPlatformHandle : MonoBehaviour
{
    [SerializeField]
    private SkeletonDataAsset skeletonAnimation;
    [SerializeField]
    private List<GameObject> m_skeletonList = new List<GameObject>();
    [SerializeField, SpineAnimation]
    private string m_reappearAnimation;
    [SerializeField, SpineAnimation]
    private string m_disappearAnimation;

#if UNITY_EDITOR
    [Button]
    public void GetAllPlatforms()
    {
        var platforms = GetComponentsInChildren<DisappearingPlatform>();

        for(int x = 0; x < platforms.Length; x++)
        {
            m_skeletonList.Add(platforms[x].gameObject);
        }
    }
#endif

    public void PlayReappearAnimation()
    {
        for(int x = 0;x < m_skeletonList.Count; x++)
        {
            var platform = m_skeletonList[x].GetComponentInChildren<SkeletonAnimation>();
            platform.state.SetAnimation(0, m_reappearAnimation, false);
        }
    }

    public void PlayDiappearAnimation()
    {
        for (int x = 0; x < m_skeletonList.Count; x++)
        {
            var platform = m_skeletonList[x].GetComponentInChildren<SkeletonAnimation>();
            platform.state.SetAnimation(0, m_disappearAnimation, false);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
