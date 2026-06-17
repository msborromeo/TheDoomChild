using DChild.Gameplay.Environment.Interractables;
using DChild.Serialization;
using Holysoft.Event;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.Gameplay.Systems.Lore
{
    public class LoreNote : MonoBehaviour, IButtonToInteract
    {
        [SerializeField]
        private Vector3 m_promptOffset;
        [SerializeField]
        private LoreData m_data;
        [SerializeField]
        private bool m_isPickedUp;

        public event EventAction<EventActionArgs> InteractionOptionChange;
        public UnityEvent onInteract;

        public bool showPrompt => true;

        public string promptMessage => "Pick Up";

        public Vector3 promptPosition => transform.position + m_promptOffset;


        private void Start()
        {

        }

        public void Interact(Character character)
        {
            GameplaySystem.gamplayUIHandle.notificationManager.QueueNotification(UI.StoreNotificationType.Lore,m_data.codexData.GetInstanceID());
            gameObject.SetActive(false);
            onInteract?.Invoke();

        }

        public void SetAsPickedUp()
        {
            gameObject.SetActive(false);
        }

        public void SetAsNotPickedUp()
        {
            gameObject.SetActive(true);
        }


        [Button]
        private void Pickup()
        {
            Interact(null);
        }

        private void OnDrawGizmosSelected()
        {
            var position = promptPosition;
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(position, 1f);
        }
    }
}