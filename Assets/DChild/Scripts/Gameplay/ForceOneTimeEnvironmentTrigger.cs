using DChild.Gameplay.Characters.Players.State;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Environment;
using DChild.Gameplay;
using DChild.Serialization;
using DChild;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PixelCrushers.DialogueSystem;
public class ForceOneTimeEnvironmentTrigger : MonoBehaviour
{
    [System.Serializable]
    public struct SaveData : ISaveData
    {
        public SaveData(bool wasTriggered)
        {
            this.m_isTriggered = wasTriggered;
        }

        [ShowInInspector]
        public bool m_isTriggered;
        public bool isTriggered => m_isTriggered;                                                                                                                                                                                                                                                                                                                                                                                       ISaveData ISaveData.ProduceCopy() => new SaveData(m_isTriggered);
    }
    public bool m_hasDialogue;
    [ShowIf("m_hasDialogue")]
    public DialogueSystemTrigger m_dialogueToTrigger;
    [SerializeField]
    private Collider2D m_collider;
    private bool m_wasTriggered;
    [SerializeField, TabGroup("Enter")]
    private UnityEvent m_enterEvents;
    private IGroundednessState m_playerGroundedness;
    private Coroutine m_enterEventRoutine;
    private Coroutine m_exitEventRoutine;

    public ISaveData Save()
    {
        return new SaveData(m_wasTriggered);
    }

    public void Load(ISaveData data)
    {
        m_wasTriggered = ((SaveData)data).isTriggered;
    }
    public void Initialize()
    {
        m_wasTriggered = false;      
        m_playerGroundedness = GameplaySystem.playerManager.player.character.GetComponentInChildren<IGroundednessState>();        
        TryGetComponent(out m_collider);
    }

    private IEnumerator ExecuteEnterWhenPlayerIsGrounded()
    {
      while (m_playerGroundedness.isGrounded == false)
      {
            Debug.Log("Highest in the room");
        yield return null;
      }
        TriggerEnterEvent();
        m_enterEventRoutine = null;
    }


    private void TriggerEnterEvent()
    {
        m_enterEvents?.Invoke();
        
        gameObject.GetComponent<Collider2D>().enabled = true;
        m_wasTriggered = true;       
    }


    private void Start()
    {
        Initialize();
    }

    private void OnTriggerEnter2D                                                                                                                                                                                                                                                               (Collider2D collision)
    {
        if (collision.tag != "Hitbox" && m_dialogueToTrigger == null)
        {
            Debug.Log("no dialogue");
            return;
            
        }
          

        var playerObject = collision.gameObject.GetComponentInParent<PlayerControlledObject>();
        if (playerObject != null && collision.tag != "Sensor" && playerObject.owner == (IPlayer)GameplaySystem.playerManager.player)
        {
            if(!m_wasTriggered)
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
                if (m_exitEventRoutine != null)
                {
                                                                                                                                                                                                                                                                                                                                            StopCoroutine(m_exitEventRoutine);
                    m_exitEventRoutine = null;
                }

                if (m_enterEventRoutine == null)
                {
                    m_enterEventRoutine = StartCoroutine(ExecuteEnterWhenPlayerIsGrounded());
                }
            }
            else
            {
               TriggerEnterEvent();
            }
        }
        

    }

    private void OnValidate()
    {
        DChildUtility.ValidateSensor(gameObject);
    }

#if UNITY_EDITOR
    private void OnValueChange() 
    { 
    //m_exitEvents.RemoveAllListeners();

    }

    [Button, HideInEditorMode]
    private void OnEnter()
    {
        m_enterEvents?.Invoke();
    }

    
#endif
}

