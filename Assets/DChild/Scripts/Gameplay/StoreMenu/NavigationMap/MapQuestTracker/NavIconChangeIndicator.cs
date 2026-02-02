using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.QuestHints.ChestMapTracker
{
    public class NavIconChangeIndicator : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Sprite spriteDefault, spriteInteracted;
        [SerializeField, ValueDropdown("GetVariables", IsUniqueList = true, SortDropdownItems = true)]
        private string Variable;
        [SerializeField]
        DialogueDatabase database;

        private IEnumerable GetVariables()
        {
            ValueDropdownList<string> list = new ValueDropdownList<string>();

            foreach (var variable in database.variables)
            {
                list.Add(variable.Name.ToString());
            }

            return list;
        }

        private void OnEnable()
        {
            //Debug.LogError("AAAAAA");
            if (DialogueLua.GetVariable(Variable).asBool)
            {
                image.sprite = spriteInteracted;
            }
        }
    }
}
