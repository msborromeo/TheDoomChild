using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.Gameplay.UI
{
    public class SetTextToTextBox : MonoBehaviour
    {

        private enum CurrentDevice
        {
            Keyboard,
            Gamepad,
            Ps4
        }


        [TextArea(2, 5)]
        [SerializeField]
        private string m_message;

        [SerializeField]
        private SpriteButtonIconListObject m_spriteButtonList;
        [SerializeField]
        private CurrentDevice m_deviceType;

        [SerializeField]
        private PlayerControls m_playerControls;
        [SerializeField]
        private InputActionReference m_inputaction;
        InputAction m_action;

        private TMP_Text m_textbox;

        [Button]
        public void SetText()
        {
            //if((int)m_deviceType > m_spriteButtonList.tmpSpriteList.Count - 1)
            //{
            //    return;
            //}

            m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_inputaction.action.bindings[(int)m_deviceType], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
        }



        private void Awake()
        {
            m_playerControls = new PlayerControls();
            m_action = new InputAction();
            m_textbox = GetComponent<TMP_Text>();
        }
        // Start is called before the first frame update
        void Start()
        {
            
            if(m_action.activeControl.device.name == CurrentDevice.Keyboard.ToString())
            {
                m_deviceType = CurrentDevice.Keyboard;
            }
            else if(m_action.activeControl.device.name == CurrentDevice.Ps4.ToString())
            {
                m_deviceType = CurrentDevice.Ps4;
            }
            else
            {
                m_deviceType = CurrentDevice.Gamepad;
            }
            
            SetText();

        }

    }
}

