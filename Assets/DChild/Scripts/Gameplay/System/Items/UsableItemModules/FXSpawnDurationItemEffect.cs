using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Combat.StatusAilment;
using DChild.Gameplay.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;

namespace DChild.Gameplay.Items
{
    [System.Serializable]
    public class FXSpawnDurationItemEffect : IDurationItemEffect
    {
        [SerializeField]
        private BodyReference.BodyPart m_attachTo;
        [SerializeField]
        private GameObject m_fx;

        private Dictionary<int, GameObject> m_fxTracker;
        private FXSpawnHandle<FX> m_fXSpawnHandle;
        private ParticleFX m_particleFX;

        public IDurationItemEffect GetInstance() => this;

        public void StartEffect(IPlayer player)
        {
            var character = player.character;
            var bodypart = character.GetBodyPart(m_attachTo);

            var instance = m_fXSpawnHandle.InstantiateFX(m_fx, bodypart.position).gameObject;
            m_particleFX = instance.GetComponent<ParticleFX>();
            instance.transform.parent = bodypart;
            if (m_fxTracker == null)
            {
                m_fxTracker = new Dictionary<int, GameObject>();
            }
            character.InstanceDestroyed += OnInstanceDestroyed;
            m_fxTracker.Add(character.GetInstanceID(), instance);
        }

        public void StopEffect(IPlayer player)
        {
            var character = player.character;
            var instanceID = character.GetInstanceID();
            character.InstanceDestroyed -= OnInstanceDestroyed;
            RemoveFXForInstance(instanceID);
        }

        private void RemoveFXForInstance(int instanceID)
        {
            if (m_fxTracker.ContainsKey(instanceID))
            {
                m_particleFX.Stop();
                //UnityEngine.Object.Destroy(m_fxTracker[instanceID]);
                m_fxTracker.Remove(instanceID);
            }
        }

        private void OnInstanceDestroyed(object sender, ObjectIDEventArgs eventArgs)
        {
            RemoveFXForInstance(eventArgs.ID);
        }
    }
}

