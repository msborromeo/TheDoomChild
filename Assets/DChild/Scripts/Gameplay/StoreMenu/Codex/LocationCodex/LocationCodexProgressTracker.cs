using DChild.Gameplay;
using DChild.Gameplay.UI;
using DChild.Menu.Codex;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Codex.LocationCodex
{
    public class LocationCodexProgressTracker : CodexProgressTracker<LocationCodexList, LocationCodexData>
    {

        public void RecordLocationToCodex(int ID)
        {
            if (HasInfoOf(ID) == false)
            {
                GameplaySystem.gamplayUIHandle.notificationManager.QueueNotification(StoreNotificationType.Location,ID);
            }
            SetProgress(ID, true);
        }

        [Button]
        public void RecordLocationToCodex(LocationCodexData data)
        {
            RecordLocationToCodex(data.id);
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

