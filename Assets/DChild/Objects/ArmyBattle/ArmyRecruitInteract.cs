using DChild.Gameplay.ArmyBattle;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.ArmyBattle.Recruitment
{
    public class ArmyRecruitInteract : MonoBehaviour
    {

        [SerializeField, ValueDropdown("GetVariables", IsUniqueList = true, SortDropdownItems = true)]
        private string Variable;
        [SerializeField]
        DialogueDatabase database;
        [SerializeField, TabGroup("Main/Reference", "Actions")]
        private UnityEvent m_FirstTimeTalk, m_SubsequentUse;
        private Collider2D m_interactCollider;
        private bool m_HasAfterRecruitDialogue;

        private IEnumerable GetVariables()
        {
            ValueDropdownList<string> list = new ValueDropdownList<string>();

            foreach (var variable in database.variables)
            {
                list.Add(variable.Name.ToString());
            }

            return list;
        }
        private void Start()
        {
            m_interactCollider = GetComponentInParent<Collider2D>();
        }
        public void SetDatabase(DialogueDatabase data)
        {
            database = data;
        }

        public void SetVariable(string vari)
        {
            Variable = vari;
        }

        public void SetCollider(Collider2D col)
        {
            Debug.Log(col);
            m_interactCollider = col;
        }

        public void HasAfterRecruitDialogue(bool hasDialogue)
        {
            m_HasAfterRecruitDialogue = hasDialogue;
        }

        public void TurnOffCollider()
        {
            if(!m_HasAfterRecruitDialogue)
            {
                m_interactCollider.enabled = false;
            }
        }
        public void CheckFirstTimeTalk()
        {
            Debug.LogError("AAAAAA");
            if (DialogueLua.GetVariable(Variable).asBool)
            {
                m_FirstTimeTalk?.Invoke();
                return;
            }
            m_SubsequentUse?.Invoke();
        }
    }
}

