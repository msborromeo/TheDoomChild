using System.Collections;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills
{
    public interface ISpecialSkillIEnumeratorModule
    {
        IEnumerator ApplyEffect(ArmyController owner, ArmyController target);
        IEnumerator RemoveEffect(ArmyController owner, ArmyController target);
    }
}

