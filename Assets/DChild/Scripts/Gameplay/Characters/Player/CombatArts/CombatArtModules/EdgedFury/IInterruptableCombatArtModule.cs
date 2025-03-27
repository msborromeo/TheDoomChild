using DChild.Gameplay.Characters.Players.Modules;

namespace DChild.Gameplay.Characters.Players.BattleAbilityModule
{
    public interface IInterruptableCombatArtModule
    {
        void EndExecution();
        void Cancel();
    }
}