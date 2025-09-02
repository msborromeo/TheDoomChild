using DChild.Gameplay.Characters.Players;
using Holysoft.Collections;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.SoulSkills
{
    public class PlayerSoulItemHandle : SerializedMonoBehaviour, ISerializable<PlayerSoulSkillData>
    {
        [SerializeField]
        private IPlayer m_player;

        public void LoadData(PlayerSoulSkillData data)
        {
            throw new System.NotImplementedException();
        }

        public PlayerSoulSkillData SaveData()
        {
            throw new System.NotImplementedException();
        }
    }
}

