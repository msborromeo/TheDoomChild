using DChild.Gameplay.ArmyBattle.SpecialSkills;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI
{

    public class AttackingGroupOptionUI : MonoBehaviour
    {
        [SerializeField]
        private ArmyPartyNameUI m_partyName;
        public ArmyPartyNameUI partyName => m_partyName;

        [SerializeField]
        private ArmyCharacterGroupUI m_characterGroupUI;
        public ArmyCharacterGroupUI characterGroupUI => m_characterGroupUI;

        [SerializeField]
        private AttackingGroupPowerUI m_attackPowerUI;
        public AttackingGroupPowerUI attackingPowerUI => m_attackPowerUI;

        [SerializeField]
        private SelectedSkillButton m_selectedSkill;
        public SelectedSkillButton selectedSkill => m_selectedSkill;

        [FoldoutGroup("GLOW OVERRIDE")]
        [SerializeField, FoldoutGroup("GLOW OVERRIDE/ASSETS")]
        private Sprite m_meleeGlow;
        [SerializeField, FoldoutGroup("GLOW OVERRIDE/ASSETS")]
        private Sprite m_rangeGlow;
        [SerializeField, FoldoutGroup("GLOW OVERRIDE/ASSETS")]
        private Sprite m_magicGlow;
        [SerializeField, FoldoutGroup("GLOW OVERRIDE/ASSETS")]
        private List<Image> m_partyGlow;


        public virtual void Display(IAttackingGroup group)
        {
            DamageType m_damageType = group.GetDamageType();
            m_characterGroupUI.Display(group?.GetCharacterGroup() ?? null);
            m_selectedSkill.Display(m_damageType);
            m_partyName.Display(group);
            m_attackPowerUI.Display(group);

            switch (m_damageType)
            {
                case DamageType.Melee:
                    SelectGlow(m_meleeGlow);
                    break;
                case DamageType.Range:
                    SelectGlow(m_rangeGlow);
                    break;
                case DamageType.Magic:
                    SelectGlow(m_magicGlow);
                    break;

            }
        }


        public virtual void SelectGlow(Sprite glow)
        {
            foreach (Image glowClass in m_partyGlow)
            {
                glowClass.sprite = glow;
            }
        }
    }
}
