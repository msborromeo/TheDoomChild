using DChild.Gameplay;
using DChild.Gameplay.UI;
using DChild.Menu.Codex;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Codex.Tutorial
{
    public class TutorialCodexProgressTracker : CodexProgressTracker<TutorialCodexList, TutorialCodexData>
    {

        public void RecordTutorialToCodex(int ID)
        {
            if (HasInfoOf(ID) == false)
            {
                GameplaySystem.gamplayUIHandle.notificationManager.QueueNotification(StoreNotificationType.Tutorial,ID);
            }
            SetProgress(ID, true);
        }

        [Button]
        public void RecordTutorialToCodex(TutorialCodexData data)
        {
            RecordTutorialToCodex(data.id);
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


