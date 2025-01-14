using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
   
    public class AddInventoryItems : ISpecialSkillModule, ISpecialSkillImplementor
    {
        [SerializeField]
        private int m_stolenEssence;
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            var reward = GameplaySystem.playerManager.player.inventory;
            reward.AddSoulEssence(m_stolenEssence);
        }   

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {

        }
    }
}

