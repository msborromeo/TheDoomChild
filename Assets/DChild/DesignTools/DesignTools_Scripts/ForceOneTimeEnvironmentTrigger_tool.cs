using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ForceOneTimeEnvironmentTrigger_tool : MonoBehaviour
{
    [SerializeField]
    private bool m_RevertChangesClearData;
    [SerializeField, ReadOnly]
    private List<DialogueSystemTrigger> m_dialogueDatabases = new List<DialogueSystemTrigger>();
    private List<DialogueSystemTriggerEvent> m_dialougeTriggerState = new List<DialogueSystemTriggerEvent>();

    private bool m_hideFindAllDialogueSystemTriggers;
    private bool m_hideScriptConverterFunciton;
#if UNITY_EDITOR
    [Button(ButtonSizes.Gigantic)]
    [GUIColor(0, 1, 0)]
    [ButtonGroup("MyButton"), HideIf("m_RevertChangesClearData"), HideIf("m_hideFindAllDialogueSystemTriggers")]
    public void FindAllDialogueSystemTriggers()
    {
        m_dialogueDatabases.Clear();
        m_dialougeTriggerState.Clear();
        DialogueSystemTrigger[] triggers = FindObjectsOfType<DialogueSystemTrigger>(true);
       
            
        foreach (DialogueSystemTrigger trigger in triggers)
        {
            if (trigger.gameObject.name.Contains("Banter"))
                continue;

            if (trigger.trigger != DialogueSystemTriggerEvent.OnTriggerEnter)
                continue;

            var triggerState = trigger.GetComponent<DialogueSystemTrigger>().trigger;
            m_dialougeTriggerState.Add(triggerState);
            m_dialogueDatabases.Add(trigger);
            Debug.Log($"Found: {trigger.gameObject.name}");
            Debug.Log($"Found: {triggerState.ToString()}");

        }
        if (m_dialogueDatabases.Count == 0)
        {
            EditorUtility.DisplayDialog(
                "Nigga what double check scene",
                "What the hell no DialogueSystemTrigger found in scene",
                "OKAY");
            return;
        }
        m_hideFindAllDialogueSystemTriggers = true;
        Debug.Log($"Total DialogueSystemTrigger objects found: {m_dialogueDatabases.Count}");
        Debug.Log($"Total m_dialougeTriggerState objects found: {m_dialougeTriggerState.Count}");
    }
#endif
    [ButtonGroup("MyButton"), HideIf("@this.m_RevertChangesClearData || this.m_hideScriptConverterFunciton"),ShowIf("m_hideFindAllDialogueSystemTriggers")]
    private void ConvertToOnUseAndAddScript()
    {
        
        if (m_dialogueDatabases.Count > 0)
        {
            m_hideScriptConverterFunciton = true;
#if UNITY_EDITOR

            EditorUtility.DisplayDialog(
                "DONE",
                "Check each dialogues",
                "OKAY");
#endif
            for (int i = 0; i < m_dialogueDatabases.Count; i++)
            {

                m_dialogueDatabases[i].trigger = DialogueSystemTriggerEvent.OnUse;
                var newAddedComponents = m_dialogueDatabases[i].gameObject.AddComponent<ForceOneTimeEnvironmentTrigger>();
                var dialogueSystemTrigger = m_dialogueDatabases[i].gameObject.GetComponent<DialogueSystemTrigger>();
                newAddedComponents.m_hasDialogue = true;
                newAddedComponents.m_dialogueToTrigger = dialogueSystemTrigger;
            }
        }
        else
        {
#if UNITy_EDITOR
            EditorUtility.DisplayDialog(
                "Empty List Of Dialogues",
                "No dialogues Found",
                "Sorry My Bad"
            );
#endif


           // TrollPopup.ShowWindow();

        }    
    }
    [GUIColor(1,0,0)]
    [Button(ButtonSizes.Large), ShowIf("@this.m_RevertChangesClearData|| this.m_hideScriptConverterFunciton")]
    private void RevertChangesClearData()
    {
        if(m_dialogueDatabases.Count > 0)
        {
            m_hideScriptConverterFunciton = false;
            for (int i = 0; i < m_dialogueDatabases.Count; i++)
            {           
                 m_dialogueDatabases[i].trigger = m_dialougeTriggerState[i];

                if (m_dialogueDatabases[i].gameObject.
                TryGetComponent<ForceOneTimeEnvironmentTrigger>(out ForceOneTimeEnvironmentTrigger trigger))
                {
                    trigger.m_hasDialogue = false;
                    trigger.m_dialogueToTrigger = null;
                    DestroyImmediate(trigger);
                }        
            }
            m_hideFindAllDialogueSystemTriggers = false;
            m_RevertChangesClearData = false;
            m_dialogueDatabases.Clear();
            m_dialougeTriggerState.Clear();

        }
        else
        {
            m_hideFindAllDialogueSystemTriggers = false;
            m_RevertChangesClearData = false;
            m_dialogueDatabases.Clear();
#if UNITY_EDITOR
            EditorUtility.DisplayDialog(
                          "Empty List of Dialogues",
                          "No dialogues Found :D",
                          "Sorry My Bad :("
                      );
#endif
            // m_wantToRevertChanges = false;
        }    
    }




#if UNITY_EDITOR
    #region :D
    public class TrollPopup : EditorWindow
    {
        private static TrollPopup window;
        private Rect proceedButtonRect = new Rect(10, 40, 100, 30);
        private Rect cancelButtonRect = new Rect(130, 40, 100, 30);
        public static void ShowWindow()
        {
            window = CreateInstance<TrollPopup>();
            window.ShowPopup();
            window.position = new Rect(300, 300, 500, 500);
        }

        private void OnGUI()
        {
            GUILayout.Label("Choose wisely...", EditorStyles.wordWrappedLabel);

            Vector2 mousePos = Event.current.mousePosition;

            // Evade if mouse gets too close to "Proceed"
            if (proceedButtonRect.Contains(mousePos))
            {
                Vector2 randomOffset = new Vector2(Random.Range(-200, 200), Random.Range(-200, 200));
                position = new Rect(position.position + randomOffset, position.size);
                Repaint();
            }

            if (GUI.Button(proceedButtonRect, "Proceed"))
            {
                Debug.Log("You bravely proceeded.");
                Close();
            }

            if (GUI.Button(cancelButtonRect, "Cancel"))
            {
                Debug.Log("You backed out safely.");
                Close();
            }
        }
    }
    #endregion
#endif

}

