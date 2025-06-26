using DChild.Gameplay.Characters;
using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeNiJan : MonoBehaviour
{

    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_rockSpikeAnimation;
    [SerializeField]
    private SpineEventListener m_spineListener;
    [SerializeField, TabGroup("GetEvents"), SpineEvent(dataField = "m_skeletonAnimation")]
    private string m_startFX;
    [SerializeField, TabGroup("GetEvents"), SpineEvent(dataField = "m_skeletonAnimation")]
    private string m_endFX;
    [SerializeField, TabGroup("GetEvents"), SpineEvent(dataField = "m_skeletonAnimation")]
    private string m_colliderOn;
    [SerializeField, TabGroup("GetEvents"), SpineEvent(dataField = "m_skeletonAnimation")]
    private string m_colliderOff;
    [SerializeField]
    private ParticleSystem m_startFXXX;
    [SerializeField]
    private ParticleSystem m_endFXXX;
    [SerializeField]
    private SpineRootAnimation m_animation;
    [SerializeField]
    private Collider2D m_collider;
    // Start is called before the first frame update
    void Start()
    {
        m_animation.SetAnimation(0, m_rockSpikeAnimation, false);
        m_spineListener.Subscribe(m_startFX, StartFX);
        m_spineListener.Subscribe(m_endFX, EndFX);
        m_spineListener.Subscribe(m_colliderOn, ColliderOn);
        m_spineListener.Subscribe(m_colliderOff, ColliderOff);
        
    }
    private void EndFX()
    {
        m_endFXXX.Play();
    }
    private void StartFX()
    {
        m_startFXXX.Play();
    }
    private void ColliderOn()
    {
        m_collider.enabled = true;
    }
    private void ColliderOff()
    {
        m_collider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
