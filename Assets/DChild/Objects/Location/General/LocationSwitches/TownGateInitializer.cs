using DChild.Gameplay.FastTravel;
using DChild.Gameplay.Systems;
using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TownGateInitializer : MonoBehaviour
{
    [SerializeField, TabGroup("Reference")]
    private SkeletonAnimation m_SkeletonAnimation;
    [SerializeField, TabGroup("Reference")]
    private LocationSwitcher m_switcher;
    [SerializeField, TabGroup("Reference")]
    private LocationPoster m_Poster;
    [SerializeField, TabGroup("Actions")]
    private UnityEvent Default, Interact;
    [SerializeField, Spine.Unity.SpineAnimation, TabGroup("Animation")]
    private List<string> m_Interact;
    [SerializeField, Spine.Unity.SpineAnimation, TabGroup("Animation")]
    private List<string> m_Idle;
    [SerializeField, TabGroup("Appearance"), OnValueChanged("GateValueChanged")]
    private SkeletonDataAsset m_GateAnimation;
    public FastTravelHandle fastTravel;

    // Start is called before the first frame update
    void Start()
    {
        IdlePortal();
    }

    string ChooseIdleAnim()
    {
        if (m_Idle.Count > 1)
        {
            int x = UnityEngine.Random.Range(0, m_Idle.Count);
            return m_Idle[x];
        }
        else
        {
            return m_Idle[0];
        }
    }

    string ChooseInteractAnim()
    {
        if (m_Interact.Count > 1)
        {
            int x = UnityEngine.Random.Range(0, m_Interact.Count);
            return m_Interact[x];
        }
        else
        {
            return m_Interact[0];
        }
    }

    [Button]
    public void InteractPortal()
    {
        Interact?.Invoke();
        m_SkeletonAnimation.AnimationName = ChooseInteractAnim();
        Debug.Log("Test, On a Portal");
    }
    [Button]
    public void IdlePortal()
    {
        Default?.Invoke();
        m_SkeletonAnimation.AnimationName = ChooseIdleAnim();
        Debug.Log("Test, leaving a portal");
    }

    void GateValueChanged()
    {
        m_SkeletonAnimation.skeletonDataAsset = m_GateAnimation;
        m_SkeletonAnimation.Initialize(true);
        m_SkeletonAnimation.loop = true;
#if UNITY_EDITOR
        EditorUtility.SetDirty(m_SkeletonAnimation);
        EditorUtility.SetDirty(m_SkeletonAnimation.transform);
#endif
    }

}
