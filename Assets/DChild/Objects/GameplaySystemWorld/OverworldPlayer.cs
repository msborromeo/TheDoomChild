using DChild.Gameplay.Characters.Players.Behaviour;
using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Combat.StatusAilment;
using DChild.Gameplay.Inventories;
using DChild.Gameplay.Items;
using DChild.Gameplay.SoulSkills;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players
{
    public class OverworldPlayer : MonoBehaviour, IPlayer
    {
        [Title("Model")]
        [SerializeField]
        private Character m_controlledCharacter;

        public CharacterState state => throw new System.NotImplementedException();

        public IPlayerStats stats => throw new System.NotImplementedException();

        public Health health => throw new System.NotImplementedException();

        public Magic magic => throw new System.NotImplementedException();

        public Health armor => throw new System.NotImplementedException();

        public IHealable healableModule => throw new System.NotImplementedException();

        public IDamageable damageableModule => throw new System.NotImplementedException();

        public IAttacker attackModule => throw new System.NotImplementedException();

        public PlayerModuleActivator behaviourModule => throw new System.NotImplementedException();

        public PlayerSkills skills => throw new System.NotImplementedException();

        public CombatArts combatArts => throw new System.NotImplementedException();

        public PlayerSoulSkillHandle soulSkills => throw new System.NotImplementedException();

        public PlayerModifierHandle modifiers => throw new System.NotImplementedException();

        public PlayerWeapon weapon => throw new System.NotImplementedException();

        public ExtendedAttackResistance attackResistance => throw new System.NotImplementedException();

        public StatusEffectResistance statusResistance => throw new System.NotImplementedException();

        public IMainController controller => throw new System.NotImplementedException();

        public PlayerInventory inventory => throw new System.NotImplementedException();

        public ItemEffectHandle itemEffect => throw new System.NotImplementedException();

        public LootPicker lootPicker => throw new System.NotImplementedException();

        public StatusEffectReciever statusEffectReciever => throw new System.NotImplementedException();

        public Character character => m_controlledCharacter;

        public ICriticalHitHandle criticalHitHandle => throw new System.NotImplementedException();

        public event EventAction<EventActionArgs> OnDeath;

        public void SetPosition(Vector2 position)
        {
            m_controlledCharacter.transform.position = position;
        }
    }
}

