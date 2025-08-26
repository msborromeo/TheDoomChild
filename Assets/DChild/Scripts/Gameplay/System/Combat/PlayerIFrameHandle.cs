using DChild.Gameplay.Characters.Players;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using static DChild.Gameplay.Combat.Damageable;

namespace DChild.Gameplay.Combat
{
    [System.Serializable]
    public class PlayerIFrameHandle
    {
        [SerializeField, BoxGroup("IFrame")]
        private float m_invulnerabilityDuration;
        [SerializeField, BoxGroup("IFrame")]
        private float m_inputDisableDuration;

        public IEnumerator ExecuteTemporaryInvulnerability(IPlayer player)
        {
            var damageableModule = player.damageableModule;
            damageableModule.SetInvulnerability(Invulnerability.MAX);
            //Subscribe to OnInvulnerability changed
            damageableModule.InvulnerabilityChanged += OnInvulnerabilityChanged;
            yield return new WaitForWorldSeconds(m_invulnerabilityDuration);
            //Unsubscribe to OnInvulnerability changed
            damageableModule.InvulnerabilityChanged -= OnInvulnerabilityChanged;
            damageableModule.SetInvulnerability(Invulnerability.None);
        }

        private void OnInvulnerabilityChanged(object sender, InvulnerabilityEventArgs eventArgs)
        {
            if (eventArgs.invulnerabilityLevel != Invulnerability.MAX)
            {
                eventArgs.damageables.SetInvulnerability(Invulnerability.MAX);
            }
        }

        public IEnumerator DisableInputTemporarily(IPlayer player)
        {
            Debug.Log("Flinch Disable Input");
            GameplaySystem.playerManager.DisableControls();
            yield return new WaitForWorldSeconds(m_inputDisableDuration);
            GameplaySystem.playerManager.EnableControls();
            Debug.Log("Flinch Enable Input");
            player.state.canFlinch = true;
        }
    }
}