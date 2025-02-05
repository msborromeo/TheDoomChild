using DChild.Gameplay.Combat;
using Holysoft.Event;
using UnityEngine;
using DChild.Gameplay.Characters.Players.BattleAbilityModule;
using DChild.Inputs;
using System;
using UnityEngine.UIElements;

namespace DChild.Gameplay.Characters.Players.Modules
{
    public class UnderworldPlayerController : MonoBehaviour, IMainController
    {
        [SerializeField]
        private InputReader m_inputReader;
        [SerializeField]
        private PlayerModuleActivator m_skills;
        [SerializeField]
        private Rigidbody2D m_rigidbody;
        [SerializeField]
        private CombatArts m_abilities;
        [SerializeField]
        private Character m_character;
        [SerializeField]
        private CharacterState m_state;

        private IDash m_activeDash;
        private ISlide m_activeSlide;

        #region Modules
        private PlayerStatisticTracker m_tracker;
        private GroundednessHandle m_groundedness;
        private PlayerPhysicsMatHandle m_physicsMat;
        private IdleHandle m_idle;
        private CombatReadiness m_combatReadiness;
        private PlayerFlinch m_flinch;
        private PlayerDeath m_death;
        private InitialDescentBoost m_initialDescentBoost;
        private ObjectInteraction m_objectInteraction;
        private ShadowGaugeRegen m_shadowGaugeRegen;
        private ObjectManipulation m_objectManipulation;
        private PlayerOneWayPlatformDropHandle m_platformDrop;

        private Movement m_movement;
        private Crouch m_crouch;
        private Dash m_dash;
        private Slide m_slide;
        private LedgeGrab m_ledgeGrab;
        private GroundJump m_groundJump;
        private ExtraJump m_extraJump;
        private DevilWings m_devilWings;
        private ShadowDash m_shadowDash;
        private ShadowSlide m_shadowSlide;
        private ShadowMorph m_shadowMorph;
        private AutoStepClimb m_stepClimb;
        private WallStick m_wallStick;
        private WallMovement m_wallMovement;
        private WallSlide m_wallSlide;
        private WallJump m_wallJump;

        private CollisionRegistrator m_attackRegistrator;
        private BasicSlashes m_basicSlashes;
        private SlashCombo m_slashCombo;
        private SwordThrust m_swordThrust;
        private EarthShaker m_earthShaker;
        private WhipAttack m_whip;
        private WhipAttackCombo m_whipCombo;
        private ProjectileThrow m_projectileThrow;
        private PlayerBlock m_block;
        private PlayerIntroControlsController m_introController;
        private ChargeAttackHandle m_chargeAttackHandle;
        private ShadowbladeFX m_shadowBladeFX;

        private ReaperHarvest m_reaperHarvest;
        private KrakenRage m_krakenRage;
        private SovereignImpale m_sovereignImpale;
        private HellTrident m_hellTrident;
        private FoolsVerdict m_foolsVerdict;
        private SoulFireBlast m_soulFireBlast;
        private EdgedFury m_edgedFury;
        private BackDiver m_backDiver;
        private Barrier m_barrier;
        private DiagonalSwordDash m_diagonalSwordDash;
        private ChampionsUprising m_championsUprising;
        private LightningSpear m_lightningSpear;
        private IcarusWings m_icarusWings;
        private TeleportingSkull m_teleportingSkull;
        private AirSlashRange m_airSlashRange;
        #endregion

        private bool m_updateEnabled = true;

        #region Input Variables
        [SerializeField]
        private Vector2 m_vector2Input;

        #endregion

        public event EventAction<EventActionArgs> ControllerDisabled;
        public event EventAction<EventActionArgs> ControllerEnabled;

