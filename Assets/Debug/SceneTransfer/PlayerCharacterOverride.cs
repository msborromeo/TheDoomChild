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

        [Title("Modules")]
        [SerializeField]
        private InputReader m_input;

        public float moveDirectionInput {
            set
            {
                m_moveDirectionInput = Mathf.Clamp(value, -1f, 1f);
                if (value == 0)
                {
                    m_input.OnVector2(UnityEngine.InputSystem.InputActionPhase.Canceled, Vector2.zero);
                }
                else
                {
                    m_input.OnVector2(UnityEngine.InputSystem.InputActionPhase.Performed, new Vector2(m_moveDirectionInput, 0)); // Changed 6 to 0 based on context
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
            m_input.Disable(); // Disable input when the object is disabled
        }

        private void OnEnable()
        {
            moveDirectionInput = 0;
            m_input.Enable(); // Enable input when the object is enabled
        }
    } 
}
