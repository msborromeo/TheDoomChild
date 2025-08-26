using DChild.Gameplay.ArmyBattle;

public class RandomizedArmyAttack : IArmyAIAction
{
    public bool isRandomizedAction => true;

    ArmyAIAttackInfo IArmyAIAction.GetAction() => new ArmyAIAttackInfo();


}
