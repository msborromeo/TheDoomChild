using DChild.Gameplay;
using UnityEngine;

namespace DChild.Codex.Characters
{
    public class CharacterCodexEntryUpdate : MonoBehaviour
    {
        [SerializeField] private CharacterCodexData[] m_charactersForUpdate;

        public void UpdateCharacterCodex()
        {
            var tracker = GameplaySystem.playerManager.player.GetComponentInChildren<CharacterCodexProgressTracker>();

            for (int i = 0; i < m_charactersForUpdate.Length; i++)
                tracker.RecordCharacterToCodex(m_charactersForUpdate[i]);
        }
    }
}

