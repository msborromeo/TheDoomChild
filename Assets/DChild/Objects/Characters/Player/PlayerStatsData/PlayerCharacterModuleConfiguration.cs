using DChild.Gameplay.Characters.Players.BattleAbilityModule;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.Modules
{
    [CreateAssetMenu(fileName = "PlayerCharacterModuleConfiguration", menuName = "DChild/Gameplay/Character/Player Character Module Configuration")]
    public class PlayerCharacterModuleConfiguration : ScriptableObject
    {
        [Header("Basic")]
        [Title("Movement")]
        [SerializeField, HideLabel]
        private MovementStatsInfo m_movementInfo;
        public MovementStatsInfo movementStatsInfo => m_movementInfo;

        [Title("Ground Jump")]
        [SerializeField, HideLabel]
        private GroundJumpStatsInfo m_groundJumpInfo;
        public GroundJumpStatsInfo groundJumpInfo => m_groundJumpInfo;

        [Title("Object Manipulation")]
        [SerializeField, HideLabel]
        private ObjectManipulationStatsInfo m_objectManipulationInfo;
        public ObjectManipulationStatsInfo objectManipulationInfo => m_objectManipulationInfo;

        [Title("Ledge Grab")]
        [SerializeField, HideLabel]
        private LedgeGrabStatsInfo m_ledgeGrabStatsInfo;
        public LedgeGrabStatsInfo ledgeGrabStatsInfo => m_ledgeGrabStatsInfo;

        [Title("Shadow Morph")]
        [SerializeField, HideLabel]
        private ShadowMorphStatsInfo m_shadowMorphInfo;
        public ShadowMorphStatsInfo shadowMorphStatsInfo => m_shadowMorphInfo;

        [Title("Auto Step Climb")]
        [SerializeField, HideLabel]
        private AutoStepClimbStatsInfo m_autoStepClimbInfo;
        public AutoStepClimbStatsInfo autoStepClimbStatsInfo => m_autoStepClimbInfo;

        [Header("Combat")]
        [Title("Basic Slashes")]
        [SerializeField, HideLabel]
        private BasicSlashesStatsInfo m_basicSlashesStatsInfo;
        public BasicSlashesStatsInfo basicSlashesStatsInfo => m_basicSlashesStatsInfo;

        [Title("Slash Combo")]
        [SerializeField, HideLabel]
        private SlashComboStatsInfo m_slashComboStatsInfo;
        public SlashComboStatsInfo slashComboStatsInfo => m_slashComboStatsInfo;

        [Title("Player Flinch")]
        [SerializeField, HideLabel]
        private FlinchStatsInfo m_flinchStatsInfo;
        public FlinchStatsInfo flinchStatsInfo => m_flinchStatsInfo;

        [Header("Skills")]
        [Title("Dash")]
        [SerializeField, HideLabel]
        private DashStatsInfo m_dashStatsInfo;
        public DashStatsInfo dashStatsInfo => m_dashStatsInfo;

        [Title("Devil Wings")]
        [SerializeField, HideLabel]
        private DevilWingsStatsInfo m_devilWingsInfo;
        public DevilWingsStatsInfo devilWingsInfo => m_devilWingsInfo;

        [Title("Extra Jump")]
        [SerializeField, HideLabel]
        private ExtraJumpStatsInfo m_extraJumpInfo;
        public ExtraJumpStatsInfo extraJumpInfo => m_extraJumpInfo;

        [Title("Shadow Dash")]
        [SerializeField, HideLabel]
        private ShadowDashStatsInfo m_shadowDashInfo;
        public ShadowDashStatsInfo shadowDashInfo => m_shadowDashInfo;

        [Title("Shadow Slide")]
        [SerializeField, HideLabel]
        private ShadowSlideStatsInfo m_shadowSlideInfo;
        public ShadowSlideStatsInfo shadowSlideInfo => m_shadowSlideInfo;

        [Title("Slide")]
        [SerializeField, HideLabel]
        private SlideStatsInfo m_slideInfo;
        public SlideStatsInfo slideInfo => m_slideInfo;

        [Header("Movement Skills")]
        [Title("Wall Jump")]
        [SerializeField, HideLabel]
        private WallJumpStatsInfo m_wallJumpStatsInfo;
        public WallJumpStatsInfo wallJumpInfo => m_wallJumpStatsInfo;

        [Title("Wall Movement")]
        [SerializeField, HideLabel]
        private WallMovementStatsInfo m_wallMovementStatsInfo;
        public WallMovementStatsInfo wallMovementInfo => m_wallMovementStatsInfo;

        [Title("Wall Slide")]
        [SerializeField, HideLabel]
        private WallSlideStatsInfo m_wallSlideStatsInfo;
        public WallSlideStatsInfo wallSlideInfo => m_wallSlideStatsInfo;

        [Title("Wall Stick")]
        [SerializeField, HideLabel]
        private WallStickStatsInfo m_wallStickStatsInfo;
        public WallStickStatsInfo wallStickInfo => m_wallStickStatsInfo;

        [Header("Combat Skills")]
        [Title("Earth Shaker")]
        [SerializeField, HideLabel]
        private EarthShakerStatsInfo m_earthShakerStatsInfo;
        public EarthShakerStatsInfo earthShakerInfo => m_earthShakerStatsInfo;
        [Title("Sword Thrust")]
        [SerializeField, HideLabel]
        private SwordThrustStatsInfo m_swordThrustStatsInfo;
        public SwordThrustStatsInfo swordThrustInfo => m_swordThrustStatsInfo;
        [Title("Whip Attack")]
        [SerializeField, HideLabel]
        private WhipAttackStatsInfo m_whipAttackStatsInfo;
        public WhipAttackStatsInfo whipAttackInfo => m_whipAttackStatsInfo;
        [Title("Whip Attack Combo")]
        [SerializeField, HideLabel]
        private WhipAttackComboStatsInfo m_whipAttackComboStatsInfo;
        public WhipAttackComboStatsInfo whipAttackComboInfo => m_whipAttackComboStatsInfo;
        [Title("Projectile Throw")]
        [SerializeField, HideLabel]
        private ProjectileThrowStatsInfo m_projectileThrowStatsInfo;
        public ProjectileThrowStatsInfo projectileThrowInfo => m_projectileThrowStatsInfo;
        [Title("Block")]
        [SerializeField, HideLabel]
        private BlockStatsInfo m_blockStatsInfo;
        public BlockStatsInfo blockInfo => m_blockStatsInfo;

        [Header("Misc")]
        [Title("Combat Readiness")]
        [SerializeField, HideLabel]
        private CombatReadinessStatsInfo m_combatReadinessStatsInfo;
        public CombatReadinessStatsInfo combatReadinessInfo => m_combatReadinessStatsInfo;

        [Title("Idle Handle")]
        [SerializeField, HideLabel]
        private IdleHandleStatsInfo m_idleHandleStatsInfo;
        public IdleHandleStatsInfo idleHandleInfo => m_idleHandleStatsInfo;

        [Header("Combat Arts")]
        [Title("Barrier")]
        [SerializeField, HideLabel]
        private BarrierStatsInfo m_barrierStatsInfo;
        public BarrierStatsInfo barrierInfo => m_barrierStatsInfo;

        [Header("Basic Attacks Crit Stats")]
        [Title("Overhead Slash"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_overheadSlashCritStatsInfo;
        public PlayerCritStatsInfo overheadSlashCritStatsInfo => m_overheadSlashCritStatsInfo;

        [Title("Midair Forward Slash"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_midairForwardSlashCritStatsInfo;
        public PlayerCritStatsInfo midairForwardSlashCritStatsInfo => m_midairForwardSlashCritStatsInfo;

        [Title("Midair Overhead Slash"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_midairOverheadSlashCritStatsInfo;
        public PlayerCritStatsInfo midairOverheadSlashCritStatsInfo => m_midairOverheadSlashCritStatsInfo;

        [Title("Crouch Slash"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_crouchSlashSlashCritStatsInfo;
        public PlayerCritStatsInfo crouchSlashSlashCritStatsInfo => m_crouchSlashSlashCritStatsInfo;

        [Title("Slash Combo"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private List<PlayerCritStatsInfo> m_slashComboCritStatsInfo;
        public List<PlayerCritStatsInfo> slashComboCritStatsInfo => m_slashComboCritStatsInfo;


        [Header("Primary Skill Attacks Crit Stats")]
        [Title("Earth Shaker"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_earthShakerCritStatsInfo;
        public PlayerCritStatsInfo earthShakerCritStatsInfo => m_earthShakerCritStatsInfo;

        [Title("Sword Thrust"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_swordThrustCritStatsInfo;
        public PlayerCritStatsInfo swordThrustCritStatsInfo => m_swordThrustCritStatsInfo;

        [Title("Whip Forward"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_whipForwardCritStatsInfo;
        public PlayerCritStatsInfo whipForwardCritStatsInfo => m_whipForwardCritStatsInfo;

        [Title("Whip Overhead"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_whipOverheadCritStatsInfo;
        public PlayerCritStatsInfo whipOverheadCritStatsInfo => m_whipOverheadCritStatsInfo;

        [Title("Whip Midair Forward"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_whipMidairForwardCritStatsInfo;
        public PlayerCritStatsInfo whipMidairForwardCritStatsInfo => m_whipMidairForwardCritStatsInfo;

        [Title("Whip Midair Overhead"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_whipMidairOverheadCritStatsInfo;
        public PlayerCritStatsInfo whipMidairOverheadCritStatsInfo => m_whipMidairOverheadCritStatsInfo;

        [Title("Whip Crouch"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_whipCrouchCritStatsInfo;
        public PlayerCritStatsInfo whipCrouchCritStatsInfo => m_whipCrouchCritStatsInfo;

        [Title("Projectile Throw"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_projectileThrowCritStatsInfo;
        public PlayerCritStatsInfo projectileThrowCritStatsInfo => m_projectileThrowCritStatsInfo;

        [Title("Whip Combo One"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private List<PlayerCritStatsInfo> m_whipComboCritStatsInfo;
        public List<PlayerCritStatsInfo> whipComboCritStatsInfo => m_whipComboCritStatsInfo;

        [Header("Combat Arts Crit Stats")]
        [Title("Reaper Harvest"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_reaperHarvestCritStatsInfo;
        public PlayerCritStatsInfo reaperHarvestCritStatsInfo => m_reaperHarvestCritStatsInfo;

        [Title("Hell Trident"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_hellTridentCritStatsInfo;
        public PlayerCritStatsInfo hellTridentCritStatsInfo => m_hellTridentCritStatsInfo;

        [Title("Edged Fury"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_edgedFuryCritStatsInfo;
        public PlayerCritStatsInfo edgedFuryCritStatsInfo => m_edgedFuryCritStatsInfo;

        [Title("Diagonal Sword Dash"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_diagonalSwordDashCritStatsInfo;
        public PlayerCritStatsInfo diagonalSwordDashCritStatsInfo => m_diagonalSwordDashCritStatsInfo;

        [Title("Air Slash Ranged"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_airSlashRangedCritStatsInfo;
        public PlayerCritStatsInfo airSlashRangedCritStatsInfo => m_airSlashRangedCritStatsInfo;

        [Title("Lightning Spear"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_lightningSpearCritStatsInfo;
        public PlayerCritStatsInfo lightningSpearCritStatsInfo => m_lightningSpearCritStatsInfo;

        [Title("Soul Fire Blast"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_soulFireBlastCritStatsInfo;
        public PlayerCritStatsInfo soulFireBlastCritStatsInfo => m_soulFireBlastCritStatsInfo;

        [Title("Sovereigns Impale"), FoldoutGroup("Critical Hit Stats")]
        [SerializeField, HideLabel]
        private PlayerCritStatsInfo m_sovereignsImpaleCritStatsInfo;
        public PlayerCritStatsInfo sovereignsImpaleCritStatsInfo => m_sovereignsImpaleCritStatsInfo;

    }
}