        #region Usual Unity Stuff
        private void Awake()
        {
            m_chargeAttackHandle = new ChargeAttackHandle();

            m_tracker = m_character.GetComponentInChildren<PlayerStatisticTracker>();
            m_groundedness = m_character.GetComponentInChildren<GroundednessHandle>();
            m_physicsMat = m_character.GetComponentInChildren<PlayerPhysicsMatHandle>();
            m_idle = m_character.GetComponentInChildren<IdleHandle>();
            m_combatReadiness = m_character.GetComponentInChildren<CombatReadiness>();
            m_flinch = m_character.GetComponentInChildren<PlayerFlinch>();
            m_death = m_character.GetComponentInChildren<PlayerDeath>();
            m_initialDescentBoost = m_character.GetComponentInChildren<InitialDescentBoost>();
            m_objectInteraction = m_character.GetComponentInChildren<ObjectInteraction>();
            m_shadowGaugeRegen = m_character.GetComponentInChildren<ShadowGaugeRegen>();
            m_shadowGaugeRegen.Enable(true);
            m_objectManipulation = m_character.GetComponentInChildren<ObjectManipulation>();
            m_platformDrop = m_character.GetComponentInChildren<PlayerOneWayPlatformDropHandle>();

            m_movement = m_character.GetComponentInChildren<Movement>();
            m_crouch = m_character.GetComponentInChildren<Crouch>();
            m_dash = m_character.GetComponentInChildren<Dash>();
            m_slide = m_character.GetComponentInChildren<Slide>();
            m_ledgeGrab = m_character.GetComponentInChildren<LedgeGrab>();
            m_groundJump = m_character.GetComponentInChildren<GroundJump>();
            m_extraJump = m_character.GetComponentInChildren<ExtraJump>();
            m_devilWings = m_character.GetComponentInChildren<DevilWings>();
            m_shadowDash = m_character.GetComponentInChildren<ShadowDash>();
            m_shadowSlide = m_character.GetComponentInChildren<ShadowSlide>();
            m_shadowMorph = m_character.GetComponentInChildren<ShadowMorph>();
            m_wallStick = m_character.GetComponentInChildren<WallStick>();
            m_wallMovement = m_character.GetComponentInChildren<WallMovement>();
            m_wallSlide = m_character.GetComponentInChildren<WallSlide>();
            m_wallJump = m_character.GetComponentInChildren<WallJump>();
            m_stepClimb = m_character.GetComponentInChildren<AutoStepClimb>();

            m_attackRegistrator = m_character.GetComponentInChildren<CollisionRegistrator>();
            m_basicSlashes = m_character.GetComponentInChildren<BasicSlashes>();
            m_slashCombo = m_character.GetComponentInChildren<SlashCombo>();
            m_swordThrust = m_character.GetComponentInChildren<SwordThrust>();
            m_earthShaker = m_character.GetComponentInChildren<EarthShaker>();
            m_whip = m_character.GetComponentInChildren<WhipAttack>();
            m_whipCombo = m_character.GetComponentInChildren<WhipAttackCombo>();
            m_projectileThrow = m_character.GetComponentInChildren<ProjectileThrow>();
            m_block = m_character.GetComponentInChildren<PlayerBlock>();

            m_shadowBladeFX = m_character.GetComponentInChildren<ShadowbladeFX>();

            m_reaperHarvest = m_character.GetComponentInChildren<ReaperHarvest>();
            m_krakenRage = m_character.GetComponentInChildren<KrakenRage>();
            m_sovereignImpale = m_character.GetComponentInChildren<SovereignImpale>();
            m_hellTrident = m_character.GetComponentInChildren<HellTrident>();
            m_foolsVerdict = m_character.GetComponentInChildren<FoolsVerdict>();
            m_soulFireBlast = m_character.GetComponentInChildren<SoulFireBlast>();
            m_edgedFury = m_character.GetComponentInChildren<EdgedFury>();
            m_backDiver = m_character.GetComponentInChildren<BackDiver>();
            m_barrier = m_character.GetComponentInChildren<Barrier>();
            m_diagonalSwordDash = m_character.GetComponentInChildren<DiagonalSwordDash>();
            m_championsUprising = m_character.GetComponentInChildren<ChampionsUprising>();
            m_lightningSpear = m_character.GetComponentInChildren<LightningSpear>();
            m_icarusWings = m_character.GetComponentInChildren<IcarusWings>();
            m_teleportingSkull = m_character.GetComponentInChildren<TeleportingSkull>();
            m_airSlashRange = m_character.GetComponentInChildren<AirSlashRange>();


            //Intro Controller
            m_introController = GetComponent<PlayerIntroControlsController>();

            //Abilities
            m_abilities = GetComponentInParent<Player>().GetComponentInChildren<CombatArts>();
            m_updateEnabled = true;
        }

        private void OnEnable()
        {
            m_groundedness.StateChange += OnGroundednessStateChange;
            m_flinch.OnExecute += OnFlinch;
            m_death.OnExecute += OnDeath;
            m_projectileThrow.ExecutionRequested += OnProjectileThrowRequest;
            m_projectileThrow.ProjectileThrown += ResetProjectile;
            m_teleportingSkull.Teleported += HasTeleported;

            //action handles
            m_inputReader.Vector2InputPerformedEvent += OnVector2PerformedInput;
            m_inputReader.Vector2CancelledInputEvent += OnVector2CancelledInput;
            m_inputReader.JumpPerformedEvent += OnJumpPerformedInput;
            m_inputReader.JumpCancelledEvent += OnJumpCancelledInput;
            m_inputReader.JumpStartedEvent += OnJumpStartedInput;
            m_inputReader.PauseStartedEvent += OnPauseInput;
            m_inputReader.StoreStartedEvent += OnStoreInput;
            m_inputReader.DashStartedEvent += OnDashStartedInput;
            m_inputReader.LevitateStartedEvent += OnLevitateStartedInput;
            m_inputReader.LevitatePerformedEvent += OnLevitateInput;
            m_inputReader.LevitateCancelledEvent += OnLevitateCancelledInput;
            m_inputReader.InteractStartedEvent += OnInteractInput;
            m_inputReader.ShadowMorphStartedEvent += OnShadowMorphStartedInput;

        }

        private void OnDisable()
        {
            m_groundedness.StateChange -= OnGroundednessStateChange;
            m_flinch.OnExecute -= OnFlinch;
            m_death.OnExecute -= OnDeath;
            m_projectileThrow.ExecutionRequested -= OnProjectileThrowRequest;
            m_projectileThrow.ProjectileThrown -= ResetProjectile;
            m_teleportingSkull.Teleported -= HasTeleported;

            //action handles
            m_inputReader.Vector2InputPerformedEvent += OnVector2PerformedInput;
            m_inputReader.Vector2CancelledInputEvent += OnVector2CancelledInput;
            m_inputReader.JumpPerformedEvent -= OnJumpPerformedInput;
            m_inputReader.JumpCancelledEvent -= OnJumpCancelledInput;
            m_inputReader.JumpStartedEvent -= OnJumpStartedInput;
            m_inputReader.PauseStartedEvent -= OnPauseInput;
            m_inputReader.StoreStartedEvent -= OnStoreInput;
            m_inputReader.DashStartedEvent -= OnDashStartedInput;
            m_inputReader.LevitateStartedEvent -= OnLevitateStartedInput;
            m_inputReader.LevitatePerformedEvent -= OnLevitateInput;
            m_inputReader.LevitateCancelledEvent -= OnLevitateCancelledInput;
            m_inputReader.InteractStartedEvent -= OnInteractInput;
            m_inputReader.ShadowMorphStartedEvent -= OnShadowMorphStartedInput;
        }

