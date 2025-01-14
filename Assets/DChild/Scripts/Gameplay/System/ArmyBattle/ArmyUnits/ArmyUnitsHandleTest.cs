using DChild.Gameplay.ArmyBattle.Battalion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DChild.Gameplay.ArmyBattle.Units
{
    [System.Serializable]
    public class ArmyUnitsHandleTest : ArmyUnitsHandle
    {
        [SerializeField]
        private List<ArmyUnit> m_liveUnits;
        [SerializeField]
        private List<ArmyUnit> m_deadUnits;

        private ArmyUnit[] m_allUnitList;
        private Vector3[] m_unitStartingPositions;


        public override void Attack(IArmyBattalion target)
        {
            m_visualizer.Attack(m_liveUnits, target);
        }

        public override List<ArmyUnit> GetUnits()
        {
            return m_liveUnits;
        }

        public override void Idle()
        {
            for (int i = 0; i < m_liveUnits.Count; i++)
            {
                m_liveUnits[i].Idle();
            }
        }

        public override void KillOffUnits(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var index = Random.Range(0, m_liveUnits.Count);
                var unit = m_liveUnits[index];
                unit.Die();
                m_liveUnits.RemoveAt(index);
                m_deadUnits.Add(unit);
            }
        }

        public override void RessurectUnits(int count)
        {
            RepositionUnitsToStartingPosition();
            for (int i = 0; i < count; i++)
            {
                var index = Random.Range(0, m_deadUnits.Count);
                var unit = m_deadUnits[index];
                unit.Ressurect();
                m_deadUnits.RemoveAt(index);
                m_liveUnits.Add(unit);
            }
        }

        public override void RepositionUnitsToStartingPosition()
        {
            for (int i = 0; i < m_allUnitList.Length; i++)
            {
                m_allUnitList[i].transform.position = m_unitStartingPositions[i];
            }
        }

        public override void SetUnits(ArmyUnit[] units)
        {
            m_liveUnits.Clear();
            m_liveUnits.AddRange(units);
            for (int i = 0; i < m_liveUnits.Count; i++)
            {
                m_liveUnits[i].transform.SetParent(m_parent);
            }

            m_allUnitList = new ArmyUnit[units.Length];
            m_unitStartingPositions = new Vector3[units.Length];

            for (int i = 0; i < m_allUnitList.Length; i++)
            {
                var unit = units[i];
                m_allUnitList[i] = unit;
                m_unitStartingPositions[i] = unit.transform.position;
            }
        }

        public override void StopAttack()
        {
            m_visualizer.StopAttack(m_liveUnits);
        }

        public override int GetMaxUnitCount()
        {
            return m_allUnitList.Length;
        }
    }
}