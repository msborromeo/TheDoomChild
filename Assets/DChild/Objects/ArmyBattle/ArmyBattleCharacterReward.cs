using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using DChild.Gameplay.Environment.Interractables;
using Holysoft.Event;

namespace DChild.Gameplay.ArmyBattle
{
    public class ArmyBattleCharacterReward : MonoBehaviour ,IButtonToInteract
    {
        [SerializeField, TabGroup("Reference")]
        private SpriteRenderer m_Graphics;
        [SerializeField, TabGroup("Actions")]
        private UnityEvent m_Reward;
        [SerializeField, TabGroup("Reference")]
        private Vector3 m_promptOffset;
        [SerializeField, TabGroup("Character Reward")]
        private List<ArmyCharacterData> m_CharacterReward;
        [SerializeField]
        private CharacterGiver m_CharacterGiver;
        [SerializeField, TabGroup("Requirements")]


        public event EventAction<EventActionArgs> InteractionOptionChange;

        public bool showPrompt => true;

        public string promptMessage => "Army Battle Character Dispenser";

        public Vector3 promptPosition => transform.position + m_promptOffset;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GiveReward()
        {
            
            m_CharacterGiver?.RecruitCharacter(m_CharacterReward);
            Debug.LogError("AHHHHHHHHHHHHHH pain");
        }

        public void Interact(Character character)
        {
            //GiveReward();
            m_Reward?.Invoke();
        }
    }
}