        private void FixedUpdate()
        {
            if (m_state.isDead)
                return;

            if (m_introController.IsUsingIntroControls())
            {
                m_introController.HandleIntroControlsFixedUpdate();
                return;
            }

            if (m_state.isGrounded)
            {
                if (m_state.forcedCurrentGroundedness == false)
                {
                    m_groundedness?.Evaluate();
                }

                if (m_groundedness?.isUsingCoyote ?? false)
                {
                    m_physicsMat.SetPhysicsTo(PlayerPhysicsMatHandle.Type.Midair);
                }
                else
                {
                    m_physicsMat.SetPhysicsTo(PlayerPhysicsMatHandle.Type.Ground);
                }
            }
            else
            {
                if (m_earthShaker.CanEarthShaker())
                {
                    if (m_state.isShadowBlade && !m_shadowBladeFX.canShadowblade)
                    {
                        m_shadowBladeFX.EnableShadowblade();
                    }
                    else if (!m_state.isShadowBlade && m_shadowBladeFX.canShadowblade)
                    {
                        m_shadowBladeFX.DisableShadowblade();
                    }
                }

                m_initialDescentBoost?.Handle();
                if (m_rigidbody.velocity.y < m_groundedness?.groundCheckOffset)
                {
                    if (m_state.forcedCurrentGroundedness == false)
                    {
                        m_groundedness?.Evaluate();
                    }
                    m_extraJump?.EndExecution();
                }
            }
        }

        private void Update()
        {
            if (m_updateEnabled == false)
                return;

            if (m_state.isDead)
                return;

            if (m_introController.IsUsingIntroControls())
            {
                m_introController.HandleIntroControls();
                return;
            }

            if (m_shadowGaugeRegen?.CanRegen() ?? false)
            {
                m_shadowGaugeRegen.Execute();
            }

            m_platformDrop.HandleDroppablePlatformCollision();

            if (m_state.isInShadowMode)
            {
                if (m_shadowMorph.HaveEnoughSourceForExecution())
                {
                    m_shadowMorph.ConsumeSource();
                }
                else
                {
                    m_shadowMorph?.Cancel();
                    m_shadowGaugeRegen?.Enable(true);
                }
            }

            if (m_state.waitForBehaviour /*|| !m_earthShaker.CanEarthShaker()*/)
                return;

            if (m_state.isCombatReady)
            {
                m_combatReadiness?.HandleDuration();
            }

            if (m_slashCombo.CanSlashCombo() == false)
            {
                m_slashCombo.HandleSlashComboTimer();
            }

            if (m_slashCombo.CanMove() == false)
            {
                m_slashCombo.HandleMovementTimer();
            }

            if (m_whipCombo.CanWhipCombo() == false)
            {
                m_whipCombo.HandleComboTimer();
            }

            if (m_whipCombo.CanMove() == false)
            {
                m_whipCombo.HandleMovementTimer();
            }

            if (m_whip.CanMove() == false)
            {
                m_whip.HandleMovementTimer();
            }

            #region Combat Arts Cooldowns

            if (m_sovereignImpale.CanSovereignImpale() == false)
            {
                m_sovereignImpale.HandleAttackTimer();
            }

            if (m_sovereignImpale.CanMove() == false)
            {
                m_sovereignImpale.HandleMovementTimer();
            }

            if (m_hellTrident.CanHellTrident() == false)
            {
                m_hellTrident.HandleAttackTimer();
            }

            if (m_hellTrident.CanMove() == false)
            {
                m_hellTrident.HandleMovementTimer();
            }

            if (m_foolsVerdict.CanFoolsVerdict() == false)
            {
                m_foolsVerdict.HandleAttackTimer();
            }

            if (m_foolsVerdict.CanMove() == false)
            {
                m_foolsVerdict.HandleMovementTimer();
            }

            if (m_soulFireBlast.CanSoulFireBlast() == false)
            {
                m_soulFireBlast.HandleAttackTimer();
            }

            if (m_championsUprising.CanChampionsUprising() == false)
            {
                m_championsUprising.HandleAttackTimer();
            }

            if (m_barrier.CanMove() == false)
            {
                m_barrier.HandleMovementTimer();
            }

            if (m_lightningSpear.CanReset() == true)
            {
                m_lightningSpear.HandleResetTimer();
            }

            if (m_lightningSpear.CanMove() == false)
            {
                m_lightningSpear.HandleMovementTimer();
            }

            if (m_icarusWings.CanIcarusWings() == false)
            {
                m_icarusWings.HandleAttackTimer();
            }

            if (m_airSlashRange.CanReset() == true)
            {
                m_airSlashRange.HandleResetTimer();
            }

            if (m_airSlashRange.CanMove() == false)
            {
                m_airSlashRange.HandleMovementTimer();
            }
            #endregion

            if (m_state.canAttack == true)
            {
                m_slashCombo.HandleComboResetTimer();
                m_whipCombo.HandleComboResetTimer();
            }
            else
            {
                if (m_state.isAttacking == false)
                {
                    m_basicSlashes.HandleNextAttackDelay();
                    m_slashCombo.HandleComboAttackDelay();
                    m_whip.HandleNextAttackDelay();
                    m_whipCombo.HandleComboAttackDelay();
                    m_projectileThrow.HandleNextAttackDelay();
                }
            }

            if (m_state.isGrounded)
            {
                HandleGroundBehaviour();
                m_basicSlashes?.ResetAerialGravityControl();
                m_basicSlashes?.ResetAirAttacks();
                m_whip?.ResetAerialGravityControl();
                m_whip?.ResetAirAttacks();
                m_devilWings?.EnableLevitate();

                #region Combat Arts Cooldowns
                if (m_diagonalSwordDash.CanDiagonalSwordDash() == false)
                {
                    m_diagonalSwordDash.HandleAttackTimer();
                }

                if (m_backDiver.CanBackDiver() == false)
                {
                    m_backDiver.HandleAttackTimer();
                }

                if (m_reaperHarvest.CanReaperHarvest() == false)
                {
                    m_reaperHarvest.HandleAttackTimer();
                }


                if (m_edgedFury.CanEdgedFury() == false)
                {
                    m_edgedFury.HandleAttackTimer();
                }

                if (m_lightningSpear.CanLightningSpear() == false)
                {
                    m_lightningSpear.HandleAttackTimer();
                }

                if (m_airSlashRange.CanAirSlashRange() == false)
                {
                    m_airSlashRange.HandleAttackTimer();
                }
                #endregion
            }
            else
            {
                HandleAirBehaviour();
            }

            if (m_state.isStickingToWall)
            {
                WallStickMovementAction();
                return;
            } 
            if(m_state.isHighJumping)
            {
                if (m_rigidbody.velocity.y <= (m_groundJump?.highJumpCutoffThreshold ?? 0f))
                {
                    m_groundJump?.EndExecution();
                }
                m_groundJump?.HandleCutoffTimer();
            }

            MoveAction();
            LedgeGrabMovementAction();

            if (m_skills.IsModuleActive(PrimarySkill.WallMovement))
            {
                if (m_state.isGrounded == false)
                {
                    if (m_wallStick?.IsThereAWall() ?? false)
                    {
                        m_wallStick.Execute();
                    }
                }
            }
        }
        #endregion

