using DChild.Gameplay.Characters.Players;
using Holysoft.Event;
using UnityEngine;

namespace DChild.Gameplay.Environment
{
    public class PlayerTriggerSensor : MonoBehaviour, IPlayerWallStickPlatformReaction
    {
        public event EventAction<EventActionArgs> CollisionDetected;

        private bool hasAnything;
        public void EnableTriggerSensor()
        {
            Debug.LogError("ENABLE");
            hasAnything = true;
        }

        public void ReactToPlayerWallStick(Character player)
        {
            CollisionDetected?.Invoke(this, EventActionArgs.Empty);
        }

        public void ReactToPlayerWallUnstick(Character player)
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!hasAnything)
            {
                gameObject.SetActive(false);
                return;
            }
            if(!collision.gameObject.CompareTag("Hitbox"))
            {
                return;
            }
            Debug.LogError("HIT SOMETHING");
            if (GameplaySystem.playerManager.IsPartOfPlayer(collision.gameObject))
            {
                Debug.LogError("IS A PLAYER");
                CollisionDetected?.Invoke(this, EventActionArgs.Empty);
            }
        }
    }
}
