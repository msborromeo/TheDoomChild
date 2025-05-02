using DChild.Gameplay.Characters.Players.Modules;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.Gameplay.UI
{
    public class SetTextToTextBox : MonoBehaviour
    {
        private enum InputActionType
        {
            NoneComposite,
            Directional,
            Cycle,
            Modifier,
            _Count
        }

        private enum DirectionActionPart
        {
            Up,
            Down,
            Left,
            Right,
            _Count
        }

        private enum CycleActionPart
        {
            Negative,
            Positive,
            _Count
        }

        [TextArea(2, 5)]
        [SerializeField]
        private string m_message;

        [SerializeField]
        private InputActionType m_actionType;

        [SerializeField]
        private SpriteButtonIconListObject m_spriteButtonList;
        [SerializeField]
        private CurrentDeviceType m_deviceType;

        //Directional
        [SerializeField, ShowIf("@m_actionType == InputActionType.Directional")]
        private DirectionActionPart m_directionPart;

        //Cycle
        [SerializeField, ShowIf("@m_actionType == InputActionType.Cycle")]
        private CycleActionPart m_cycleActionPart;

        [SerializeField]
        private InputManager m_inputManager;

        [SerializeField]
        private PlayerControls m_playerControls;
        [SerializeField]
        private InputActionReference m_inputaction;
        InputAction m_action;

        private List<InputBinding> currentBinding = new List<InputBinding>();
        private List<InputBinding> m_activeDeviceBinding = new List<InputBinding>();

        private TMP_Text m_textbox;

        public static event Action<CurrentDeviceType> DeviceTypeChanged;

        private List<InputBinding> keyBoardList = new List<InputBinding>();
        private List<InputBinding> gamepadList = new List<InputBinding>();
        private List<InputBinding> psList = new List<InputBinding>();

        public void OnActiveControllerChanged(string controlScheme)
        {
            CurrentDeviceType deviceType = CurrentDeviceType.Keyboard;
            if (controlScheme.Contains("Keyboard"))
            {
                deviceType = CurrentDeviceType.Keyboard;
            }

            if (controlScheme.Contains("Gamepad"))
            {
                deviceType = CurrentDeviceType.Gamepad;
            }

            OnDeviceTypeChanged(deviceType);
        }

        public static void ChangeDeviceType(CurrentDeviceType deviceType)
        {
            DeviceTypeChanged?.Invoke(deviceType);
        }

        public CurrentDeviceType deviceType { get { return m_deviceType; } set { m_deviceType = value; } }

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


            if (m_inputaction.action.bindings.Count > (int)CurrentDeviceType._COUNT)
            {

                switch (m_actionType)
                {
                    case InputActionType.Cycle:
                        SetTextToCyclePrompts();
                        break;
                    case InputActionType.Modifier:
                        SetTextToModifierPrompts();
                        break;
                    case InputActionType.Directional:
                        SetTextToDirectionalPrompts();
                        break;
                    case InputActionType.NoneComposite:
                        m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_inputaction.action.bindings[(int)m_deviceType], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
                        break;
                }
            }
            else
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_inputaction.action.bindings[(int)m_deviceType], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
            }



            //m_textbox.text = FillInTextWithButtonSprite.ReplaceBindings(m_message, m_deviceType,m_inputManager, m_spriteButtonList);
        }

        private void PopulateCurrentBinding()
        {
            var inputBinding = m_inputaction.action.bindings;

            // Optimize this later
            //puts ALL bindings in one list called currendBinding
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

            AddCurrentBindings();
        }

        private void AddCurrentBindings()
        {
            for (int x = 0; x < currentBinding.Count; x++)
            {
                var curBind = currentBinding[x];
                Debug.Log("Current Binding Effective Path: " + curBind.effectivePath);
                if (curBind.effectivePath.Contains("Keyboard"))
                {
                    // check if already in list
                    //if(curBind != null)
                    //{
                    //    if (keyBoardList[x] == curBind)
                    //    {
                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        keyBoardList.Add(curBind);
                    //    }
                    //}
                    //else
                    //{
                    //    keyBoardList.Add(curBind);
                    //}

                    keyBoardList.Add(curBind);
                }
                if (curBind.effectivePath.Contains("Gamepad"))
                {
                    //if (gamepadList.Contains(curBind))
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    //}
                        gamepadList.Add(curBind);
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

            if (m_activeDeviceBinding != null)
            {
                m_activeDeviceBinding.Clear();
            }

            if(m_deviceType == CurrentDeviceType.Keyboard)
            {
                for (int x = 0; x < keyBoardList.Count; x++)
                {
                    m_activeDeviceBinding.Add(keyBoardList[x]);
                }

            }
            else if(m_deviceType == CurrentDeviceType.Gamepad)
            {
                for (int x = 0; x < gamepadList.Count; x++)
                {
                    m_activeDeviceBinding.Add(gamepadList[x]);
                }
            }
        }

        //Sets text to Inputs that require Modifiers
        private void SetTextToModifierPrompts()
        {
            if (currentBinding.Count > 0)
            {
                //Where SetCurrentBinding went before
                //AddCurrentBindings();
                //Keyboard list is used as a condition in the if statement because keyboard is the default control devicetype
                if (keyBoardList.Count > 2)
                {
                    if (m_deviceType == CurrentDeviceType.Keyboard)
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
                    if (m_deviceType == CurrentDeviceType.Keyboard)
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
        }

        private void SetTextToDirectionalPrompts()
        {
            if (m_directionPart == DirectionActionPart.Up)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[0], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
            }
            if (m_directionPart == DirectionActionPart.Down)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[1], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
            }

            if (m_directionPart == DirectionActionPart.Left)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[2], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
            }
            if (m_directionPart == DirectionActionPart.Right)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[3], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
            }

        }

        private void SetTextToCyclePrompts()
        {
            if (m_cycleActionPart == CycleActionPart.Negative)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[0], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
            }
            if (m_cycleActionPart == CycleActionPart.Positive)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[1], m_spriteButtonList.tmpSpriteList[(int)m_deviceType]);
            }
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
            //m_inputManager.OnActiveDeviceChange += SetText;
            m_inputManager.BindingsChangedEvent += SetText;
            UnderworldPlayerController.ActiveControllerChanged += OnActiveControllerChanged;
        }
        // Start is called before the first frame update
        void Start()
        {
            PopulateCurrentBinding();
            SetText();

        }

        private void OnEnable()
        {
            DeviceTypeChanged += OnDeviceTypeChanged;
        }

        private void OnDisable()
        {
            DeviceTypeChanged -= OnDeviceTypeChanged;
        }

        private void OnDeviceTypeChanged(CurrentDeviceType type)
        {
            if (deviceType == type)
                return;

            deviceType = type;
            m_activeDeviceBinding.Clear();
            if (m_deviceType == CurrentDeviceType.Keyboard)
            {
                for (int x = 0; x < keyBoardList.Count; x++)
                {
                    m_activeDeviceBinding.Add(keyBoardList[x]);
                }

            }
            else if (m_deviceType == CurrentDeviceType.Gamepad)
            {
                for (int x = 0; x < gamepadList.Count; x++)
                {
                    m_activeDeviceBinding.Add(gamepadList[x]);
                }
            }
            SetText();
        }
    }
}