        #region Input Handles
        private void OnVector2PerformedInput(Vector2 vector)
        {
            m_vector2Input = vector;

            if (m_state.isGrounded)
            {
                //Grounded Movement
                //Crouch handling
                if (vector.y < 0)
                {
                    m_crouch?.Execute();
                    m_movement?.SwitchConfigTo(Movement.Type.Crouch);
                }
                else
                {
                    if (m_crouch?.IsThereNoCeiling() ?? true)
                    {
                        m_crouch?.Cancel();
                        m_movement?.SwitchConfigTo(Movement.Type.Jog);
                    }
                }
            }
            else
            {
                //Air Movement
                if (m_skills.IsModuleActive(PrimarySkill.WallMovement))
                {
                    Debug.Log("Checking Vector2 input in midair");
                    if (m_wallStick?.IsThereAWall() ?? false)
                    {
                        if (vector.x != 0 && (Mathf.Sign(vector.x) == (float)m_character.facing))
                        {
                            //m_wallStick?.Execute();
                            if (m_state.isLevitating)
                            {
                                m_devilWings?.Cancel();
                            }
                            m_dash?.Reset();
                            m_extraJump?.Reset();
                        }
                    }
                }
            }
        }

        private void OnVector2CancelledInput(Vector2 vector)
        {
            m_vector2Input = new Vector2(0, 0);

            if (m_state.isCrouched)
            {
                if (m_crouch?.IsThereNoCeiling() ?? true)
                {
                    m_crouch?.Cancel();
                    m_movement?.SwitchConfigTo(Movement.Type.Jog);
                }
            }

            if (m_state.isStickingToWall)
            {
                if (m_wallSlide.IsThereAWall())
                {
                    m_wallSlide?.Execute();
                }
            }

            if (m_state.isStickingToWall == false)
            {
                m_movement.Cancel();
                m_idle?.Execute(m_state.allowExtendedIdle);
            }
        }

        private void OnJumpCancelledInput()
        {
            if (m_state.isHighJumping)
            {
                if(m_groundJump?.CanCutoffJump() ?? true)
                {
                    m_groundJump?.CutOffJump();
                }
            }
        }

        private void OnJumpStartedInput()
        {
            if (m_state.isGrounded)
            {
                if (m_platformDrop?.IsThereADroppablePlatform() == true && m_vector2Input.y < 0)
                {
                    m_platformDrop.Execute();

                    return;
                }

                if (m_skills.IsModuleActive(PlayerBehaviour.Jump))
                {
                    if (m_crouch?.IsThereNoCeiling() ?? true)
                    {
                        if (m_state.isDashing)
                        {
                            m_activeDash?.Cancel();
                        }
                        m_activeSlide?.Cancel();
                        m_groundedness?.ChangeValue(false);
                        m_groundJump?.Execute();
                        m_groundJump?.HandleCutoffTimer();
                        m_movement?.SwitchConfigTo(Movement.Type.MidAir);
                    }
                }
            }
            else
            {
                if (m_state.isInShadowMode == false)
                {
                    if (m_skills.IsModuleActive(PrimarySkill.DoubleJump))
                    {
                        if (m_extraJump?.HasExtras() ?? false)
                        {
                            if (m_state.isLevitating)
                            {
                                m_devilWings?.Cancel();
                            }
                            
                            m_extraJump?.Execute();
                        }
                    }
                }

                //wallJumpAway
                if (m_state.isStickingToWall)
                {
                    if (m_skills.IsModuleActive(PrimarySkill.WallMovement))
                    {
                        if (m_state.canWallCrawl)
                        {
                            m_wallMovement?.Cancel();
                        }
                        m_wallStick?.Cancel();
                        m_wallMovement?.Cancel();
                        FlipCharacter();
                        m_wallJump?.JumpAway();
                    }
                }
            }
        }

        private void OnJumpPerformedInput()
        {

        }

