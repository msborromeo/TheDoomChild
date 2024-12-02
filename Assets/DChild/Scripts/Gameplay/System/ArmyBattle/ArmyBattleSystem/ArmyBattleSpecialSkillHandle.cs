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
                Destroy(m_ownerVisuals.gameObject);
                Destroy(m_targetVisuals.gameObject);
            }
        }

        [SerializeField]
        private SignalSender m_skillActivationEndSignal;
        [SerializeField]
        private List<ActiveSkill> m_waitingTypeSkillList;
        [SerializeField]
        private List<ActiveSkill> m_turnTypeSkillList;

        public void Activate(SpecialSkill specialSkill, ArmyController owner)
        {
            if (specialSkill != null && owner != null)
            {
                var target = ArmyBattleSystem.GetTargetOf(owner);
                var skill = new ActiveSkill(specialSkill, owner, target);
                //This creates a Circle Dependency

                switch (specialSkill.type)
                {
                    case SpecialSkill.Type.Instant:
                        StartCoroutine(ApplyInstantSpecialSkillRoutine(skill, owner, target));
                        break;
                    case SpecialSkill.Type.Turn:
                        m_turnTypeSkillList.Add(skill);
                        break;
                    case SpecialSkill.Type.Waiting:
                        m_waitingTypeSkillList.Add(skill);
                        break;
                }
            }
        }

        public IEnumerator ApplyWaitingSkillsRoutine()
        {
            for (int i = 0; i < m_waitingTypeSkillList.Count; i++)
            {
                var currentSkill = m_waitingTypeSkillList[i];
                currentSkill.turnsLeft -= 1;
                if (currentSkill.turnsLeft == 0)
                {
                    var owner = currentSkill.owner;
                    yield return currentSkill.specialSkill.ApplyEffect(owner, ArmyBattleSystem.GetTargetOf(owner)); ;
                    currentSkill.DestroyVisuals();
                }
                else
                {
                    currentSkill.PlayVisuals();
                    while (currentSkill.AreSkillVFXDonePlaying == false)
                        yield return null;
                }
            }

            for (int i = m_waitingTypeSkillList.Count - 1; i >= 0; i--)
            {
                var currentSkill = m_waitingTypeSkillList[i];
                if (currentSkill.turnsLeft == 0)
                {
                    currentSkill.DestroyVisuals();
                    m_waitingTypeSkillList.RemoveAt(i);
                }
            }
        }

        public IEnumerator ApplyTurnSpecialSkillsRoutine()
        {
            for (int i = 0; i < m_turnTypeSkillList.Count; i++)
            {
                var currentSkill = m_turnTypeSkillList[i];
                var owner = currentSkill.owner;
                yield return currentSkill.specialSkill.ApplyEffect(owner, ArmyBattleSystem.GetTargetOf(owner));
            }

            m_turnTypeSkillList.Clear();

            m_skillActivationEndSignal?.SendSignal();
        }


        private IEnumerator ApplyInstantSpecialSkillRoutine(ActiveSkill skill, ArmyController owner, ArmyController target)
        {
            yield return skill.specialSkill.ApplyEffect(owner, target);
            m_skillActivationEndSignal?.SendSignal();
        }

    }
}