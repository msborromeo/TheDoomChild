using DChild.Gameplay.ArmyBattle;
public interface IArmyAIAction
{
    public ArmyAIAttackInfo GetAction();
    bool isRandomizedAction { get; }

}
