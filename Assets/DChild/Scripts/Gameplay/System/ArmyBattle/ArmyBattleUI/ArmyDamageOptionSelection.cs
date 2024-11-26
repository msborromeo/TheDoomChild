using Holysoft.Event;
using System;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class ArmyDamageOptionSelection : MonoBehaviour
    {
        public struct DamageTypeSelectedEventArgs : IEventActionArgs
        {
            private DamageType m_damageType;

            public DamageTypeSelectedEventArgs(DamageType damageType)
            {
                m_damageType = damageType;
            }

            public DamageType damageType => m_damageType;
        }

        [SerializeField]
        private ArmyDamageTypeOptionUI[] m_options;
        private Army m_reference;

        public event EventAction<DamageTypeSelectedEventArgs> OnOptionSelected;

        public void Initialize(Army army)
        {
            m_reference = army;
        }

        public void UpdateSelectionAvailability()
        {
            if(m_reference != null)
            {
                for (int i = 0; i < m_options.Length; i++)
                {
                    var option = m_options[i];
                    option.SetInteractability(m_reference.HasAvailableGroup(option.damageType));
                }
            }
        }

        public void SelectOption(ArmyDamageTypeOptionUI armyDamageTypeOptionUI)
        {
            OnOptionSelected?.Invoke(this, new DamageTypeSelectedEventArgs(armyDamageTypeOptionUI.damageType));
        }
    }
}