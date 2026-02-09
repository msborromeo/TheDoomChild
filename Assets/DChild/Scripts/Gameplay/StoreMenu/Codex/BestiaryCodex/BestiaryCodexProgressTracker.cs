using DChild.Gameplay;
using DChild.Gameplay.Combat;
using DChild.Menu.Bestiary;
using UnityEngine;
using DChild.Gameplay.UI;
using Sirenix.OdinInspector;

namespace DChild.Menu.Codex.Bestiary
{
    public class BestiaryCodexProgressTracker : CodexProgressTracker<BestiaryList, BestiaryData>
    {
        [SerializeField]
        private Attacker m_attacker;

        public void RecordCreatureToBestiary(int ID)
        {
            if (HasInfoOf(ID) == false)
            {
                GameplaySystem.gamplayUIHandle.notificationManager.QueueNotification(StoreNotificationType.Bestiary, ID);
            }
            SetProgress(ID, true);
        }

        [Button]
        public void RecordCreatureToBestiary(BestiaryData data)
        {
            RecordCreatureToBestiary(data.id);
        }

        private void Awake()
        {
            m_attacker.TargetDamaged += OnTargetDamaged;
        }

        private void OnDisable()
        {
            m_attacker.TargetDamaged -= OnTargetDamaged;
        }

        private void OnTargetDamaged(object sender, CombatConclusionEventArgs eventArgs)
        {
            if (eventArgs.target.instance.isAlive == false && eventArgs.target.hasBestiaryData)
            {
                RecordCreatureToBestiary(eventArgs.target.bestiaryID);
            }
        }

#if UNITY_EDITOR
        public void Initialize(GameObject character)
        {
            m_attacker = character.GetComponent<Attacker>();
        }
#endif
    }
}