        private void OnLevitateStartedInput()
        {
            if (m_skills.IsModuleActive(PrimarySkill.DevilWings))
            {
                if (m_state.isInShadowMode == false)
                {
                    if (m_state.isGrounded == false && m_devilWings.CanLevitate() && m_state.isLevitating == false)
                    {
                        if (m_state.isHighJumping)
                        {
                            m_groundJump?.CutOffJump();
                        }

                        m_devilWings.EnableLevitate();
                        m_devilWings?.Execute();
                    }
                }
            }
        }

        private void OnLevitateInput()
        {
            if (m_state.isLevitating)
            {
                m_devilWings?.MaintainHeight();
                m_devilWings?.GiveMovementBoost();
                m_devilWings?.ConsumeSource();
                if (m_devilWings?.HaveEnoughSourceForMaintainingHeight() ?? true)
                {
                    m_devilWings?.Cancel();
                }
            }
            else
            {
                if (m_devilWings?.HaveEnoughSourceForMaintainingHeight() ?? true)
                {
                    m_devilWings.EnableLevitate();
                }
            }
        }


        private void OnLevitateCancelledInput()
        {
            if (m_state.isLevitating)
            {
                m_devilWings.Cancel();
            }
        }

        private void OnDashStartedInput()
        {
            if (m_state.isInShadowMode == false)
            {
                m_idle?.Cancel();
                m_movement?.Cancel();
                m_whipCombo?.Cancel();
                m_whipCombo?.Reset();
                m_earthShaker?.Cancel();
                m_objectManipulation?.Cancel();

                if (m_state.isGrounded)
                {
                    if ((m_skills.IsModuleActive(PrimarySkill.Slide) || m_skills.IsModuleActive(PrimarySkill.ShadowSlide)) && m_state.canSlide)
                    {
                        if (m_vector2Input.y < 0 && m_state.canSlide)
                        {
                            ExecuteSlide();
                        }
                    }

                    if ((m_skills.IsModuleActive(PrimarySkill.Dash) || m_skills.IsModuleActive(PrimarySkill.ShadowDash)) && m_state.canDash)
                    {
                        ExecuteDash();
                    }
                }
                else
                {
                    if ((m_skills.IsModuleActive(PrimarySkill.Dash) || m_skills.IsModuleActive(PrimarySkill.ShadowDash)) && m_state.canDash)
                    {
                        if (m_state.isStickingToWall)
                        {
                            m_wallStick?.Cancel();
                            FlipCharacter();
                        }

                        if (m_state.isLevitating)
                        {
                            m_devilWings?.Cancel();
                        }

                        ExecuteDash();
                    }
                }
            }
        }

        private void OnInteractInput()
        {
            if (m_state.isGrounded)
            {
                m_objectInteraction?.Interact();
                return;
            }
        }

        private void OnShadowMorphStartedInput()
        {
            if (m_skills.IsModuleActive(PrimarySkill.ShadowMorph))
            {
                m_idle?.Cancel();
                m_movement?.Cancel();
                m_objectManipulation?.Cancel();

                if (m_state.isInShadowMode)
                {
                    m_shadowMorph.Cancel();
                    m_shadowGaugeRegen?.Enable(true);
                }
                else
                {
                    m_shadowGaugeRegen?.Enable(false);
                    m_shadowMorph.Execute();
                }
            }
        }

        private void OnStoreInput()
        {
            Debug.Log("Open Necronomicon");
        }

        private void OnPauseInput()
        {
            Debug.Log("Open Pause Menu");
        }
        #endregion

        #region Action Functions
        private void WallStickMovementAction()
        {
            if (m_state.isStickingToWall)
            {
                if (m_state.canWallCrawl == true)
                {
                    m_wallSlide?.Cancel();
                    m_wallMovement?.Move(m_vector2Input.y);

                    m_groundedness?.Evaluate();

                    if ((m_wallMovement?.IsThereAWall(WallMovement.SensorType.Body) ?? false) == false ||
                        (m_wallMovement?.IsThereAWall(WallMovement.SensorType.Overhead) ?? false) == false)
                    {
                        m_wallMovement?.Cancel();
                    }

                    if (m_state.isGrounded)
                    {
                        return;
                    }
                }
                else
                {
                    m_wallMovement?.Cancel();

                    if (m_vector2Input.x != 0 && Mathf.Sign(m_vector2Input.x) == (float)m_character.facing)
                    {
                        m_wallSlide?.Cancel();
                    }
                    else
                    {
                        if (m_wallSlide.IsThereAWall())
                        {
                            m_wallSlide?.Execute();
                            m_groundedness?.Evaluate();
                            if (m_state.isGrounded)
                                return;
                        }
                        else
                        {
                            m_wallSlide?.Cancel();
                            m_wallStick?.Cancel();
                        }
                    }
                }
                return;
            }
        }

        private void LedgeGrabMovementAction()
        {
            if(m_state.isGrounded == false)
            {
                if(m_vector2Input.x != 0)
                {
                    if (m_ledgeGrab?.IsDoable() ?? false)
                    {
                        m_wallMovement?.Cancel();
                        m_wallStick?.Cancel();
                        m_ledgeGrab?.Execute();
                    }
                }
            }
        }

        private void MoveAction()
        {
            if (m_state.isDashing)
            {
                return;
            }

            if (m_vector2Input.x == 0)
            {
                m_movement.Cancel();
                m_idle?.Execute(m_state.allowExtendedIdle);
                return;
            }

            MoveCharacter(m_state.isGrabbing, m_vector2Input.x);
        }
        #endregion

