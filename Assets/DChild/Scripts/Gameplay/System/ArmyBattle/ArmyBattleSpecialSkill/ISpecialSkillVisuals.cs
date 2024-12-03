using UnityEngine;

namespace DChild.Gameplay.ArmyBattle
{
    public interface ISpecialSkillVisuals
    {
        Transform transform { get; }
        void Play(int turnCount);
        bool isEffectDone { get; }
        GameObject gameObject { get; }
    }
}

