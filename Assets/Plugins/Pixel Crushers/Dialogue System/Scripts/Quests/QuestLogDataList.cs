using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem
{
    [CreateAssetMenu(fileName = "QuestLogDataList", menuName = "DChild/Database/Quest Log DataList")]
    public class QuestLogDataList : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField, AssetSelector(IsUniqueList = true)]
        private DialogueDatabase[] m_databases;

        [Button, PropertyOrder(-1), ShowIf("@m_databases.Length > 0")]
        private void RetriveData()
        {
            var retriever = new QuestLogDataRetriever();
            m_mainQuests = retriever.RetrieveQuestDatas(m_databases, true);
            m_sideQuests = retriever.RetrieveQuestDatas(m_databases, false);
        }
#endif

        [SerializeField, TabGroup("Main Quest")]
        private Quest[] m_mainQuests;
        [SerializeField, TabGroup("Side Quest")]
        private Quest[] m_sideQuests;

        public Quest[] mainQuests => m_mainQuests;
        public Quest[] sideQuests => m_sideQuests;

    }
}
