using DChild.Gameplay.Characters;
using DChild.Gameplay.FastTravel;
using DChild.Gameplay.Systems;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Environment.Interractables
{
    public class OverworldTownGateHandler : MonoBehaviour, IButtonToInteract
    {
        [SerializeField, VariablePopup(true)]
        private string m_serializationReference;
        [SerializeField, TabGroup("Reference")]
        private LocationPoster m_poster;
        [SerializeField]
        private GameObject m_onEffect;
        [SerializeField]
        public FastTravelData m_UIData;

        [SerializeField]
        public Vector3 m_Offset;

        public event EventAction<EventActionArgs> InteractionOptionChange;

        public bool showPrompt => IsCurrentActiveState();

        public string promptMessage => "Town Portal";

        public Vector3 promptPosition => transform.position + m_Offset;

        private void Start()
        {
            m_onEffect.SetActive(IsCurrentActiveState());
        }

        private bool IsCurrentActiveState() => DialogueLua.GetVariable(m_serializationReference).asBool;

        [Button, HideInEditorMode]
        public void Interact(Character character)
        {
            //DialogueLua.SetVariable(m_serializationReference, true);
            GameplaySystem.gamplayUIHandle.OpenFastTravel(m_poster.data.location, m_UIData);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(promptPosition, 1f);
        }
    }

}
