using DChild.Gameplay.Quests;
using DChild.Gameplay.Systems;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.Gameplay.UI
{
    public class PromptNotificationHandle : NotificationHandle<PromptNotificationHandle.NotificationType>, INotificationHandle
    {
        #region SubHandles
        protected class PromptSubHandle<T> : SubHandle<T> where T : NotificationUI
        {
            public void AddListenerToOnNotificationShown(UnityAction action)
            {
                ui.container.OnVisibleCallback.Event.AddListener(action);
            }
        }

        protected class StoreNotificationHandle : PromptSubHandle<StoreNotificationUI>
        {
            [SerializeField]
            private Dictionary<StoreNotificationType, StoreNotificationInfo> m_storeNotificationPair;
            private StorePage m_openStoreOverride;

            public void ShowNotification(StoreNotificationType storeNotificationType)
            {
                if (m_storeNotificationPair.TryGetValue(storeNotificationType, out StoreNotificationInfo info))
                {
                    switch (storeNotificationType)
                    {
                        case StoreNotificationType.Bestiary:
                            m_openStoreOverride = StorePage.Bestiary;
                            break;
                        case StoreNotificationType.Lore:
                            m_openStoreOverride = StorePage.Bestiary;
                            break;
                        case StoreNotificationType.Extras:
                            m_openStoreOverride = StorePage.Bestiary;
                            break;
                    }
                    ui.Show(info);
                }
                ui.container.Show();
            }

            public void OpenStore()
            {
                GameplaySystem.gamplayUIHandle.OpenStoreAtPage(m_openStoreOverride);
            }

            public override void AddListenerToOnNotificationHidden(UnityAction action)
            {
                ui.container.OnHiddenCallback.Event.AddListener(action);
            }
        }
        #endregion

        public enum NotificationType
        {
            Quest,
            Loot,
            Store,
            Journal,
        }

        [SerializeField]
        private GameplayInput m_input;
        [SerializeField]
        private PromptSubHandle<QuestNotificationUI> m_questNotification;
        [SerializeField]
        private PromptSubHandle<LootAcquiredUI> m_lootNotification;
        [SerializeField]
        private StoreNotificationHandle m_storeNotification;

        public void QueueNotification(QuestEntryArgs questInfo)
        {
            bool appendQuestNotifiction = false;
            if (questInfo.entryNumber != 0)
            {
                if (questInfo.entryNumber == 1)
                {
                    if (QuestLog.GetQuestEntryState(questInfo.questName, questInfo.entryNumber) == QuestState.Success)
                    {
                        appendQuestNotifiction = QuestLog.GetQuestState(questInfo.questName) == QuestState.Success;
                        RemoveQuestNotification(questInfo.questName);
                    }
                }
                else
                {
                    if (QuestLog.GetQuestEntryState(questInfo.questName, questInfo.entryNumber) == QuestState.Success)
                    {
                        appendQuestNotifiction = QuestLog.GetQuestState(questInfo.questName) == QuestState.Success;
                    }
                    RemoveQuestNotification(questInfo.questName);
                }
            }
            else
            {
                //There is a chance that the quest Notif will be called twice due to how Quest State Changes work in Dialogue System
                //Might as well Remove Any Existing ones
                RemoveQuestNotification(questInfo.questName);
            }

            AddNotificationRequest(new NotificationRequest<NotificationType>(m_questNotification.priority, NotificationType.Quest, questInfo));
            if (appendQuestNotifiction)
            {
                var newQuestInfo = new QuestEntryArgs(questInfo.questName, 0);
                AddNotificationRequest(new NotificationRequest<NotificationType>(m_questNotification.priority, NotificationType.Quest, newQuestInfo));
            }

            void RemoveQuestNotification(string questName)
            {
                for (int i = m_requests.Count - 1; i >= 0; i--)
                {
                    var request = m_requests[i];
                    if (request.type == NotificationType.Quest)
                    {
                        var requestQuestNotif = (QuestEntryArgs)request.referenceData;
                        if (requestQuestNotif.questName == questInfo.questName && requestQuestNotif.entryNumber == 0)
                        {
                            m_requests.RemoveAt(i);
                        }
                    }
                }
            }
        }

        public void QueueNotification(LootList lootList)
        {
            AddNotificationRequest(new NotificationRequest<NotificationType>(m_lootNotification.priority, NotificationType.Loot, lootList));
        }

        public void QueueNotification(StoreNotificationType storeNotificationType)
        {
            AddNotificationRequest(new NotificationRequest<NotificationType>(m_storeNotification.priority, NotificationType.Store, storeNotificationType));
        }

        protected override void InitializeSubHandles(List<SubHandle> subHandles)
        {
            subHandles.Add(m_questNotification);
            subHandles.Add(m_lootNotification);
            subHandles.Add(m_storeNotification);



            UnityAction questNotifCallback = () => { AddOverrideInputCallBackFor(m_questNotification, CreateCallBack(m_questNotification.ui, StorePage.Bestiary)); };
            m_questNotification.AddListenerToOnNotificationShown(questNotifCallback);

            UnityAction lootNotifCallback = () => { AddOverrideInputCallBackFor(m_lootNotification, CreateCallBack(m_lootNotification.ui, StorePage.Items)); };
            m_lootNotification.AddListenerToOnNotificationShown(lootNotifCallback);

            EventAction<EventActionArgs> storeNotificationOverrideInputCallback = (sender, eventArgs) =>
            {
                m_storeNotification.ui.container.Hide();
                m_storeNotification.OpenStore();
            };
            UnityAction storeNotificationCallback = () => { AddOverrideInputCallBackFor(m_storeNotification, storeNotificationOverrideInputCallback); };
            m_storeNotification.AddListenerToOnNotificationShown(storeNotificationCallback);

            AddListenerToOnNotificationHidden(RemoveInputOverride);
        }

        protected override void HandleNotification(NotificationRequest<NotificationType> notificationRequest)
        {
            switch (notificationRequest.type)
            {
                case NotificationType.Quest:
                    m_questNotification.ui.UpdateLog((QuestEntryArgs)notificationRequest.referenceData);
                    m_questNotification.ui.container.Show();
                    break;
                case NotificationType.Loot:
                    m_lootNotification.ui.SetDetails((LootList)notificationRequest.referenceData);
                    m_lootNotification.ui.container.Show();
                    break;
                case NotificationType.Store:
                    m_storeNotification.ShowNotification((StoreNotificationType)notificationRequest.referenceData);
                    break;
                case NotificationType.Journal:
                    break;
            }
        }

        private EventAction<EventActionArgs> CreateCallBack(NotificationUI ui, StorePage storePageToOpen)
        {
            EventAction<EventActionArgs> callback = (sender, eventArgs) =>
            {
                ui.container.Hide();
                GameplaySystem.gamplayUIHandle.OpenStoreAtPage(storePageToOpen);
            };

            return callback;
        }

        private void AddOverrideInputCallBackFor<T>(PromptSubHandle<T> subHandle, EventAction<EventActionArgs> callback) where T : NotificationUI
        {
            m_input.OverrideOpenStore(subHandle.ui.container.AutoHideAfterShowDelay, callback);
        }

        private void RemoveInputOverride()
        {
            m_input.RemoveOverrideOpenStore();
        }
    }
}