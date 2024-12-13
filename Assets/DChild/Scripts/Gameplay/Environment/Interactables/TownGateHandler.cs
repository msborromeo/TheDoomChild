using DChild.Gameplay;
using DChild.Gameplay.Environment.Interractables;
using DChild.Gameplay.FastTravel;
using DChild.Gameplay.Systems;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TownGateHandler : MonoBehaviour, IButtonToInteract
{
    [SerializeField, VariablePopup(true)]
    private string m_serializationReference;
    [SerializeField, TabGroup("Reference")]
    private SkeletonAnimation m_SkeletonAnimation;
    [SerializeField, TabGroup("Reference")]
    private LocationPoster m_poster;
    [SerializeField, TabGroup("Actions")]
    private UnityEvent Default, InteractAction;
    [SerializeField, Spine.Unity.SpineAnimation, TabGroup("Animation")]
    private List<string> m_Interact;
    [SerializeField, Spine.Unity.SpineAnimation, TabGroup("Animation")]
    private List<string> m_Idle;
    [SerializeField, TabGroup("Appearance"), OnValueChanged("GateValueChanged")]
    private SkeletonDataAsset m_GateAnimation;
    [SerializeField]
    public Vector3 m_Offset;

    public event EventAction<EventActionArgs> InteractionOptionChange;

    public bool showPrompt => true;

    public string promptMessage => "Town Portal";

    public Vector3 promptPosition => transform.position + m_Offset;

    private void Start()
    {
        IdlePortal();
    }

    private string ChooseIdleAnim()
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

    private string ChooseInteractAnim()
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

    [Button, HideInEditorMode]
    public void NearPortal()
    {
        InteractAction?.Invoke();
        m_SkeletonAnimation.AnimationName = ChooseInteractAnim();
        Debug.Log("Test, On a Portal");
    }
    [Button, HideInEditorMode]
    public void IdlePortal()
    {
        Default?.Invoke();
        m_SkeletonAnimation.AnimationName = ChooseIdleAnim();
        Debug.Log("Test, leaving a portal");
    }

    private void GateValueChanged()
    {
        m_SkeletonAnimation.skeletonDataAsset = m_GateAnimation;
        m_SkeletonAnimation.Initialize(true);
        m_SkeletonAnimation.loop = true;
#if UNITY_EDITOR
        EditorUtility.SetDirty(m_SkeletonAnimation);
        EditorUtility.SetDirty(m_SkeletonAnimation.transform);
#endif
    }
    [Button, HideInEditorMode]
    public void Interact(Character character)
    {
        DialogueLua.SetVariable(m_serializationReference, true);
        GameplaySystem.gamplayUIHandle.OpenFastTravel(m_poster.data.location);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(promptPosition, 1f);
    }
}
