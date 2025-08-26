using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace DChild.CustomInput.Keybind
{
    public class KeyboardBindButton : MonoBehaviour
    {
        [SerializeField]
        private KeybindFieldUI m_ui;
        [SerializeField]
        private KeybindSelection m_selection;
        public KeybindSelection selection => m_selection;

        [SerializeField, ReadOnly]
        private string m_currentPath;
        [SerializeField]
        private InputActionAsset m_actionAsset;


        //[Button]
        //private void Display()
        //{
        //    SetActionMap("Underworld");

        //    if (m_ui != null)
        //    {
        //        var inputMap = m_actionAsset.FindActionMap(m_actionMap);

        //        var input  = inputMap.FindAction(m_selection.ToString());
        //        Debug.Log("received input:" + input);
        //    }
        //}

        [Button]
        private void ResetKeybind()
        {
            
        }      

        public void UpdateUI(InputBinding binding)
        {
            m_ui.UpdateVisual(binding);
            m_currentPath = binding.effectivePath;
        }

#if UNITY_EDITOR
        [Button]
        private void Rebind()
        {
            GetComponent<UIButton>().Click();
        }
#endif
    }
}
