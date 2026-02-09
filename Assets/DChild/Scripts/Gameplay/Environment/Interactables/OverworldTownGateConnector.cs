using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace DChild.Gameplay.Environment.Interractables
{
    [RequireComponent(typeof(TownGateHandler))]
    public class OverworldTownGateConnector : MonoBehaviour
    {
        [SerializeField, VariablePopup(true)]
        private string m_serializationReference;

        private TownGateHandler m_townGate;

        private void Start()
        {
            m_townGate = GetComponent<TownGateHandler>();

            var isActivated = DialogueLua.GetVariable(m_serializationReference).asBool;
            if (isActivated == false)
            {
                m_townGate.InteractionOptionChange += OnFirstInteraction;
            }
        }

        private void OnFirstInteraction(object sender, EventActionArgs eventArgs)
        {
            DialogueLua.SetVariable(m_serializationReference, true);
            m_townGate.InteractionOptionChange -= OnFirstInteraction;
        }

        private void OnDisable()
        {
            m_townGate.InteractionOptionChange -= OnFirstInteraction;
        }
    }

}
