namespace DChild.Gameplay.Combat.StatusAilment
{
    public interface IStatusEffectInfo
    {
        StatusEffectType type { get; }

        float durationPercentage { get; }
    }
}