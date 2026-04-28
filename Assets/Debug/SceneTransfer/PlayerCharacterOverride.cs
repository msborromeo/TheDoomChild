using DChild.Gameplay.Characters.Players.Behaviour;
using DChild.Gameplay.Characters.Players.Modules;
using DChild.Inputs;
using PlayerNew;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players
{
    [AddComponentMenu("DChild/Gameplay/Player/Controller/Player Character Override")]
    public class PlayerCharacterOverride : MonoBehaviour
    {
        [SerializeField, Range(-1f, 1f), OnValueChanged("OnHorizontalInputChanged")]
        private float m_moveDirectionInput;
        [SerializeField]
        private UnderworldPlayerController m_playerController;

        [Title("Modules")]
        [SerializeField]
        private InputReader m_input;

        public float moveDirectionInput {
            set
            {
                m_moveDirectionInput = Mathf.Clamp(value, -1f, 1f);
                if (value == 0)
                {
                    m_playerController.ControlMovementOverride(0);
                }
                else
                {
                    m_playerController.ControlMovementOverride(m_moveDirectionInput);
                }
            }
        }

        private void OnHorizontalInputChanged()
        {
            moveDirectionInput = m_moveDirectionInput; // Potential redundancy, see explanation below
        }

        private void Awake()
        {
            enabled = false;
        }

        private void OnDisable()
        {
            moveDirectionInput = 0;
        }

        private void OnEnable()
        {
            moveDirectionInput = 0;
        }
    } 
}
