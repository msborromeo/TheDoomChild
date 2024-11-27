using UnityEngine;

namespace DChild.Gameplay.Systems
{
    public class ArmyBattleGameplaySystem : MonoBehaviour
    {
        private static ArmyBattleGameplaySystem m_instance;

        private static DChild.Gameplay.Systems.ArmyBattlePlayerManager m_playerManager;

        public static IPlayerManager playerManager => m_playerManager;
        private void AssignModules()
        {
            AssignModule(out m_playerManager);
        }

        private void AssignModule<T>(out T module) where T : MonoBehaviour, IGameplaySystemModule => module = GetComponentInChildren<T>();

        private void Awake()
        {
            if (m_instance)
            {
                Destroy(gameObject);
            }
            else
            {
                m_instance = this;
                AssignModules();

                var worldTypeManager = FindObjectOfType<WorldTypeManager>();
                worldTypeManager?.SetCurrentWorldType(Environment.Location._COUNT);

                var initializables = GetComponentsInChildren<IGameplayInitializable>();
                for (int i = 0; i < initializables.Length; i++)
                {
                    initializables[i].Initialize();
                }

                GameplaySystem.campaignSerializer?.Load(SerializationScope.Gameplay | SerializationScope.Menu, true);

                Debug.Log("ArmyBattle Gameplay Awake Done");
            }
        }


        private void OnDestroy()
        {
            if (m_instance == this)
            {
                m_instance = null;
                m_playerManager = null;
            }
        }
    }
}