        #region Utility
        private void HandleGroundBehaviour()
        {
            if (m_state.isDashing == false && m_state.canDash == false)
            {
                m_dash?.HandleCooldown();
            }

            if (m_state.isSliding == false && m_state.canSlide == false)
            {
                m_slide?.HandleCooldown();
            }

            if (m_state.isAttacking)
            {
                if (m_state.isChargingAttack)
                {
                    m_chargeAttackHandle?.Execute();
                }
                else if (m_state.isAimingProjectile)
                {
                    if (m_projectileThrow?.HasReachedVerticalThreshold() == true)
                    {
                        GameplaySystem.cinema.ApplyCameraPeekMode(Cinematics.CameraPeekMode.Up);
                    }
                    else
                    {
                        GameplaySystem.cinema.ApplyCameraPeekMode(Cinematics.CameraPeekMode.None);
                    }
                }
                else if (m_state.isDoingSwordThrust)
                {
                    HandleSwordThrust();
                    return;
                }
                else
                {
                    m_attackRegistrator?.ResetHitCache();
                }
            }
            else if (m_state.isDashing)
            {
                HandleDash();
            }
            else if (m_state.isSliding)
            {
                HandleSlide(m_vector2Input.x);
            }
        }

        private void HandleAirBehaviour()
        {
            if (m_state.isAttacking)
            {

            }
            else if (m_state.isDashing)
            {
                HandleDash();
            }
            else if (m_state.isSliding)
            {
                HandleSlide(m_vector2Input.x);
            }
            else if (m_state.isLevitating)
            {
                m_devilWings?.MaintainHeight();
                m_devilWings?.GiveMovementBoost();
                m_devilWings?.ConsumeSource();
                if (m_devilWings?.HaveEnoughSourceForMaintainingHeight() ?? true)
                {
                    m_devilWings?.Cancel();
                }
            }
            else if (m_state.isStickingToWall)
            {
                if (m_wallSlide.IsThereAWall())
                {
                    m_groundedness?.Evaluate();
                    if (m_state.isGrounded)
                        return;
                }
                else
                {
                    m_wallSlide?.Cancel();
                    m_wallStick?.Cancel();
                }
            }
        }

        private void OnGroundednessStateChange(object sender, EventActionArgs eventArgs)
        {
            if (m_state.isDead)
            {
                //Then you need to git gud.
            }
            else
            {
                #region Groundedness Switch
                m_dash.Reset();
                m_slide.Reset();
                m_objectManipulation?.Cancel();
                if (m_state.isGrounded)
                {
                    m_physicsMat.SetPhysicsTo(PlayerPhysicsMatHandle.Type.Ground);

                    if (m_state.isStickingToWall)
                    {
                        m_wallMovement?.Cancel();
                        m_wallStick?.Cancel();
                    }
                    else if (m_state.isLevitating)
                    {
                        m_devilWings?.Cancel();
                    }

                    m_initialDescentBoost?.Reset();
                    m_extraJump?.Reset();
                    m_movement?.SwitchConfigTo(Movement.Type.Jog);

                    if (m_state.isAttacking)
                    {
                        m_basicSlashes.Cancel();
                        m_slashCombo.Cancel();
                        m_whip.Cancel();
                        m_whipCombo.Cancel();
                    }
                }
                else
                {
                    m_physicsMat.SetPhysicsTo(PlayerPhysicsMatHandle.Type.Midair);
                    m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, m_rigidbody.velocity.y);
                    if (m_state.isCrouched)
                    {
                        m_crouch?.Cancel();
                    }
                    else if (m_state.isAimingProjectile)
                    {
                        m_projectileThrow.EndAim();
                        m_projectileThrow.Cancel();
                    }
                    m_idle?.Cancel();
                    m_movement?.SwitchConfigTo(Movement.Type.MidAir);
                }
                #endregion
            }
        }

        private void OnDeath(object sender, EventActionArgs eventArgs)
        {
            Disable();
            m_idle?.Cancel();
            m_shadowMorph?.Cancel();
            m_teleportingSkull?.DisableTeleport();
        }

        private void OnFlinch(object sender, EventActionArgs eventArgs)
        {
            if (m_teleportingSkull.canTeleport)
            {
                m_teleportingSkull?.Cancel();
                m_teleportingSkull.TeleportToProjectile();
            }
            else
            {
                m_combatReadiness?.Execution();
                if (m_state.isGrounded)
                {
                    if (m_state.isAttacking)
                    {
                        if (m_state.isChargingAttack)
                        {
                            m_swordThrust?.Cancel();
                        }
                        else
                        {
                            m_swordThrust?.Cancel();
                            m_basicSlashes?.Cancel();
                            m_whip?.Cancel();
                            m_slashCombo?.Cancel();
                            m_slashCombo?.Reset();
                            m_whipCombo?.Cancel();
                            m_whipCombo?.Reset();
                            m_reaperHarvest?.Cancel();
                            m_sovereignImpale?.Cancel();
                            m_hellTrident?.Cancel();
                            m_foolsVerdict?.Cancel();
                            m_backDiver?.Cancel();
                            m_barrier?.Cancel();
                            m_championsUprising?.Cancel();
                            m_icarusWings?.Cancel();
                        }
                    }

                    if (m_state.isCrouched)
                    {
                        m_idle?.Cancel();
                        m_crouch.Cancel();
                    }
                    else if (m_state.isDashing)
                    {
                        m_dash.Cancel();
                    }
                    else if (m_state.isSliding)
                    {
                        m_slide.Cancel();
                    }
                    else if (m_state.isGrabbing)
                    {
                        m_objectManipulation.Cancel();
                    }
                    else if (m_state.isInShadowMode)
                    {
                        m_shadowMorph?.Cancel();
                    }
                    else
                    {
                        m_shadowGaugeRegen?.Enable(true);
                        m_idle?.Cancel();
                        m_movement?.Cancel();
                        m_block?.Cancel();
                        m_shadowSlide.Cancel();
                    }

                    GameplaySystem.cinema.ApplyCameraPeekMode(Cinematics.CameraPeekMode.None);
                }
                else
                {
                    if (m_state.isAttacking)
                    {
                        m_basicSlashes?.Cancel();
                        m_earthShaker?.Cancel();
                        m_whip?.Cancel();
                        if (m_projectileThrow.willResetProjectile)
                            m_projectileThrow.ResetProjectile();
                        m_projectileThrow?.Cancel();
                    }

                    if (m_state.isStickingToWall)
                    {
                        m_wallMovement?.Cancel();
                        m_wallSlide?.Cancel();
                        m_wallStick?.Cancel();
                    }
                    else if (m_state.isDashing)
                    {
                        m_activeDash?.Cancel();
                    }
                    else if (m_state.isLevitating)
                    {
                        m_devilWings?.Cancel();
                    }
                    else if (m_state.isInShadowMode)
                    {
                        m_shadowMorph?.Cancel();
                    }

                    m_devilWings?.Cancel();
                    m_krakenRage?.Cancel();
                    m_soulFireBlast?.Cancel();
                    m_edgedFury?.Cancel();
                    m_reaperHarvest?.Cancel();
                    m_diagonalSwordDash?.Cancel();
                    m_lightningSpear?.Cancel();
                    m_airSlashRange?.Cancel();
                }
            }
        }

