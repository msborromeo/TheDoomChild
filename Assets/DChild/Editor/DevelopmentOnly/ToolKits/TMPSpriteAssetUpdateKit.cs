using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


namespace DChild.Serialization
{
#if UNITY_EDITOR
    public class TMPSpriteAssetUpdateKit : OdinEditorWindow
    {
        [SerializeField]
        private CurrentDeviceType currentDeviceType;
        [SerializeField]
        private TMP_SpriteAsset m_spriteAsset;
        private string[] m_mainValues;
        [SerializeField]
        private List<string> m_glyphName = new List<string>();


        [MenuItem("Tools/Kit/TMPSpriteAssetUpdateKit")]


        private static void ShowWindow()
        {
            var window = GetWindow<TMPSpriteAssetUpdateKit>(false, "TMPSprite Update Kit", true);
        }

        [Button]
        public void PopulateList()
        {
            m_glyphName.Clear();

     

            string[] iniGamepad = new string[]{
                "dpad/left","dpad/right","dpad/up","dpad/down",
                "leftStick/left", "leftStick/right","leftStick/up","leftStick/down",
                "rightStick/left", "rightStick/right","rightStick/up","rightStick/down",
                "buttonNorth","buttonWest","buttonSouth","buttonEast",
                "leftTrigger","leftShoulder","rightTrigger","rightShoulder",
                "menu","leftStickPress","rightStickPress","select","start"
            };
            string[] iniPs4 = new string[] {"dpad/left","dpad/right","dpad/up","dpad/down",
                "leftStick/left", "leftStick/right","leftStick/up","leftStick/down",
                "rightStick/left", "rightStick/right","rightStick/up","rightStick/down",
                "triangle","square","cross","circle",
                "leftTrigger","leftShoulder","rightTrigger","rightShoulder",
                "menu","leftStick/press","rightStick/press","select","start" };
            string[] iniAlpha = new string[]
          { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
        "1","2","3","4","5","6","7","8","9","0",
        "numpad1","numpad2","numpad3","numpad4","numpad5","numpad6","numpad7","numpad8","numpad9","numpad0",
        "`","dash","equals","leftBracket","rightBracket","semicolon","quote","backslash","comma","period","slash","numpad.","numpad/","numpad*","numpad-","numpad+",
        "f1","f2","f3","f4","f5","f6","f7","f8","f9","f10","f11","f12",
        "escape","scrollL   ock","pause/break","insert","home","pageUp","delete","end","pagedown","upArrow","leftArrow","downArrow","rightArrow","numlock",
        "enter","backSpace","control","tab","leftButton","rightButton","shift","space","leftAlt","capsLock","middleButton"
          };

                

            switch (currentDeviceType)
            {
                case CurrentDeviceType.Keyboard:
                    m_mainValues = iniAlpha;     
                    break;
                case CurrentDeviceType.Gamepad:
                    m_mainValues = iniGamepad;
                    break;
                case CurrentDeviceType.PS4:
                    m_mainValues = iniPs4;
                    break;
            }
            foreach(var item in m_spriteAsset.spriteGlyphTable)
            {
                m_glyphName.Add(item.sprite.name);
            }

        }

        [Button]
        public void RenameGlyph()
        {

            for (int x = 0; x < m_glyphName.Count; x++)
            {
                m_glyphName[x] = $"{currentDeviceType}_{m_mainValues[x]}";
            }
        }
        [Button]
        public void UpdateGlyph()
        {
            for (int x = 0; x < m_glyphName.Count; x++)
            {
                m_spriteAsset.spriteCharacterTable[x].name = m_glyphName[x];
            }
        }


    }
#endif
}

