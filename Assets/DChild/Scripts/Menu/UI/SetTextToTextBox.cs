using DChild.Gameplay.Characters.Players.Modules;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

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
        InputAction m_action;

        [SerializeField]
        private float m_promptFontSize = 7;

        private List<InputBinding> currentBinding = new List<InputBinding>();
        private List<InputBinding> m_activeDeviceBinding = new List<InputBinding>();

        private TMP_Text m_textbox;

        public static event Action<CurrentDeviceType> DeviceTypeChanged;

        private List<InputBinding> keyBoardList = new List<InputBinding>();
        private List<InputBinding> gamepadList = new List<InputBinding>();
        private List<InputBinding> psList = new List<InputBinding>();

        [SerializeField, MinValue(1), MaxValue(4)]
        private int m_numberOfActions = 1;

        [SerializeField]
        private InputActionConfiguration m_actionConfiguration1;
        [SerializeField, ShowIf("@m_numberOfActions > 1")]
        private InputActionConfiguration m_actionConfiguration2;
        [SerializeField, ShowIf("@m_numberOfActions > 2")]
        private InputActionConfiguration m_actionConfiguration3;
        [SerializeField, ShowIf("@m_numberOfActions > 3")]
        private InputActionConfiguration m_actionConfiguration4;

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
            if ((int)m_deviceType > m_spriteButtonList.tmpSpriteList.Count - 1)
            {
                return;
            }

            switch (m_numberOfActions)
            {
                case 1:
                    {
                        var case1Action = GetInputBinding(m_actionConfiguration1);
                        m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message,
                            case1Action, 
                            m_spriteButtonList.tmpSpriteList[(int)m_deviceType], 
                            m_promptFontSize);
                    }
                    break;
                case 2:
                    var case2Action1 = GetInputBinding(m_actionConfiguration1);
                    var case2Action2 = GetInputBinding(m_actionConfiguration2);
                    m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message,
                        case2Action1,
                        case2Action2,
                        m_spriteButtonList.tmpSpriteList[(int)m_deviceType],
                        m_promptFontSize);
                    break;
                case 3:
                    var case3Action1 = GetInputBinding(m_actionConfiguration1);
                    var case3Action2 = GetInputBinding(m_actionConfiguration2);
                    var case3Action3 = GetInputBinding(m_actionConfiguration3);
                    m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message,
                        case3Action1,
                        case3Action2,
                        case3Action3,
                        m_spriteButtonList.tmpSpriteList[(int)m_deviceType],
                        m_promptFontSize);
                    break;
                case 4:
                    break;
            }

            //if (m_actionConfiguration1.inputAction.action.bindings.Count > (int)CurrentDeviceType._COUNT)
            //{

            //    switch (m_actionConfiguration1.actionType)
            //    {
            //        case InputActionConfiguration.InputActionType.Cycle:
            //            SetTextToCyclePrompts();
            //            break;
            //        case InputActionConfiguration.InputActionType.Modifier:
            //            SetTextToModifierPrompts();
            //            break;
            //        case InputActionConfiguration.InputActionType.Directional:
            //            SetTextToDirectionalPrompts();
            //            break;
            //        case InputActionConfiguration.InputActionType.NoneComposite:
            //           m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_actionConfiguration1.inputAction.action.bindings[(int)m_deviceType], m_spriteButtonList.tmpSpriteList[(int)m_deviceType], m_promptFontSize);
            //            break;
            //    }
            //}
            //else
            //{
            //    m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_actionConfiguration1.inputAction.action.bindings[(int)m_deviceType], m_spriteButtonList.tmpSpriteList[(int)m_deviceType], m_promptFontSize);
            //}
        }

        //intended to be used in UI Managers that have dynamic text through data like Primary Skills

        //intended to be used on text localized
        public void SetText(string localizedText)
        {
            m_message = localizedText;
            SetText();
        }
        public void SetText(string text, InputActionConfiguration configuration1)
        {
            m_message = text;
            m_numberOfActions = 1;
            m_actionConfiguration1 = configuration1;
            currentBinding.Clear();
            PopulateCurrentBinding(m_actionConfiguration1);
            AddCurrentBindings();
            SetText();
        }

        public void SetText(string text, InputActionConfiguration configuration1,  InputActionConfiguration configuration2)
        {
            m_message = text;
            m_numberOfActions = 2;
            m_actionConfiguration1 = configuration1;
            currentBinding.Clear();
            PopulateCurrentBinding(m_actionConfiguration1);

            m_actionConfiguration2 = configuration2;
            PopulateCurrentBinding(m_actionConfiguration2);

            AddCurrentBindings();

            SetText();
        }

        public void SetText(string text, InputActionConfiguration configuration1, InputActionConfiguration configuration2, InputActionConfiguration configuration3)
        {
            m_message = text;
            m_numberOfActions = 3;
            m_actionConfiguration1 = configuration1;
            m_actionConfiguration2 = configuration2;
            m_actionConfiguration3 = configuration3;
            SetText();
        }

        private InputBinding GetInputBinding(InputActionConfiguration configuration)
        {
            InputBinding inputBinding = new InputBinding(); //default
            switch(configuration.actionType)
            {
                case InputActionConfiguration.InputActionType.Directional:
                    {
                        switch(configuration.directionPart)
                        {
                            case InputActionConfiguration.DirectionActionPart.Up:
                                {
                                    inputBinding = configuration.inputAction.action.bindings[1];
                                }
                                break;
                            case InputActionConfiguration.DirectionActionPart.Down:
                                {
                                    inputBinding = configuration.inputAction.action.bindings[2];
                                }
                                break;
                            case InputActionConfiguration.DirectionActionPart.Left:
                                {
                                    inputBinding = configuration.inputAction.action.bindings[3];
                                }
                                break;
                            case InputActionConfiguration.DirectionActionPart.Right:
                                {
                                    inputBinding = configuration.inputAction.action.bindings[4];
                                }
                                break;
                        }
                    }
                    break;
                case InputActionConfiguration.InputActionType.Cycle:
                    switch (configuration.cycleActionPart)
                    {
                        case InputActionConfiguration.CycleActionPart.Negative:
                            {
                                inputBinding = configuration.inputAction.action.bindings[1];
                            }
                            break;
                        case InputActionConfiguration.CycleActionPart.Positive:
                            {
                                inputBinding = configuration.inputAction.action.bindings[2];
                            }
                            break;
                    }
                    break;
                case InputActionConfiguration.InputActionType.Modifier:
                    {
                        inputBinding = m_actionConfiguration1.inputAction.action.bindings[1];
                    }
                    break;
                case InputActionConfiguration.InputActionType.NoneComposite:
                    inputBinding = m_actionConfiguration1.inputAction.action.bindings[(int)m_deviceType];
                    break;
            }
            return inputBinding;
        }

        private void PopulateCurrentBinding(InputActionConfiguration configuration)
        {
            var inputBinding = configuration.inputAction.action.bindings;

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
        }

        private void AddCurrentBindings()
        {
            for (int x = 0; x < currentBinding.Count; x++)
            {
                var curBind = currentBinding[x];
                Debug.Log("Current Binding Effective Path: " + curBind.effectivePath);
                if (curBind.effectivePath.Contains("Keyboard"))
                {
                    keyBoardList.Add(curBind);
                }
                if (curBind.effectivePath.Contains("Gamepad"))
                {
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
            if (m_actionConfiguration1.directionPart == InputActionConfiguration.DirectionActionPart.Up)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[0], m_spriteButtonList.tmpSpriteList[(int)m_deviceType], m_promptFontSize);
            }
            if (m_actionConfiguration1.directionPart == InputActionConfiguration.DirectionActionPart.Down)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[1], m_spriteButtonList.tmpSpriteList[(int)m_deviceType], m_promptFontSize);
            }

            if (m_actionConfiguration1.directionPart == InputActionConfiguration.DirectionActionPart.Left)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[2], m_spriteButtonList.tmpSpriteList[(int)m_deviceType], m_promptFontSize);
            }
            if (m_actionConfiguration1.directionPart == InputActionConfiguration.DirectionActionPart.Right)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[3], m_spriteButtonList.tmpSpriteList[(int)m_deviceType], m_promptFontSize);
            }

        }

        private void SetTextToCyclePrompts()
        {
            if (m_actionConfiguration1.cycleActionPart == InputActionConfiguration.CycleActionPart.Negative)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[0], m_spriteButtonList.tmpSpriteList[(int)m_deviceType], m_promptFontSize);
            }
            if (m_actionConfiguration1.cycleActionPart == InputActionConfiguration.CycleActionPart.Positive)
            {
                m_textbox.text = FillInTextWithButtonSprite.ReadAndReplaceBinding(m_message, m_activeDeviceBinding[1], m_spriteButtonList.tmpSpriteList[(int)m_deviceType], m_promptFontSize);
            }
        }

        private void OnDestroy()
        {
            m_inputManager.OnActiveDeviceChange -= SetText;
            m_inputManager.BindingsChangedEvent -= SetText;
            UnderworldPlayerController.ActiveControllerChanged -= OnActiveControllerChanged;
            OverWorldPlayerController.ActiveControllerChanged -= OnActiveControllerChanged;
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
            OverWorldPlayerController.ActiveControllerChanged += OnActiveControllerChanged;
        }
        // Start is called before the first frame update
        void Start()
        {
            PopulateCurrentBinding(m_actionConfiguration1);
            AddCurrentBindings();
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

