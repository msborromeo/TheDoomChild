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
        private void RecordEntry(int id, StoreNotificationType notificationType)
        {
            if (!HasInfoOf(id))
                GameplaySystem.gamplayUIHandle.notificationManager.QueueNotification(notificationType, id);
            
            SetProgress(id, true);
        }

        public void RecordCharacterToCodex(int id) => RecordEntry(id, StoreNotificationType.Character);

        public void RecordArmyUnitToCodex(int id) => RecordEntry(id, StoreNotificationType.ArmyTroops);

        [Button]
        public void RecordCharacterToCodex(CharacterCodexData data)
        {
            if (data == null) return;

            StoreNotificationType notificationType = data.characterType == CharacterType.Army
                ? StoreNotificationType.ArmyTroops
                : StoreNotificationType.Character;

            RecordEntry(data.id, notificationType);
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