        private void OnProjectileThrowRequest(object sender, EventActionArgs eventArgs)
        {
            //m_input.projectileThrowPressed = true;
        }

        private void FlipCharacter()
        {
            var oppositeFacing = m_character.facing == HorizontalDirection.Right ? HorizontalDirection.Left : HorizontalDirection.Right;
            m_character.SetFacing(oppositeFacing);
            m_slashCombo.Cancel();
            m_slashCombo.Reset();
            m_whipCombo.Cancel();
            m_whipCombo.Reset();
        }

        private void ResetProjectile(object sender, EventActionArgs eventArgs)
        {
            if (m_projectileThrow.willResetProjectile)
                m_projectileThrow.ResetProjectile();
        }

        private void HasTeleported(object sender, EventActionArgs eventArgs)
        {
            m_flinch?.CancelFlinch();
            m_idle?.Execute(false);
            m_movement?.Cancel();
            m_crouch?.Cancel();
            m_dash?.Cancel();
            m_shadowSlide?.Cancel();
            m_slide?.Cancel();
            m_wallStick?.Cancel();
            m_devilWings?.Cancel();
            m_shadowDash?.Cancel();
            m_basicSlashes?.Cancel();
            m_slashCombo?.Cancel();
            m_swordThrust?.Cancel();
            m_earthShaker?.Cancel();
            m_whip?.Cancel();
            m_whipCombo?.Cancel();
            m_projectileThrow?.Cancel();
            if (m_projectileThrow.willResetProjectile)
                m_projectileThrow.ResetProjectile();
            m_shadowMorph.Cancel();
            m_block?.Cancel();
            m_shadowGaugeRegen.Enable(true);
            m_reaperHarvest?.Cancel();
            m_krakenRage?.Cancel();
            m_sovereignImpale?.Cancel();
            m_hellTrident?.Cancel();
            m_foolsVerdict?.Cancel();
            m_soulFireBlast?.Cancel();
            m_edgedFury?.Cancel();
            m_backDiver?.Cancel();
            m_barrier?.Cancel();
            m_diagonalSwordDash?.Cancel();
            m_championsUprising?.Cancel();
            m_lightningSpear?.Cancel();
            m_icarusWings?.Cancel();
            m_airSlashRange?.Cancel();
        }

        public void Enable()
        {
            m_updateEnabled = true;
            ControllerEnabled?.Invoke(this, EventActionArgs.Empty);
        }

        public void Disable()
        {
            m_updateEnabled = false;
            m_idle?.Execute(false);
            m_movement?.Cancel();
            m_crouch?.Cancel();
            m_dash?.Cancel();
            m_slide?.Cancel();
            m_wallStick?.Cancel();
            m_devilWings?.Cancel();
            m_shadowDash?.Cancel();
            m_basicSlashes?.Cancel();
            m_slashCombo?.Cancel();
            m_swordThrust?.Cancel();
            m_earthShaker?.Cancel();
            m_whip?.Cancel();
            m_whipCombo?.Cancel();
            m_projectileThrow?.Cancel();
            m_shadowMorph.Cancel();
            m_block?.Cancel();
            m_shadowGaugeRegen.Enable(true);
            m_reaperHarvest?.Cancel();
            m_krakenRage?.Cancel();
            m_sovereignImpale?.Cancel();
            m_hellTrident?.Cancel();
            m_foolsVerdict?.Cancel();
            m_soulFireBlast?.Cancel();
            m_edgedFury?.Cancel();
            m_backDiver?.Cancel();
            m_barrier?.Cancel();
            m_diagonalSwordDash?.Cancel();
            m_championsUprising?.Cancel();
            m_lightningSpear?.Cancel();
            m_icarusWings?.Cancel();
            m_airSlashRange?.Cancel();
            m_teleportingSkull?.Cancel();

            if (m_state.isGrounded)
            {
                m_movement?.SwitchConfigTo(Movement.Type.Jog);
            }
            ControllerDisabled?.Invoke(this, EventActionArgs.Empty);
        }
        #endregion

        #region Module Handling
        private void HandleSwordThrust()
        {
            m_swordThrust?.HandleDurationTimer();
            if (m_swordThrust?.IsSwordThrustDurationOver() ?? true)
            {
                m_swordThrust?.EndSwordThrust();
                m_swordThrust?.ResetCooldownTimer();
                m_swordThrust?.ResetDurationTimer();
            }
            else
            {
                m_swordThrust?.Execute();
            }
        }

