using DChild.Gameplay;
using UnityEngine;

namespace DChild.Codex.Characters
{
    public class CharacterCodexEntryUpdate : MonoBehaviour
    {
        [SerializeField] private CharacterCodexData[] m_charactersForUpdate;

        public void UpdateCharacterCodex()
        {
            var tracker = GameplaySystem.playerManager.player.characterTracker;

            for (int i = 0; i < m_charactersForUpdate.Length; i++)
                tracker.RecordCharacterToCodex(m_charactersForUpdate[i]);
        }

        public void CharacterInteractionUpdate()
        {
            for (int i = 0;i < m_charactersForUpdate.Length; i++)
            {
                if (m_charactersForUpdate[i].firstInteract == false)
                {
                    m_charactersForUpdate[i].firstInteract = true;
                }

                if (!m_charactersForUpdate[i].doomedKnightComment == false)
                {
                    m_charactersForUpdate[i].doomedKnightComment = true;
                }

                if (!m_charactersForUpdate[i].secondInteract == false)
                {
                    m_charactersForUpdate[i].secondInteract = true;
                }
            }
        }
    }
}

