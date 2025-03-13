using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace DChild.Gameplay.UI
{
    public class SetTextToTextBox : MonoBehaviour
    {

        [TextArea(2, 5)]
        [SerializeField]
        private string m_message;

        [SerializeField]
        private SpriteButtonIconListObject m_spriteButtonList;
        [SerializeField]
        private CurrentDeviceType m_deviceType;

        [SerializeField]
        private InputManager m_inputManager;

        [SerializeField]
        private PlayerControls m_playerControls;
        [SerializeField]
        private InputActionReference m_inputaction;
        InputAction m_action;

        private List<InputBinding> currentBinding = new List<InputBinding>();

        private TMP_Text m_textbox;

        [Button]
        public void SetText()
        {
            //if((int)m_deviceType > m_spriteButtonList.tmpSpriteList.Count - 1)
            //{
            //    return;
            //}
            if ((int)m_deviceType > m_spriteButtonList.tmpSpriteList.Count - 1)
            {
                return;
            }


            if(m_inputaction.action.bindings.Count > (int)CurrentDeviceType._COUNT)
            {
                var inputBinding = m_inputaction.action.bindings;
             
                // Optimize this later
                for (int x = 0; x < inputBinding.Count; x++)
                {
                    if (inputBinding[x].isComposite)
                    {
                        // filter out the composite to get modifier and binding key
                    }
                    if (inputBinding[x].isPartOfComposite)
                    {
                        currentBinding.Add(inputBinding[x]);
                    }

                }
            }
            if(currentBinding.Count > 0)
            {
                var keyBoardList = new List<InputBinding>();
                var gamepadList = new List<InputBinding>();
                var psList = new List<InputBinding>();
                for(int x = 0; x < currentBinding.Count; x++)
                {
                    var curBind = currentBinding[x];
                    if (curBind.effectivePath.Contains("Keyboard"))
                    {
                        // check if already in list
                        if (keyBoardList.Contains(curBind))
                        {
                            continue;
                        }
                        else
                        {
                            keyBoardList.Add(curBind);
                        }
                        
                        
                    }
                    if (curBind.effectivePath.Contains("Gamepad"))
                    {
                        if (gamepadList.Contains(curBind))
                        {
                            continue;
                        }
                        else
                        {
                            gamepadList.Add(curBind);
                        }
                    }
                    if (curBind.effectivePath.Contains("PS4"))
                    {
                        if (psList.Contains(curBind))
                        {
                            continue;
                        }
                        else
                        {
                            psList.Add(curBind);
                        }
                        psList.Add(curBind);
                    }

                }
                //var startIndex = ((int)m_deviceType * currentBinding.Count) + (int)m_deviceType;
                //var buttonIndex = startIndex + 1;
                //Debug.Log("Modifier "+currentBinding[startIndex]);
                //Debug.Log("Button "+currentBinding[buttonIndex]);
                if(keyBoardList.Count > 2)
                {
                    if(m_deviceType == CurrentDeviceType.Keyboard)
                    {
                        m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceCompositeBinding(m_message, keyBoardList[0], keyBoardList[1], keyBoardList[2], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
                    }
                    if (m_deviceType == CurrentDeviceType.Gamepad)
                    {
                        m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceCompositeBinding(m_message, gamepadList[0], gamepadList[1], gamepadList[2], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
                    }
                    if (m_deviceType == CurrentDeviceType.PS4)
                    {
                        m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceCompositeBinding(m_message, psList[0], psList[1], psList[2], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
                    }

                }
                else
                {
                    if(m_deviceType == CurrentDeviceType.Keyboard)
                    {
                        m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceCompositeBinding(m_message, keyBoardList[0], keyBoardList[1], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
                    }
                    if (m_deviceType == CurrentDeviceType.Gamepad)
                    {
                        m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceCompositeBinding(m_message, gamepadList[0], gamepadList[1], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
                    }
                    if (m_deviceType == CurrentDeviceType.PS4)
                    {
                        m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceCompositeBinding(m_message, psList[0], psList[1], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
                    }


                }
                

                    //m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceCompositeBinding(m_message, currentBinding[startIndex], currentBinding[startIndex + 1], currentBinding[startIndex + 2], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);

            }
            else
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_inputaction.action.bindings[(int)m_deviceType], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
            }
            
            //m_textbox.text = FillInTextWithButtonSprite.ReplaceBindings(m_message, m_deviceType,m_inputManager, m_spriteButtonList);
        }

        private void OnDestroy()
        {
            m_inputManager.OnActiveDeviceChange -= SetText;
            m_inputManager.BindingsChangedEvent -= SetText;
        }

        private void Awake()
        {
            m_playerControls = new PlayerControls();
            m_action = new InputAction();
            m_textbox = GetComponent<TMP_Text>();

            m_inputManager.GetCurrentDevice();
            m_inputManager.OnActiveDeviceChange += SetText;
            m_inputManager.BindingsChangedEvent += SetText;
        }
        // Start is called before the first frame update
        void Start()
        {  
            SetText();

        }

    }
}

