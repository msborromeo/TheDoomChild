using DChild.Gameplay;
using DChild.Gameplay.UI;
using DChild.Menu.Codex;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Codex.Characters
{
    public class CharacterCodexProgressTracker : CodexProgressTracker<CharacterCodexList, CharacterCodexData>
    {
        public void RecordCharacterToCodex(int ID)
        {
            if (HasInfoOf(ID) == false)
            {
                GameplaySystem.gamplayUIHandle.notificationManager.QueueNotification(StoreNotificationType.Character, ID);
            }
            SetProgress(ID, true);
        }

        [Button]
        public void RecordCharacterToCodex(CharacterCodexData data)
        {
            RecordCharacterToCodex(data.id);
        }

        private void Awake()
        {

        }

#if UNITY_EDITOR
        public void Initialize(GameObject character)
        {

        }
#endif
    }

}

