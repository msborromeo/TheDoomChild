using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Systems;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace DChild.Gameplay.Cinematics.Cameras
{
    public class ReactivePlayerCamera : SerializedMonoBehaviour, IGameplayInitializable
    {
        [SerializeField]
        private bool m_shakeOnDamage;
        [SerializeField, ShowIf("m_shakeOnDamage")]
        private CameraShakeData m_onDamageShakeData;

        [SerializeField]
        private bool m_shakeOnAttackHit;
        [SerializeField, ShowIf("m_shakeOnAttackHit")]
        private CameraShakeData m_onAttackHitShakeData;
        [SerializeField, ShowIf("m_shakeOnAttackHit")]
        private CameraShakeData m_onAttackKillShakeData;
        [SerializeField, ShowIf("m_shakeOnAttackHit")]
        private CameraShakeData m_onEarthShakerImpactShakeData;
        [SerializeField, ShowIf("m_shakeOnAttackHit")]
        private CameraShakeData m_onSwordThrustShakeData;
        [SerializeField, ShowIf("m_shakeOnAttackHit")]
        private CameraShakeData m_onCriticalHitDealtData;

        private ICinema m_cinema;
        private ISwordThrustState m_swordThrustState;
        private Attacker m_attacker;


        public void Initialize()
        {
            m_cinema = GameplaySystem.cinema;

            var player = GameplaySystem.playerManager.player;
            m_swordThrustState = player.state;
            var playerCharacter = GameplaySystem.playerManager.player.character;
            playerCharacter.GetComponentInChildren<EarthShaker>().OnImpact += OnEarthShakerImpact;
            m_attacker = playerCharacter.GetComponent<Attacker>();
        }

        private void OnEnable()
        {
            m_attacker.CriticalHitInflicted += OnCriticalHitInflicted;
        }

        private void OnDisable()
        {
            m_attacker.CriticalHitInflicted -= OnCriticalHitInflicted;
        }

        private void OnCriticalHitInflicted(Vector3 vector, ParticleFX fX)
        {
            m_cinema.ExecuteCameraShake(m_onCriticalHitDealtData);
        }

        public void HandleOnDamageRecieveShake()
        {
            m_cinema.ExecuteCameraShake(m_onDamageShakeData);
        }

        public void HandleOnAttackHit(CombatConclusionEventArgs eventArgs)
        {
            if (m_shakeOnAttackHit)
            {
                if (m_swordThrustState.isDoingSwordThrust)
                {
                    m_cinema.ExecuteCameraShake(m_onSwordThrustShakeData);
                }
                else
                {
                    m_cinema.ExecuteCameraShake(m_onAttackHitShakeData);
                }
            }
        }

        private void OnEarthShakerImpact(object sender, EventActionArgs eventArgs)
        {
            m_cinema.ExecuteCameraShake(m_onEarthShakerImpactShakeData);
        }
    }
}