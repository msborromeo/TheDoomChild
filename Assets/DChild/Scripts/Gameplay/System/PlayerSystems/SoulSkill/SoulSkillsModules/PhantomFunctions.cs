using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.BattleAbilityModule;
using DChild.Gameplay.Characters.Players.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay
{
    public class PhantomFunctions : MonoBehaviour
    {
        private BasicSlashes m_basicSlashes;
        private SlashCombo m_slashCombo;
        private WhipAttack m_whip;
        private WhipAttackCombo m_whipCombo;

        public void SwordJumpSlashForwardFX()
        {
            m_basicSlashes?.PlayFXFor(BasicSlashes.Type.MidAir_Forward, true);
        }
        public void SwordJumpSlashForwardEnableCollision()
        {
            m_basicSlashes?.EnableCollision(BasicSlashes.Type.MidAir_Forward, true);
        }

        public void JumpUpSlashFX()
        {
            m_basicSlashes?.PlayFXFor(BasicSlashes.Type.MidAir_Overhead, true);
        }

        public void SwordJumpUpSlashEnableCollision()
        {
            m_basicSlashes?.EnableCollision(BasicSlashes.Type.MidAir_Overhead, true);
        }

        public void SwordJumpUpSlashDisableCollision()
        {
            m_basicSlashes?.EnableCollision(BasicSlashes.Type.MidAir_Overhead, false);
        }

        public void SwordUpSlashFX()
        {
            m_basicSlashes?.PlayFXFor(BasicSlashes.Type.Ground_Overhead, true);
        }

        public void SwordUpSlashEnableCollision()
        {
            m_basicSlashes?.EnableCollision(BasicSlashes.Type.Ground_Overhead, true);
        }

        public void SwordUpSlashDisableCollision()
        {
            m_basicSlashes?.EnableCollision(BasicSlashes.Type.Ground_Overhead, false);
        }

        public void CrouchSlashFX()
        {
            m_basicSlashes?.PlayFXFor(BasicSlashes.Type.Crouch, true);
        }
        public void SwordCrouchSlashEnableCollision()
        {
            m_basicSlashes?.EnableCollision(BasicSlashes.Type.Crouch, true);
        }

        public void SlashCombo()
        {
            m_slashCombo?.PlayFX(true);
            m_slashCombo?.EnableCollision(true);
        }

        public void WhipCombo()
        {
            //Debug.Log("Do Whip Combo EVENT");
            // m_whipCombo?.PlayFXFor(WhipAttack.Type.Ground_Forward,true);
            //m_whipCombo?.PlayFX(true);
            m_whipCombo?.EnableCollision(true);
        }

        public void GroundForwardWhipAttackFX()
        {
            //m_whip?.PlayFXFor(WhipAttack.Type.Ground_Forward, true);
            m_whip?.PlayFXFor(WhipAttack.Type.Ground_Forward, true);
            m_whip?.EnableCollision(WhipAttack.Type.Ground_Forward, true);
        }

        public void GroundOverheadWhipAttackFX()
        {
            //m_whip?.PlayFXFor(WhipAttack.Type.Ground_Overhead, true);
            m_whip?.PlayFXFor(WhipAttack.Type.Ground_Overhead, true);
            m_whip?.EnableCollision(WhipAttack.Type.Ground_Overhead, true);
        }

        public void MidairForwardWhipAttackFX()
        {
            m_whip?.PlayFXFor(WhipAttack.Type.MidAir_Forward, true);
            m_whip?.EnableCollision(WhipAttack.Type.MidAir_Forward, true);
        }

        public void MidairOverheadWhipAttackFX()
        {
            m_whip?.PlayFXFor(WhipAttack.Type.MidAir_Overhead, true);
            m_whip?.EnableCollision(WhipAttack.Type.MidAir_Overhead, true);
        }

        public void CrouchForwardWhipAttackFX()
        {
            m_whip?.PlayFXFor(WhipAttack.Type.Ground_Forward, true);
            m_whip?.EnableCollision(WhipAttack.Type.Crouch_Forward, true);
        }

        public void ContinueSlashCombo()
        {
            m_slashCombo?.PlayFX(true);
            m_slashCombo?.EnableCollision(true);
        }
        public void DisableBasicSlashCollisions()
        {
            m_basicSlashes?.EnableCollision(BasicSlashes.Type.Ground_Overhead, false);
            m_basicSlashes?.EnableCollision(BasicSlashes.Type.MidAir_Overhead, false);
            m_basicSlashes?.EnableCollision(BasicSlashes.Type.MidAir_Forward, false);
            m_basicSlashes?.EnableCollision(BasicSlashes.Type.Crouch, false);
        }

        public void DisableSlashComboCollisions()
        {
            m_slashCombo?.EnableCollision(false);
        }

        public void DisableWhipComboCollisions()
        {
            m_whipCombo?.EnableCollision(false);
        }
        public void Null() { }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            m_basicSlashes = GetComponentInChildren<BasicSlashes>();
            m_slashCombo = GetComponentInChildren<SlashCombo>();
            m_whip = GetComponentInChildren<WhipAttack>();
            m_whipCombo = GetComponentInChildren<WhipAttackCombo>();
        }
    }
}