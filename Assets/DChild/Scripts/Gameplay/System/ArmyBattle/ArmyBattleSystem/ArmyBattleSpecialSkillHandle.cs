using Doozy.Runtime.Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills
{
    public class ArmyBattleSpecialSkillHandle : MonoBehaviour
    {
        [System.Serializable]
        private class ActiveSkill
        {
            [SerializeField]
            private SpecialSkill m_specialSkill;
            [SerializeField]
            private ArmyController m_owner;

            public int turnsLeft;
            private ISpecialSkillVisuals m_ownerVisuals;
            private ISpecialSkillVisuals m_targetVisuals;


            public ActiveSkill(SpecialSkill specialSkill, ArmyController owner, ArmyController target)
            {
                m_specialSkill = specialSkill;
                m_owner = owner;
                turnsLeft = specialSkill.duration;

                var visualizerInfo = specialSkill.visualizerInfo;
                if (visualizerInfo.ownerFX != null)
                {
                    m_ownerVisuals = Object.Instantiate(visualizerInfo.ownerFX).GetComponent<ISpecialSkillVisuals>();
                    m_ownerVisuals.transform.position = ArmyBattleSystem.GetBattlationPositionOf(owner);

                }

                if (visualizerInfo.targetFX != null)
                {
                    m_targetVisuals = Object.Instantiate(visualizerInfo.targetFX).GetComponent<ISpecialSkillVisuals>();
                    m_targetVisuals.transform.position = ArmyBattleSystem.GetBattlationPositionOf(target);
                }
            }

            private int activationTurnCount => turnsLeft - m_specialSkill.duration;

            public SpecialSkill specialSkill => m_specialSkill;
            public ArmyController owner => m_owner;

            public bool AreSkillVFXDonePlaying => m_ownerVisuals.isEffectDone && m_targetVisuals.isEffectDone;

            public void PlayVisuals()
            {
                m_ownerVisuals?.Play(activationTurnCount);
                m_targetVisuals?.Play(activationTurnCount);
            }

            public void DestroyVisuals()
            {
                if (m_ownerVisuals != null)
                {
                    Destroy(m_ownerVisuals.gameObject);
                }
                if (m_targetVisuals != null)
                {
                    Destroy(m_targetVisuals.gameObject);
                }

            }
        }

        [System.Serializable]
        private class ActiveSkillList
        {
            [SerializeField]
            private List<ActiveSkill> m_waitingTypeSkillList;
            [SerializeField]
            private List<ActiveSkill> m_turnTypeSkillList;

            public int totalSkillCount => m_waitingTypeSkillList.Count + m_turnTypeSkillList.Count;

            public List<ActiveSkill> GetSkillTypeList(SpecialSkill.Type type)
            {
                switch (type)
                {
                    case SpecialSkill.Type.Turn:
                        return m_turnTypeSkillList;
                    case SpecialSkill.Type.Waiting:
                        return m_waitingTypeSkillList;
                    default:
                        return null;
                }
            }
        }

        [SerializeField]
        private SignalSender m_skillActivationEndSignal;
        [SerializeField]
        private int m_maxPlayerActiveSkills;
        [SerializeField]
        private ActiveSkillList m_playerActiveSkills;
        [SerializeField]
        private ActiveSkillList m_enemyActiveSkills;

        public bool CanPlayerActivateMoreSkills() => m_playerActiveSkills.totalSkillCount < m_maxPlayerActiveSkills;

        public void Activate(SpecialSkill specialSkill, ArmyController owner)
        {
            if (specialSkill != null && owner != null)
            {
                var target = ArmyBattleSystem.GetTargetOf(owner);
                StartCoroutine(ActivateSkillRoutine(specialSkill, owner, target));
            }
        }

        private IEnumerator ActivateSkillRoutine(SpecialSkill specialSkill, ArmyController owner, ArmyController target)
        {
            var skill = new ActiveSkill(specialSkill, owner, target);

            yield return specialSkill.ExecuteOnSelect(owner, target);

            switch (specialSkill.type)
            {
                case SpecialSkill.Type.Instant:
                    yield return ApplyInstantSpecialSkillRoutine(skill, owner, target);
                    break;
                case SpecialSkill.Type.Turn:
                    var turnActiveList = ArmyBattleSystem.GetPlayer() == owner ? m_playerActiveSkills.GetSkillTypeList(specialSkill.type) : m_enemyActiveSkills.GetSkillTypeList(specialSkill.type);
                    turnActiveList.Add(skill);
                    //StartCoroutine(ApplyTurnSpecialSkillsRoutine(turnActiveList));
                    break;
                case SpecialSkill.Type.Waiting:
                    var activeList = ArmyBattleSystem.GetPlayer() == owner ? m_playerActiveSkills.GetSkillTypeList(specialSkill.type) : m_enemyActiveSkills.GetSkillTypeList(specialSkill.type);
                    activeList.Add(skill);
                    break;
            }
        }

        public IEnumerator ApplyWaitingSkillsRoutine()
        {
            var skillType = SpecialSkill.Type.Waiting;
            yield return ApplyWaitingSkillsRoutine(m_playerActiveSkills.GetSkillTypeList(skillType));
            yield return ApplyWaitingSkillsRoutine(m_enemyActiveSkills.GetSkillTypeList(skillType));
        }

        private IEnumerator ApplyWaitingSkillsRoutine(List<ActiveSkill> waitingSkills)
        {
            for (int i = 0; i < waitingSkills.Count; i++)
            {
                var currentSkill = waitingSkills[i];
                currentSkill.turnsLeft -= 1;
                if (currentSkill.turnsLeft <= 0)
                {
                    var owner = currentSkill.owner;
                    yield return currentSkill.specialSkill.ApplyEffect(owner, ArmyBattleSystem.GetTargetOf(owner)); ;
                }
                else
                {
                    currentSkill.PlayVisuals();
                    while (currentSkill.AreSkillVFXDonePlaying == false)
                        yield return null;
                }
            }

            for (int i = waitingSkills.Count - 1; i >= 0; i--)
            {
                var currentSkill = waitingSkills[i];
                if (currentSkill.turnsLeft <= 0)
                {
                    currentSkill.DestroyVisuals();
                    waitingSkills.RemoveAt(i);
                }
            }
        }

        public IEnumerator ApplyTurnSpecialSkillsRoutine()
        {
            var skillType = SpecialSkill.Type.Turn;
            yield return ApplyTurnSpecialSkillsRoutine(m_playerActiveSkills.GetSkillTypeList(skillType));
            yield return ApplyTurnSpecialSkillsRoutine(m_enemyActiveSkills.GetSkillTypeList(skillType));

            m_skillActivationEndSignal?.SendSignal();
        }

        private IEnumerator ApplyTurnSpecialSkillsRoutine(List<ActiveSkill> turnSkills)
        {
            for (int i = 0; i < turnSkills.Count; i++)
            {
                var currentSkill = turnSkills[i];
                var owner = currentSkill.owner;
                yield return currentSkill.specialSkill.ApplyEffect(owner, ArmyBattleSystem.GetTargetOf(owner));
            }

            turnSkills.Clear();
        }

        private IEnumerator ApplyInstantSpecialSkillRoutine(ActiveSkill skill, ArmyController owner, ArmyController target)
        {
            yield return skill.specialSkill.ApplyEffect(owner, target);
            m_skillActivationEndSignal?.SendSignal();
        }

    }
}