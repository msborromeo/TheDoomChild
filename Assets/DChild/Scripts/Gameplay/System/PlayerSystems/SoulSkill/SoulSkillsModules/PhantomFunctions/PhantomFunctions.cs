using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.BattleAbilityModule;
using DChild.Gameplay.Characters.Players.Module;
using DChild.Gameplay.Characters.Players.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay
{
    public class PhantomFunctions : MonoBehaviour
    {
        private PhantomBasicSlashes m_basicSlashes;
        private PhantomSlashCombo m_slashCombo;
        private PhantomBasicWhip m_whip;
        private PhantomWhipCombo m_whipCombo;
        private PhantomEarthshaker m_earthShaker;
        private PhantomSwordThrust m_swordThrust;

        public void SwordJumpSlashForwardFX()
        {
            m_basicSlashes?.PlayFXFor(PhantomBasicSlashes.Type.MidAir_Forward, true);
        }
        public void SwordJumpSlashForwardEnableCollision()
        {
            m_basicSlashes?.EnableCollision(PhantomBasicSlashes.Type.MidAir_Forward, true);
        }

        public void JumpUpSlashFX()
        {
            m_basicSlashes?.PlayFXFor(PhantomBasicSlashes.Type.MidAir_Overhead, true);
        }

        public void SwordJumpUpSlashEnableCollision()
        {
            m_basicSlashes?.EnableCollision(PhantomBasicSlashes.Type.MidAir_Overhead, true);
        }

        public void SwordJumpUpSlashDisableCollision()
        {
            m_basicSlashes?.EnableCollision(PhantomBasicSlashes.Type.MidAir_Overhead, false);
        }

        public void SwordUpSlashFX()
        {
            m_basicSlashes?.PlayFXFor(PhantomBasicSlashes.Type.Ground_Overhead, true);
        }

        public void SwordUpSlashEnableCollision()
        {
            m_basicSlashes?.EnableCollision(PhantomBasicSlashes.Type.Ground_Overhead, true);
        }

        public void SwordUpSlashDisableCollision()
        {
            m_basicSlashes?.EnableCollision(PhantomBasicSlashes.Type.Ground_Overhead, false);
        }

        public void CrouchSlashFX()
        {
            m_basicSlashes?.PlayFXFor(PhantomBasicSlashes.Type.Crouch, true);
        }
        public void SwordCrouchSlashEnableCollision()
        {
            m_basicSlashes?.EnableCollision(PhantomBasicSlashes.Type.Crouch, true);
        }

        public void SlashCombo()
        {
            //m_slashCombo?.PlayFX(true);
            //m_slashCombo?.EnableCollision(true);
            //m_slashCombo?.IterateCurrentVisualState();
        }

        public void WhipCombo()
        {
            //m_whipCombo?.PlayFX(true);
            //m_whipCombo?.EnableCollision(true);
        }

        public void GroundForwardWhipAttackFX()
        {
            m_whip?.PlayFXFor(PhantomBasicWhip.Type.Ground_Forward, true);
            m_whip?.EnableCollision(PhantomBasicWhip.Type.Ground_Forward, true);
        }

        public void GroundOverheadWhipAttackFX()
        {
            m_whip?.PlayFXFor(PhantomBasicWhip.Type.Ground_Overhead, true);
            m_whip?.EnableCollision(PhantomBasicWhip.Type.Ground_Overhead, true);
        }

        public void MidairForwardWhipAttackFX()
        {
            m_whip?.PlayFXFor(PhantomBasicWhip.Type.MidAir_Forward, true);
            m_whip?.EnableCollision(PhantomBasicWhip.Type.MidAir_Forward, true);
        }

        public void MidairOverheadWhipAttackFX()
        {
            m_whip?.PlayFXFor(PhantomBasicWhip.Type.MidAir_Overhead, true);
            m_whip?.EnableCollision(PhantomBasicWhip.Type.MidAir_Overhead, true);
        }

        public void CrouchForwardWhipAttackFX()
        {
            m_whip?.PlayFXFor(PhantomBasicWhip.Type.Ground_Forward, true);
            m_whip?.EnableCollision(PhantomBasicWhip.Type.Crouch_Forward, true);
        }

        public void ContinueSlashCombo()
        {
            m_slashCombo?.PlayFX(true);
            m_slashCombo?.EnableCollision(true);
        }
        public void DisableBasicSlashCollisions()
        {
            m_basicSlashes?.EnableCollision(PhantomBasicSlashes.Type.Ground_Overhead, false);
            m_basicSlashes?.EnableCollision(PhantomBasicSlashes.Type.MidAir_Overhead, false);
            m_basicSlashes?.EnableCollision(PhantomBasicSlashes.Type.MidAir_Forward, false);
            m_basicSlashes?.EnableCollision(PhantomBasicSlashes.Type.Crouch, false);
        }

        public void DisableBasicWhipCollisions()
        {
            m_whip?.EnableCollision(PhantomBasicWhip.Type.Ground_Overhead, false);
            m_whip?.EnableCollision(PhantomBasicWhip.Type.Ground_Forward, false);
            m_whip?.EnableCollision(PhantomBasicWhip.Type.MidAir_Overhead, false);
            m_whip?.EnableCollision(PhantomBasicWhip.Type.MidAir_Forward, false);
            m_whip?.EnableCollision(PhantomBasicWhip.Type.Crouch_Forward, false);
        }

        public void DisableSlashComboCollisions()
        {
            m_slashCombo?.EnableCollision(false);
        }

        public void DisableWhipComboCollisions()
        {
            m_whipCombo?.EnableCollision(false);
        }

        public void IterateCurrentWhipComboState()
        {
            m_whipCombo?.IterateCurrentVisualState();
        }

        public void IterateCurrentSlashComboState()
        {
            m_slashCombo.IterateCurrentVisualState();
        }

        public void ResetSlashComboFX()
        {
            m_slashCombo.StopSlashCombo();
        }

        public void ResetWhipComboFX()
        {
            m_whipCombo.StopWhipCombo();
        }

        public void EarthShakerPreLoop()
        {
            m_earthShaker.HandlePreFall();
        }

        public void EarthShakerLoop()
        {
            m_earthShaker.HandleFall();
        }

        public void EarthShakerImpact()
        {
            m_earthShaker.Impact();
        }

        public void EarthShakerEnd()
        {
            m_earthShaker.EndExecution();
        }

        public void SwordThrustEnd()
        {
            m_swordThrust.EndSwordThrust();
            m_swordThrust.EndExecution();
        }

        public void SwordThrustChargeEnd()
        {
            m_swordThrust?.EndSwordThrustCharge();
        }

        public void SlashComboOn(int slashState)
        {
            m_slashCombo.PlaySlashCombo(slashState);
        }

        public void WhipComboOn(int whipState)
        {
            m_whipCombo.PlayWhipCombo(whipState);
        }

        public void Null() { }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            m_basicSlashes = GetComponentInChildren<PhantomBasicSlashes>();
            m_slashCombo = GetComponentInChildren<PhantomSlashCombo>();
            m_whip = GetComponentInChildren<PhantomBasicWhip>();
            m_whipCombo = GetComponentInChildren<PhantomWhipCombo>();
            m_earthShaker = GetComponentInChildren<PhantomEarthshaker>();
            m_swordThrust = GetComponentInChildren<PhantomSwordThrust>();
        }
    }
}