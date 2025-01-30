using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using I2.Loc;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TextMeshLocalizerInjector : MonoBehaviour
{
   [SerializeField] private GameObject m_targetObject;
   
   [SerializeField,ValueDropdown("GetChildTextOfTargetObject",IsUniqueList = true)]
   private TextMeshProUGUI[] m_labels;

    [SerializeField] private string m_section;



   [Button]
   public void InjectLocalization()
   {
      foreach (var label in m_labels)
      {
         if (label.GetComponent<Localize>() != null)
            continue;
         label.gameObject.AddComponent<Localize>();
         Debug.Log($"Added Localize Component: {label.name}");
      }
   }

   [Button]
   private void InitializeLocalize()
   {
      foreach (var label in m_labels)
      {
         var localize = label.GetComponent<Localize>();
         if (localize != null)
         {
            localize.mTerm = $"{m_section.ToUpper()}/{label.text}";
         }
         Debug.LogWarning($"Localize Component n/a; {label.name}");
      }      
   }

   private IEnumerable GetChildTextOfTargetObject()
   {
      if (m_targetObject == null) return null;
     var candidates = m_targetObject.GetComponentsInChildren<TextMeshProUGUI>().ToList();

      Func<Transform, string> getPath = null;
      getPath = x => (x ? getPath(x.parent) + "/" + x.gameObject.name : "");
      return candidates.Select(x => new ValueDropdownItem(getPath(x.transform), x));
   }
}