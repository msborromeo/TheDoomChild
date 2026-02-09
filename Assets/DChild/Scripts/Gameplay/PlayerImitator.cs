using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.Module;
using DChild.Menu;
using Holysoft.Event;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DChild.Gameplay.Environment
{
    public class PlayerImitator : MonoBehaviour
    {
        private class AnimationParameterInfo
        {
            //I'd like to thank Google Gemini for making it much faster to deal with all these variables
            //Note: anytime a new animation parameter is added to player, these variables need to be updated as well as the functions below
            //Basic actions
            private bool m_isIdle;
            private bool m_idleState;
            private bool isGrounded;
            private bool m_isInCombatMode;
            private bool m_isAttacking;
            private int m_slashState;
            public int slashState => m_slashState;
            private bool m_isDashing;
            private bool m_isJumping;
            private float m_speedX;
            private float m_speedY;
            private float m_YInput;
            private bool m_isCrouching;
            private bool m_isDead;
            private bool m_wallStick;
            private bool m_flinch;
            private bool m_EarthShake;
            private bool m_SwordThrust;
            private bool m_whipAttack;
            private bool m_isLevitating;
            private bool m_isGrabbing;
            private bool m_isPulling;
            private bool m_isPushing;
            private bool m_isInShadowMode;
            private bool m_isSliding;
            private bool m_projectileThrow;
            private bool m_projectileThrowVariance;
            private bool m_ledgeGrab;
            private int m_flinchState;
            private bool m_isBlocking;
            private bool m_isCharging;
            private bool m_stepClimb;
            private bool m_aimingProjectile;
            private bool m_isWallCrawling;
            private bool m_isDoubleJumping;
            private bool m_isWallJumping;
            private int m_whipState;
            public int whipState => m_whipState;
            private int m_Xinput;

            //Combat arts
            private bool m_reaperHarvest;
            private bool m_krakenRage;
            private bool m_airSlashCombo;
            private int m_airSlashState;
            private bool m_sovereignsImpale;
            private bool m_hellTrident;
            private bool m_foolsVerdict;
            private bool m_soulFireBlast;
            private bool m_edgedFury;
            private bool m_backDiver;
            private bool m_barrier;
            private bool m_diagonalSwordDash;
            private bool m_championsUprising;
            private bool m_lightningSpear;
            private bool m_icarusWings;
            private bool m_airSlashRanged;
            private bool m_teleportingSkull;

            public AnimationParameterInfo(Animator animator, AnimationParametersData animationParametersData)
            {
                //Basic actions
                m_isIdle = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsIdle));
                m_idleState = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IdleState));
                isGrounded = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsGrounded));
                m_isInCombatMode = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.CombatMode));
                m_isAttacking = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsAttacking));
                m_slashState = animator.GetInteger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SlashState));
                m_isDashing = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsDashing));
                m_isJumping = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.Jump));
                m_speedX = animator.GetFloat(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SpeedX));
                m_speedY = animator.GetFloat(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SpeedY));
                m_YInput = animator.GetFloat(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.YInput));
                m_isCrouching = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsCrouched));
                m_isDead = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsDead));
                m_wallStick = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.WallStick));
                m_flinch = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.Flinch));
                m_EarthShake = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.EarthShaker));
                m_SwordThrust = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SwordTrust));
                m_whipAttack = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.WhipAttack));
                m_isLevitating = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsLevitating));
                m_isGrabbing = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsGrabbing));
                m_isPulling = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsPulling));
                m_isPushing = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsPushing));
                m_isInShadowMode = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ShadowMode));
                m_isSliding = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsSliding));
                m_projectileThrow = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ProjectileThrow));
                m_projectileThrowVariance = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ProjectileThrowVariant));
                m_ledgeGrab = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.LedgeGrab));
                m_flinchState = animator.GetInteger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.FlinchState));
                m_isBlocking = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsBlocking));
                m_isCharging = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsCharging));
                m_stepClimb = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.StepClimb));
                m_aimingProjectile = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AimingProjectile));
                m_isWallCrawling = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsWallCrawling));
                m_isDoubleJumping = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.DoubleJump));
                m_isWallJumping = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.WallJump));
                m_whipState = animator.GetInteger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.WhipState));
                m_Xinput = animator.GetInteger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.XInput));

                //Combat arts
                m_reaperHarvest = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ReaperHarvest));
                m_krakenRage = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.KrakenRage));
                m_airSlashCombo = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AirSlashCombo));
                m_airSlashState = animator.GetInteger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AirSlashState));
                m_sovereignsImpale = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SovereignImpale));
                m_hellTrident = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.HellTrident));
                m_foolsVerdict = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.FoolsVerdict));
                m_soulFireBlast = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SoulFireBlast));
                m_edgedFury = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.EdgedFury));
                m_backDiver = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.BackDiver));
                m_barrier = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.Barrier));
                m_diagonalSwordDash = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.DiagonalSwordDash));
                m_championsUprising = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ChampionsUprising));
                m_lightningSpear = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.LightningSpear));
                m_icarusWings = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IcarusWings));
                m_airSlashRanged = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AirSlashRange));
                m_teleportingSkull = animator.GetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.TeleportingSkull));
            }

            public void Apply(Animator animator, AnimationParametersData animationParametersData)
            {
                // Basic actions
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsIdle), m_isIdle);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IdleState), m_idleState);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsGrounded), isGrounded);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.CombatMode), m_isInCombatMode);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsAttacking), m_isAttacking);
                animator.SetInteger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SlashState), m_slashState);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsDashing), m_isDashing);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.Jump), m_isJumping);
                animator.SetFloat(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SpeedX), m_speedX);
                animator.SetFloat(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SpeedY), m_speedY);
                animator.SetFloat(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.YInput), m_YInput);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsCrouched), m_isCrouching);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsDead), m_isDead);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.WallStick), m_wallStick);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.Flinch), m_flinch);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.EarthShaker), m_EarthShake);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SwordTrust), m_SwordThrust);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.WhipAttack), m_whipAttack);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsLevitating), m_isLevitating);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsGrabbing), m_isGrabbing);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsPulling), m_isPulling);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsPushing), m_isPushing);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ShadowMode), m_isInShadowMode);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsSliding), m_isSliding);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ProjectileThrow), m_projectileThrow);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ProjectileThrowVariant), m_projectileThrowVariance);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.LedgeGrab), m_ledgeGrab);
                animator.SetInteger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.FlinchState), m_flinchState);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsBlocking), m_isBlocking);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsCharging), m_isCharging);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.StepClimb), m_stepClimb);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AimingProjectile), m_aimingProjectile);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsWallCrawling), m_isWallCrawling);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.DoubleJump), m_isDoubleJumping);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.WallJump), m_isWallJumping);
                animator.SetInteger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.WhipState), m_whipState);
                animator.SetInteger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.XInput), m_Xinput);

                // Combat arts
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ReaperHarvest), m_reaperHarvest);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.KrakenRage), m_krakenRage);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AirSlashCombo), m_airSlashCombo);
                animator.SetInteger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AirSlashState), m_airSlashState);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SovereignImpale), m_sovereignsImpale);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.HellTrident), m_hellTrident);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.FoolsVerdict), m_foolsVerdict);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SoulFireBlast), m_soulFireBlast);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.EdgedFury), m_edgedFury);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.BackDiver), m_backDiver);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.Barrier), m_barrier);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.DiagonalSwordDash), m_diagonalSwordDash);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ChampionsUprising), m_championsUprising);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.LightningSpear), m_lightningSpear);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IcarusWings), m_icarusWings);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AirSlashRange), m_airSlashRanged);
                animator.SetBool(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.TeleportingSkull), m_teleportingSkull);

                // Combat Arts (from your C# variables and Animator Triggers)
                if (m_reaperHarvest == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ReaperHarvest));
                }

                if (m_krakenRage == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.KrakenRage));
                }

                // Note: You have m_airSlashCombo (bool) and AirSlashCombo (Trigger)
                if (m_airSlashCombo == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AirSlashCombo));
                }

                if (m_sovereignsImpale == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SovereignImpale)); // Assuming your enum label matches "SovereignImpale"
                }

                if (m_hellTrident == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.HellTrident));
                }

                if (m_foolsVerdict == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.FoolsVerdict));
                }

                if (m_soulFireBlast == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SoulFireBlast));
                }

                if (m_edgedFury == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.EdgedFury));
                }

                if (m_backDiver == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.BackDiver));
                }

                if (m_barrier == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.Barrier));
                }

                if (m_diagonalSwordDash == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.DiagonalSwordDash));
                }

                if (m_championsUprising == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ChampionsUprising));
                }

                if (m_lightningSpear == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.LightningSpear));
                }

                if (m_icarusWings == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IcarusWings));
                }

                if (m_airSlashRanged == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AirSlashRange)); // Assuming your enum label matches "AirSlashRange"
                }

                if (m_teleportingSkull == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.TeleportingSkull));
                }

                // Basic Actions (from your C# variables and Animator Triggers)
                // Note: You explicitly provided this one, including it for completeness
                if (m_ledgeGrab == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.LedgeGrab));
                }

                if (m_stepClimb == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.StepClimb));
                }

                // Note: You have m_SwordThrust (bool) and SwordThrust (Trigger)
                if (m_SwordThrust == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SwordTrust));
                }

                // Note: You have m_aimingProjectile (bool) and AimingProjectile (Trigger)
                if (m_aimingProjectile == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.AimingProjectile));
                }

                // Note: You have m_projectileThrow (bool) and ProjectileThrow (Trigger)
                if (m_projectileThrow == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ProjectileThrow));
                }

                // Note: You have m_projectileThrowVariance (bool) and ProjectileThrowVariance (Trigger)
                if (m_projectileThrowVariance == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.ProjectileThrowVariant));
                }

                // Note: You have m_isBlocking (bool) and IsBlocking (Trigger)
                if (m_isBlocking == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsBlocking));
                }

                // Note: You have m_isCharging (bool) and IsCharging (Trigger)
                if (m_isCharging == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsCharging));
                }

                // Note: You have m_isWallCrawling (bool) and IsWallCrawling (Trigger)
                if (m_isWallCrawling == true)
                {
                    animator.SetTrigger(animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.IsWallCrawling));
                }
            }
        }

        public class FXAnimationParameterInfo
        {
            private bool m_groundOverhead;
            private bool m_crouch;
            private bool m_jumpOverhead;
            private bool m_jump;
            private bool m_slashCombo1;
            private bool m_slashCombo2;
            private bool m_slashCombo3;
            AnimatorStateInfo m_currentClipInfo;

            public FXAnimationParameterInfo(Animator animator)
            {
                //Bleeegh hard coded values >.<
                m_groundOverhead = animator.GetBool("GroundOverhead");
                m_crouch = animator.GetBool("Crouch");
                m_jumpOverhead = animator.GetBool("JumpOverhead");
                m_jump = animator.GetBool("Jump");

                m_currentClipInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (m_currentClipInfo.IsName("Slash Combo 1"))
                {
                    m_slashCombo1 = true;
                }
                if (m_currentClipInfo.IsName("Slash Combo 2"))
                {
                    m_slashCombo2 = true;
                }
                if (m_currentClipInfo.IsName("Slash Combo 3"))
                {
                    m_slashCombo3 = true;
                }
            }

            public void Apply(Animator animator)
            {
                //Bleeegh hard coded values >.<
                animator.SetBool("GroundOverhead", m_groundOverhead);
                animator.SetBool("Crouch", m_crouch);
                animator.SetBool("JumpOverhead", m_jumpOverhead);
                animator.SetBool("Jump", m_jump);

                if (m_slashCombo1 == true)
                {
                    animator.Play("SlashCombo1");
                }
                if (m_slashCombo2 == true)
                {
                    animator.Play("SlashCombo2");
                }
                if (m_slashCombo3 == true)
                {
                    animator.Play("SlashCombo3");
                }
            }
        }

        [SerializeField]
        private AnimationParametersData m_animationParametersData;
        [SerializeField]
        private LineRenderer m_lineConnection;
        [SerializeField]
        private float m_lineYOffset;

        [SerializeField]
        private Transform m_toImitate;
        [SerializeField]
        private float m_imitateDelay;

        private Animator m_animator;
        private Animator m_animatorToImitate;
        [SerializeField]
        private Animator m_attackFXAnimator;

        private float m_imitationDelayTimer;
        private bool m_isDelayed;
        private List<Vector3> m_positionToImitate;
        private List<Vector3> m_scaleToImitate;
        private List<AnimationParameterInfo> m_animationToImitate;

        [SerializeField]
        private PhantomSlashCombo m_phantomSlashCombo;
        [SerializeField]
        private PhantomWhipCombo m_phantomWhipCombo;

        public void StartImitating(Player toImitate)
        {
            m_toImitate = toImitate.character.transform;
            m_animator = GetComponent<Animator>();
            m_animatorToImitate = toImitate.character.centerMass.GetComponentInParent<Animator>();
            ResetImitation();
            m_lineConnection.enabled = true;
        }

        public void ResetImitation()
        {
            transform.position = m_toImitate.position;
            m_imitationDelayTimer = m_imitateDelay;
            m_lineConnection.enabled = false;
            m_isDelayed = true;

            InitializeRecords();
        }

        private void InitializeRecords()
        {
            if (m_positionToImitate == null)
            {
                m_positionToImitate = new List<Vector3>();
                m_scaleToImitate = new List<Vector3>();
                m_animationToImitate = new List<AnimationParameterInfo>();
            }
            m_positionToImitate.Clear();
            m_scaleToImitate.Clear();
            m_animationToImitate.Clear();
        }

        private void UpdateImitation()
        {
            transform.position = m_positionToImitate[0];
            transform.localScale = m_scaleToImitate[0];
            m_animationToImitate[0].Apply(m_animator, m_animationParametersData);

            m_positionToImitate.RemoveAt(0);
            m_scaleToImitate.RemoveAt(0);
            m_animationToImitate.RemoveAt(0);
        }

        private void RecordInfoToImitate()
        {
            m_positionToImitate.Add(m_toImitate.position);
            var scale = m_toImitate.localScale;
            scale.x *= -1;
            m_scaleToImitate.Add(scale);

            m_animationToImitate.Add(new AnimationParameterInfo(m_animatorToImitate, m_animationParametersData));
        }

        private void DrawLineConnection()
        {
            var yOffset = Vector3.up * m_lineYOffset;
            m_lineConnection.positionCount = m_positionToImitate.Count + 1;
            m_lineConnection.SetPosition(0, transform.position + yOffset);
            for (int i = 0; i < m_positionToImitate.Count; i++)
            {
                m_lineConnection.SetPosition(i + 1, m_positionToImitate[i] + yOffset);
            }
        }

        private void Start()
        {
            InitializeRecords();
            StartImitating(GameplaySystem.playerManager.player);
            var lineConnectionTransform = m_lineConnection.transform;
            lineConnectionTransform.SetParent(null);
            lineConnectionTransform.position = Vector3.zero;
            LoadingHandle.SceneDone += OnSceneDone;
            LoadingHandle.LoadingDone += OnSceneLoadDone;
        }

        private void OnDisable()
        {
            LoadingHandle.SceneDone -= OnSceneDone;
            LoadingHandle.LoadingDone -= OnSceneLoadDone;
        }

        private void OnSceneDone(object sender, EventActionArgs eventArgs)
        {
            ResetImitation();
        }

        private void OnSceneLoadDone(object sender, EventActionArgs eventArgs)
        {
            StartImitating(GameplaySystem.playerManager.player);
        }

        private void Update()
        {
            if (GameplaySystem.isGamePaused)
                return;

            RecordInfoToImitate();

            if (m_isDelayed)
            {
                m_imitationDelayTimer -= GameplaySystem.time.deltaTime;
                if (m_imitationDelayTimer <= 0)
                {
                    m_isDelayed = false;
                }
            }
            else
            {
                if (GameplaySystem.isGamePaused)
                {

                }
                else
                {
                    UpdateImitation();
                }
            }

            DrawLineConnection();
        }

        private void OnDestroy()
        {
            LoadingHandle.SceneDone -= OnSceneDone;
            LoadingHandle.LoadingDone -= OnSceneLoadDone;
            Destroy(m_lineConnection.gameObject);
        }
    }
}