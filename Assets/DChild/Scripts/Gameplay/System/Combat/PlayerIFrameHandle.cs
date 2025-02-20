using DChild.Gameplay.Characters.Players;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

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
            yield return new WaitForWorldSeconds(m_invulnerabilityDuration);
            damageableModule.SetInvulnerability(Invulnerability.None);
        }

        public IEnumerator DisableInputTemporarily(IPlayer player)
        {
            
            GameplaySystem.playerManager.DisableControls();
            yield return new WaitForWorldSeconds(m_inputDisableDuration);
            GameplaySystem.playerManager.EnableControls();
            player.state.canFlinch = true;
        }
    }
}