        private void HandleDash()
        {
            m_activeDash?.HandleDurationTimer();
            if (m_activeDash?.IsDashDurationOver() ?? true)
            {
                m_activeDash?.Cancel();
                m_activeDash?.ResetCooldownTimer();
            }
            else
            {
                if(m_vector2Input.x !=  0)
                {
                    var signInput = Mathf.Sign(m_vector2Input.x);
                    if (signInput != (float)m_character.facing)
                    {
                        FlipCharacter();
                    }
                }
                m_activeDash?.Execute();
            }
        }

        private void HandleSlide(float horizontalInput)
        {
            m_activeSlide?.HandleDurationTimer();

            if (m_state.isGrounded)
            {
                if (m_activeSlide?.IsSlideDurationOver() ?? true)
                {
                    if (m_crouch.IsThereNoCeiling() || !m_slide.HasGroundToSlideOn() || !m_shadowSlide.HasGroundToSlideOn())
                    {
                        m_activeSlide?.Cancel();
                        m_activeSlide?.ResetCooldownTimer();
                    }
                    else
                    {
                        if (m_crouch.IsCrouchingPossible() || !m_slide.HasGroundToSlideOn() || !m_shadowSlide.HasGroundToSlideOn())
                        {
                            m_activeSlide?.Cancel();
                            m_activeSlide?.ResetCooldownTimer();

                            if (m_state.isCrouched == false)
                            {
                                m_crouch?.Execute();
                                m_idle?.Cancel();
                                m_movement?.SwitchConfigTo(Movement.Type.Crouch);
                            }
                        }
                    }
                }
                else
                {
                    if (horizontalInput != 0)
                    {
                        var signInput = Mathf.Sign(horizontalInput);
                        if (signInput != (float)m_character.facing)
                        {
                            FlipCharacter();
                        }
                    }
                    m_activeSlide?.Execute();
                }
            }
            else
            {
                m_activeSlide?.Cancel();
                m_activeSlide?.ResetCooldownTimer();
            }
        }

        private void ExecuteDash()
        {
            if (m_skills.IsModuleActive(PrimarySkill.ShadowDash))
            {
                if (m_shadowDash?.HaveEnoughSourceForExecution() ?? false)
                {
                    m_activeDash = m_shadowDash;
                    m_shadowDash.ConsumeSource();
                }
                else if (m_skills.IsModuleActive(PrimarySkill.Dash))
                {
                    m_activeDash = m_dash;
                }
            }
            else if (m_skills.IsModuleActive(PrimarySkill.Dash))
            {
                m_activeDash = m_dash;
            }

            m_activeDash?.ResetDurationTimer();
            m_activeDash?.Execute();
        }

        private void ExecuteSlide()
        {
            if (m_skills.IsModuleActive(PrimarySkill.ShadowSlide))
            {
                if (m_shadowSlide?.HaveEnoughSourceForExecution() ?? false)
                {
                    m_activeSlide = m_shadowSlide;
                    m_shadowSlide.ConsumeSource();

                    m_activeSlide?.ResetDurationTimer();
                    m_activeSlide?.Execute();
                }
            }
        }
        private void MoveCharacter(bool isGrabbing, float horizontalInput)
        {
            if (!IsFacingInput(horizontalInput))
            {
                m_basicSlashes.Cancel();
                m_slashCombo.Cancel();
                m_whip.Cancel();
                m_whipCombo.Cancel();
                m_whipCombo.Reset();
            }

            if (isGrabbing == false)
            {
                if (horizontalInput == 0)
                {
                    m_idle?.Execute(m_state.allowExtendedIdle);
                }
                else
                {
                    m_idle?.Cancel();
                }
                if (m_state.isGrounded)
                    m_movement?.GroundMove(horizontalInput, true);
                else
                    m_movement?.AirMove(horizontalInput, true);
            }
            else
            {
                if (m_state.isGrounded)
                    m_movement?.GroundMove(horizontalInput, false);
                else
                    m_movement?.AirMove(horizontalInput, false);
            }
        }
        private bool CanMove()
        {
            return m_whipCombo.CanMove()
                    && m_slashCombo.CanMove()
                    && m_whip.CanMove()
                    && m_reaperHarvest.CanMove()
                    && m_sovereignImpale.CanMove()
                    && m_hellTrident.CanMove()
                    && m_foolsVerdict.CanMove()
                    && m_barrier.CanMove()
                    && m_activeDash.IsDashDurationOver()
                    && m_activeSlide.IsSlideDurationOver();

        }

        private bool IsFacingInput(float horizontalInput)
        {
            return horizontalInput > 0 && m_character.facing == HorizontalDirection.Right
                || horizontalInput < 0 && m_character.facing == HorizontalDirection.Left
                || horizontalInput == 0;
        }

        private void PrepareForGroundAttack()
        {
            m_combatReadiness?.Execution();
            m_idle?.Cancel();
            m_movement?.Cancel();
            m_objectManipulation?.Cancel();
            m_attackRegistrator?.ResetHitCache();
            m_projectileThrow.EndAim(); //fix for projectile throw delay WIP
            m_projectileThrow?.Cancel(); //fix for projectile throw delay WIP
        }

        private void PrepareForMidairAttack()
        {
            if (m_state.isLevitating)
            {
                m_devilWings?.Cancel();
            }

            if (m_state.isHighJumping == true)
            {
                m_groundJump?.CutOffJump();
            }

            m_combatReadiness?.Execution();
            m_attackRegistrator?.ResetHitCache();
        }
        #endregion
    }
}


