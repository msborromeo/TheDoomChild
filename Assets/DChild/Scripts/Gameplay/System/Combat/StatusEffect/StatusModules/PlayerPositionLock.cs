using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Characters.Players.State;
using DChild.Inputs;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace DChild.Gameplay.Combat.StatusAilment
{
    [System.Serializable]
    public class PlayerPositionLock : IStatusEffectUpdatableModule
    {
        [SerializeField,MinValue(0)]
        private float m_maxXDistanceChange;
        private Vector2 m_lockIntoPosition;

        private Character m_character;
        private InputReader m_playerInput; //This is very dangerous need to find a way to check if player is moving without checking for input
        private bool m_playerIsAttemptingToMove;

        public PlayerPositionLock(float maxXDistanceChange)
        {
            m_maxXDistanceChange = maxXDistanceChange;
        }

        public void CalculateWithDuration(float duration)
        {

        }

        public IStatusEffectUpdatableModule CreateCopy()
        {
            return new PlayerPositionLock(m_maxXDistanceChange);
        }

        public void Initialize(Character character)
        {
            m_character = character;
            m_lockIntoPosition = character.transform.localPosition;
            var player = GameplaySystem.playerManager.player;
            if (character = player.character)
            {

                m_playerInput = player.GetComponentInChildren<UnderworldPlayerController>().inputReader;
                m_playerInput.Vector2InputPerformedEvent += OnMovementPerformed;
                m_playerInput.Vector2CancelledInputEvent += OnMovementPerformed;
                m_playerIsAttemptingToMove = true; //Since There is no way to find out if player is holding input when this is initialized, forcing this as true will make sure that player is locked in.
            }
        }

        public void Deinitialize()
        {
            if (m_playerInput == null)
                return;

            m_playerInput.Vector2InputPerformedEvent -= OnMovementPerformed;
            m_playerInput.Vector2CancelledInputEvent -= OnMovementPerformed;
        }

        private void OnMovementPerformed(Vector2 vector)
        {
            m_playerIsAttemptingToMove = vector.x != 0;
        }

        public void Update(float delta)
        {
            if (m_playerIsAttemptingToMove)
            {
                var currentlocalPosition = m_character.transform.localPosition;
                if(Mathf.Abs(m_lockIntoPosition.x - currentlocalPosition.x) > m_maxXDistanceChange)
                {
                    var toPlayerPosition = (Vector2)m_character.transform.localPosition - m_lockIntoPosition;
                    var signedMovement = Mathf.Sign(toPlayerPosition.x);

                    var proposedClampPosition = m_lockIntoPosition;
                    proposedClampPosition.x += signedMovement * m_maxXDistanceChange;
                    Debug.Log(proposedClampPosition);
                    m_character.transform.localPosition = new Vector3(proposedClampPosition.x, m_character.transform.localPosition.y, m_character.transform.localPosition.z);
                }
            }
            else
            {
                m_character.transform.localPosition = new Vector3(m_lockIntoPosition.x, m_character.transform.localPosition.y, m_character.transform.localPosition.z);
            }
        }


    }
}