using DChild.Gameplay.Characters.Players;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DChild.Gameplay.Characters.Players.Modules;

namespace DChild.Gameplay.Environment
{
    public class SequenceIllusionPlatformHandle : MonoBehaviour
    {

        [SerializeField]
        private IllusionPlatform[] m_sequence;

        private bool m_GauntletInProcess;
        /*
        private PlayerCollisionSensor[] m_collisionSensorList;
        private PlayerCollisionSensor m_subscribeCollisionSensor;
        [SerializeField]
        private int m_currentAppearingPlatformIndex;

        public void Reset()
        {
            m_currentAppearingPlatformIndex = 0;
            UseIndex(m_currentAppearingPlatformIndex);
        }

        public void RevealNext()
        {
            m_currentAppearingPlatformIndex++;
            UseIndex(m_currentAppearingPlatformIndex);
        }

        public void UseIndex(int index)
        {
            if (index == 0)
            {
                m_sequence[index].Appear(false);
                for (int i = 1; i < m_sequence.Length; i++)
                {
                    m_sequence[i]?.Disappear(false);
                }
            }
            else if (index < m_sequence.Length)
            {
                var indexToDisapear = index - 2;
                if (indexToDisapear >= 0)
                {
                    m_sequence[indexToDisapear]?.Disappear(false);
                }
                m_sequence[index]?.Appear(false);
            }
            SwitchSubscriptionTo(m_collisionSensorList[index]);
        }

        private void SwitchSubscriptionTo(PlayerCollisionSensor toSubscribeTo)
        {
            if (m_subscribeCollisionSensor)
            {
                m_subscribeCollisionSensor.CollisionDetected -= OnCollisionDetected;
            }

            m_subscribeCollisionSensor = toSubscribeTo;

            m_subscribeCollisionSensor.CollisionDetected += OnCollisionDetected;
        }

        private void OnCollisionDetected(object sender, EventActionArgs eventArgs)
        {
            RevealNext();
        }

        private void Awake()
        {
            m_collisionSensorList = new PlayerCollisionSensor[m_sequence.Length];
            for (int i = 0; i < m_sequence.Length; i++)
            {
                m_collisionSensorList[i] = m_sequence[i]?.GetComponentInChildren<PlayerCollisionSensor>() ?? null;
            }
            Reset();
        }
        */

        private int m_currentSequenceIndex;

        private void Awake()
        {
            m_sequence[0].GetComponentInChildren<PlayerCollisionSensor>().CollisionDetected += BeginGauntlet;
            m_sequence[0].GetComponentInChildren<PlayerTriggerSensor>().CollisionDetected += BeginGauntlet;
            for(int i = 0; i < m_sequence.Length; i++)
            {
                m_sequence[i].GetComponentInChildren<PlayerTriggerSensor>().EnableTriggerSensor();
                m_sequence[i].GetComponentInChildren<PlayerTriggerSensor>().CollisionDetected +=RevealNextPlatform;
            }
            Reset();
        }

        private void OnDisable()
        {
            m_sequence[0].GetComponentInChildren<PlayerCollisionSensor>().CollisionDetected -= BeginGauntlet;
            m_sequence[0].GetComponentInChildren<PlayerTriggerSensor>().CollisionDetected -= BeginGauntlet;
            for (int i = 0; i < m_sequence.Length; i++)
            {
                m_sequence[i].GetComponentInChildren<PlayerTriggerSensor>().CollisionDetected -= RevealNextPlatform;
            }
            var character = GameplaySystem.playerManager.player.character;
            character.GetComponentInChildren<WallJump>().ExecuteModule -= OnPlayerJumpExecution;
            character.GetComponentInChildren<GroundJump>().ExecuteModule -= OnPlayerJumpExecution;
        }

        private void OnPlayerJumpExecution(object sender, EventActionArgs eventArgs)
        {
            var newIndex = (int)Mathf.Repeat(m_currentSequenceIndex + 1, m_sequence.Length);
            RevealPlatformsAtConfiguration(newIndex);
            m_currentSequenceIndex = newIndex;
        }

        private void RevealPlatformsAtConfiguration(int index)
        {
            m_sequence[m_currentSequenceIndex]?.Disappear(false);
            //m_sequence[index]?.Appear(false);
        }

        private void RevealNextPlatform(object sender, EventActionArgs eventArgs)
        {
            if(!m_GauntletInProcess)
            {
                return;
            }
            if (m_sequence.Length > (m_currentSequenceIndex + 1))
            {
                m_sequence[m_currentSequenceIndex + 1]?.Appear(false);
            }
        }

        public void Reset()
        {
            m_currentSequenceIndex = 0;
            m_sequence[0].Appear(true);
            for (int i = 1; i < m_sequence.Length; i++)
            {
                //m_sequence[i]?.Appear(true);
                m_sequence[i]?.Disappear(true);
            }
            EndGauntlet();
        }

        public void BeginGauntlet(object sender, EventActionArgs eventArgs)
        {
            if(m_GauntletInProcess)
            {
                return;
            }
            var character = GameplaySystem.playerManager.player.character;
            character.GetComponentInChildren<WallJump>().ExecuteModule += OnPlayerJumpExecution;
            character.GetComponentInChildren<GroundJump>().ExecuteModule += OnPlayerJumpExecution;
            m_GauntletInProcess = true;
            m_sequence[1]?.Appear(false);
        }

        private void OnDestroy()
        {
            EndGauntlet();
        }

        private void EndGauntlet()
        {
            var character = GameplaySystem.playerManager.player.character;
            character.GetComponentInChildren<WallJump>().ExecuteModule -= OnPlayerJumpExecution;
            character.GetComponentInChildren<GroundJump>().ExecuteModule -= OnPlayerJumpExecution;
            m_GauntletInProcess = false;
        }
    }
}

