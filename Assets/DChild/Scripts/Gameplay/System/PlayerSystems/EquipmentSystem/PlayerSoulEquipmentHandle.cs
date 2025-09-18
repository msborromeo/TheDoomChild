using DChild.Gameplay.Characters.Players;
using Holysoft.Collections;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.SoulEquipment
{
    public class PlayerSoulEquipmentHandle : SerializedMonoBehaviour, ISerializable<PlayerSoulEquipmentData>
    {
        [SerializeField]
        private IPlayer m_player;

        [SerializeField]
        private Dictionary<SoulSlot, SoulEquipment> m_equippedSoulEquipment = new Dictionary<SoulSlot, SoulEquipment>();

        public void LoadData(PlayerSoulEquipmentData data)
        {
            throw new System.NotImplementedException();
        }

        public PlayerSoulEquipmentData SaveData()
        {
            throw new System.NotImplementedException();
        }
    }
}

