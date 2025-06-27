using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
public class ArmyCharacterRecruitmentToolkit : MonoBehaviour
{
    [SerializeField]
    private GameObject m_questTemplate;
    [SerializeField]
    private GameObject m_sequenceTemplate;
    [SerializeField]
    private DialogueDatabase m_databaseTemplate;
    [SerializeField, FolderPath]
    private string m_mainFolderPath;
    [SerializeField, FolderPath]
    private string m_characterDataFolderPath;
    [SerializeField, AssetSelector(Paths = "Assets/DChild/Objects/ArmyBattle/Characters/NPCsPrefab", Filter = "Base")]
    private GameObject[] m_npcPrefabs;

    private const string NPCPREFAB_NAMETAG_SEPERATOR = "_";
    private const string TEMPLATE_CHARACTER_TAG = "(Character)";
    private const string NPCPREFAB_NAMETAG_VALIDATOR = "Base Variant";

    private string GenerateChildFolderName(string npcName) => $"Recruit{npcName}";
    private void GenerateChildFolder(string npcName, out string generatedFolderPath)
    {
        var folderName = GenerateChildFolderName(npcName);
        var folderPath = $"{m_mainFolderPath}/{folderName}";
        if (Directory.Exists(folderPath) == false)
        {
            Directory.CreateDirectory(folderPath);
        }

        generatedFolderPath = folderPath;
    }

    [Button]
    private void ExecuteCreation()
    {
        var databaseAssetPath = AssetDatabase.GetAssetPath(m_databaseTemplate);
        string[] foldersToSearchCharacterData = new string[] { m_characterDataFolderPath };

        for (int i = 0; i < m_npcPrefabs.Length; i++)
        {
            var npcPrefab = m_npcPrefabs[i];
            if (npcPrefab == null)
                continue;

            if (npcPrefab.name.EndsWith(NPCPREFAB_NAMETAG_VALIDATOR) == false)
                continue;

            var npcName = npcPrefab.name.Split(NPCPREFAB_NAMETAG_SEPERATOR)[0];
            if (npcName == null)
            {
                continue;
            }

            string folderPath = "";
            GenerateChildFolder(npcName, out folderPath);

            var prefabName = m_questTemplate.name.Replace(TEMPLATE_CHARACTER_TAG, npcName);
            var prefabAssetPath = $"{folderPath}/{prefabName}.prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabAssetPath))
            {
                Debug.LogWarning($"{prefabName} Already Exists");
                continue;
            }

            //Generate Database
            var newDatabaseAssetPath = $"{folderPath}/DialogueDatabase_Recruit{npcName}.asset";
            if (AssetDatabase.CopyAsset(databaseAssetPath, newDatabaseAssetPath) == false)
            {
                Debug.LogError($"Cannot Create File: {newDatabaseAssetPath}");
                continue;
            }
            var newDatabase = AssetDatabase.LoadAssetAtPath<DialogueDatabase>(newDatabaseAssetPath);
            ModifyDatabaseForQuest(newDatabase, npcName);

            //Instantiate Neccessary prefabs
            var prefabInstance = Instantiate(m_questTemplate);
            prefabInstance.name = prefabName;
            var questEntry = prefabInstance.transform.GetChild(0).gameObject;
            questEntry.name = questEntry.name.Replace(TEMPLATE_CHARACTER_TAG, npcName);

            var npcInstance = PrefabUtility.InstantiatePrefab(npcPrefab) as GameObject;
            npcInstance.transform.SetParent(questEntry.transform, true);
            npcInstance.transform.localPosition = Vector3.zero;
            npcInstance.name = npcInstance.name.Replace(NPCPREFAB_NAMETAG_VALIDATOR, "Recruitable");

            var sequenceInstance = Instantiate(m_sequenceTemplate);
            sequenceInstance.name = sequenceInstance.name.Replace(TEMPLATE_CHARACTER_TAG, npcName);
            sequenceInstance.name = sequenceInstance.name.Replace("(Clone)", "");
            sequenceInstance.transform.SetParent(npcInstance.transform, true);

            //Prefab Instance Modification
            var questName = GenerateQuestName(npcName);
            var questStateListener = prefabInstance.GetComponent<QuestStateListener>();
            questStateListener.questName = GenerateQuestName(npcName);
            var extraDatabases = prefabInstance.GetComponent<ExtraDatabases>();
            extraDatabases.databases = new DialogueDatabase[] { newDatabase };

            var npcDialogueTrigger = npcInstance.GetComponent<DialogueSystemTrigger>();
            npcDialogueTrigger.selectedDatabase = newDatabase;
            npcDialogueTrigger.conversation = $"Recruit/{npcName}";

            var giveCharacterTrigger = sequenceInstance.GetComponent<DialogueSystemTrigger>();
            giveCharacterTrigger.selectedDatabase = newDatabase;
            giveCharacterTrigger.setQuestState = true;
            giveCharacterTrigger.questName = questName;
            giveCharacterTrigger.questState = QuestState.Success;
            giveCharacterTrigger.setQuestEntryState = true;
            giveCharacterTrigger.questEntryNumber = 1;
            giveCharacterTrigger.questEntryState = QuestState.Success;

            giveCharacterTrigger.luaCode = giveCharacterTrigger.luaCode.Replace(TEMPLATE_CHARACTER_TAG, npcName);

            //var characterReward = sequenceInstance.GetComponent<ArmyBattleCharacterReward>();
            //UnityEventTools.AddBoolPersistentListener(characterReward.m_GiveReward, (value) => { npcInstance.GetComponent<BoxCollider2D>().enabled = value; }, false);
            //var characterDatasGUID = AssetDatabase.FindAssets(npcName, foldersToSearchCharacterData);
            //if (characterDatasGUID != null)
            //{
            //    var characterData = AssetDatabase.LoadAssetAtPath<ArmyCharacterData>(AssetDatabase.GUIDToAssetPath(characterDatasGUID[0]));
            //}
            //else
            //{
            //    Debug.LogError($"{npcName} Does not Have a Character Data");
            //}

            PrefabUtility.SaveAsPrefabAssetAndConnect(prefabInstance, prefabAssetPath, InteractionMode.AutomatedAction);
            AssetDatabase.Refresh();
        }
    }

    private void ModifyDatabaseForQuest(DialogueDatabase dialogueDatabase, string npcName)
    {
        //Change Actor Name
        var actors = dialogueDatabase.actors;
        foreach (var actor in actors)
        {
            if (actor.Name == "Player")
                continue;

            actor.Name = npcName;
        }

        //Change Quest Details
        var quest = dialogueDatabase.items[0];
        quest.Name = GenerateQuestName(npcName);
        foreach (var field in quest.fields)
        {
            switch (field.title)
            {
                case "Entry 1":
                    field.value = quest.Name;
                    break;
                default:
                    continue;
            }
        }

        //Change Variable Details
        var recruitedVarTag = "HasRecruited";
        foreach (var variable in dialogueDatabase.variables)
        {
            if (variable.Name.Contains(recruitedVarTag))
            {
                variable.Name = $"{recruitedVarTag}_{npcName}";
            }
        }

        //Change Conversation Details
        var recruitedConversationTag = "Recruit/";
        foreach (var conversation in dialogueDatabase.conversations)
        {
            if (conversation.Title.Contains(recruitedConversationTag))
            {
                conversation.Title = $"{recruitedConversationTag}{npcName}";
            }
        }

        EditorUtility.SetDirty(dialogueDatabase);
        AssetDatabase.SaveAssetIfDirty(dialogueDatabase);
    }

    private string GenerateQuestName(string npcName) => $"Recruit {npcName}";
} 
#endif