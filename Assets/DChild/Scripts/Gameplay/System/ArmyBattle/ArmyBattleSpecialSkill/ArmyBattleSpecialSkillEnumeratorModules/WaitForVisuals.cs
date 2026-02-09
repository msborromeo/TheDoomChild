using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    [System.Serializable]
    public class WaitForVisuals : ISpecialSkillIEnumeratorModule
    {
        private enum VisualType
        {
            Spine,
            VFX
        }

        private enum Position
        {
            Owner,
            Target,
            Center
        }

        [SerializeField]
        private GameObject m_fx;
        [SerializeField]
        private VisualType m_visualType;
        [SerializeField]
        private Position m_position;
        [SerializeField]
        private ArmySpecialSkillVfx m_fxStatus;

        public IEnumerator ApplyEffect(ArmyController owner, ArmyController target)
        {
            switch (m_visualType)
            {
                case VisualType.Spine:
                    yield return HandleSpineVisuals(owner,target);
                    break;
                case VisualType.VFX:
                    yield return HandleSpineVFX(owner, target);
                    break;
            }
        }

        public IEnumerator RemoveEffect(ArmyController owner, ArmyController target)
        {
            yield return null;
        }

        private IEnumerator HandleSpineVisuals(ArmyController owner, ArmyController target)
        {
            var fx = CreateFX(owner, target);
            yield return null;

            bool isAnimationOver = false;
            fx.GetComponent<SkeletonAnimation>().AnimationState.Complete += OnAnimationEnd;
            while (isAnimationOver == false)
                yield return null;

            Object.Destroy(fx);

            void OnAnimationEnd(TrackEntry trackEntry)
            {
                isAnimationOver = true;
                fx.GetComponent<SkeletonAnimation>().AnimationState.Complete -= OnAnimationEnd;
            }
        }

        private IEnumerator HandleSpineVFX(ArmyController owner, ArmyController target)
        {
            var fx = CreateFX(owner, target);
            yield return null;

            var specialSkillFX = fx.GetComponent<ArmySpecialSkillVfx>();
            specialSkillFX.PlayEffects();
            while (!specialSkillFX.m_iseffectdone)
            {
                
                yield return null; 
            }
            Debug.Log("Effects finish!");
            //Temporary thing since ArmySpecialSkillVfx doesnt have an event that it is done atm
            yield return new WaitForSeconds(3f);
            specialSkillFX.StopEffects();

            Object.Destroy(fx);
        }


        private GameObject CreateFX(ArmyController owner, ArmyController target)
        {
            var fx = Object.Instantiate(m_fx) as GameObject;
            switch (m_position)
            {
                case Position.Owner:
                    fx.transform.position = ArmyBattleSystem.GetBattalionPosition(owner);
                    break;
                case Position.Target:
                    fx.transform.position = ArmyBattleSystem.GetBattalionPosition(target);
                    break;
                case Position.Center:
                    fx.transform.SetParent(owner.transform);
                    break;
            }
            fx.transform.SetParent(null);

            return fx;
        }
    }
}