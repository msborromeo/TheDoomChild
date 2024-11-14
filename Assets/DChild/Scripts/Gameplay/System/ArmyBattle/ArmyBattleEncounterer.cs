using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Environment.Interractables;
using Holysoft.Event;
using DChild.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DChild.Gameplay.ArmyBattle
{
    public class ArmyBattleEncounterer : MonoBehaviour , IButtonToInteract , ISerializableComponent
    {
        [SerializeField,TabGroup("Initialize")]
        private ArmyBattleScenarioData m_Scenario;
        [SerializeField, TabGroup("Reference")]
        private SpriteRenderer m_SpriteRenderer;
        [SerializeField, TabGroup("Reference")]
        private bool m_Repeatable;
        [SerializeField, TabGroup("Initialize")]
        private Sprite m_Appearance;
        [HideInInspector]
        private bool m_IsDefeated;
        [SerializeField]
        private Vector3 m_promptOffset;

        public event EventAction<EventActionArgs> InteractionOptionChange;

        public bool showPrompt => true;

        public string promptMessage => "Encounter an Army";

        public Vector3 promptPosition => transform.position + m_promptOffset;

        [System.Serializable]
        private struct SaveData : ISaveData
        {
            [SerializeField]
            private bool m_isDefated;

            public SaveData(bool isDefated)
            {
                m_isDefated = isDefated;
            }

            public bool isDefated => m_isDefated;

            public ISaveData ProduceCopy() => new SaveData(m_isDefated);
        }

        private void Awake()
        {
            m_SpriteRenderer.sprite = m_Appearance;
        }
        
        [Button, HideInEditorMode]
        public void InitiateEncounter()
        {
            GameSystem.LoadZone(GameMode.ArmyBattle, null, true);
            ArmyBattleSystem.BattleScenario = m_Scenario;
            Debug.Log("ARMY BATTLE SCENARIO INITIATED :" + ArmyBattleSystem.BattleScenario.name);
        }
        /*
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var playerObject = collision.gameObject.GetComponentInParent<PlayerControlledObject>();
            Debug.Log(playerObject);
            if (collision.tag != "Sensor")
            {
                InitiateEncounter();
                if(!Repeatable)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        */
        [Button, HideInEditorMode]
        public void DefeatArmy()
        {
            Debug.Log(name + " Is Defeated");
            Destroy(this.gameObject);
        }
        public void Interact(Character character)
        {
            InitiateEncounter();
        }

        public ISaveData Save()
        {
            return new SaveData(m_IsDefeated);
        }

        public void Load(ISaveData data)
        {
            m_IsDefeated = ((SaveData)data).isDefated;
            if(m_IsDefeated)
            {
                this.gameObject.SetActive(false);
            }else
            {

            }
        }

        public void Initialize()
        {
            m_IsDefeated = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(promptPosition, .1f);
        }
    }
}
