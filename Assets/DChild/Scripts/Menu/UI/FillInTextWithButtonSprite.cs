using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public static class FillInTextWithButtonSprite
{

    private static string ACTION_PATTERN = @"\{(.*?:)\}";
    private static Regex REGEX = new Regex(ACTION_PATTERN, RegexOptions.IgnoreCase);

    //public static string ReadAndReplaceBinding(string textToDisplay, InputBinding actionSelected, TMP_SpriteAsset spriteAsset)
    //{
    //    string buttonName = actionSelected.effectivePath;
    //    Debug.Log("Action: " + actionSelected.action);
    //    buttonName = RenameInput(buttonName, actionSelected.action);
    //    Debug.Log("Button" + buttonName);
    //    textToDisplay = textToDisplay.Replace("BUTTONPROMPT", $"<sprite=\"{spriteAsset.name}\" name=\"{buttonName}\">");
    //    //textToDisplay = textToDisplay.Replace("tab", $"<sprite=\"spritesheet\"name=\"Keyboard_Tab\">");

    //    return textToDisplay;

    //}

    public static string ReadAndReplaceBinding(string textToDisplay, InputBinding actionNeeded,
           TMP_SpriteAsset spriteAsset)
    {
        //Different from christina's in that we are going to use the effective path
        // ToString previously would just be "Keyboard/f" yet
        // newer versions of input system have it "Keyboard/f[Keyboard]"
        string stringButtonName = actionNeeded.effectivePath;

        stringButtonName = RenameInput(stringButtonName, actionNeeded.action);



        textToDisplay = textToDisplay.Replace("BUTTONPROMPT", $"<sprite=\"{spriteAsset.name}\" name=\"{stringButtonName}\">");

        return textToDisplay;
    }

    public static string ReadAndReplaceCompositeBinding(string textToDisplay, InputBinding actionNeededModifier, InputBinding actionNeeded,
           TMP_SpriteAsset spriteAsset)
    {
        //Different from christina's in that we are going to use the effective path
        // ToString previously would just be "Keyboard/f" yet
        // newer versions of input system have it "Keyboard/f[Keyboard]"
        string stringButtonName = actionNeeded.effectivePath;
        string stringModifierName = actionNeededModifier.effectivePath;

        stringModifierName = RenameInput(stringModifierName, actionNeededModifier.action);
        stringButtonName = RenameInput(stringButtonName, actionNeeded.action);


        textToDisplay = textToDisplay.Replace("BUTTONMODIFIER", $"<sprite=\"{spriteAsset.name}\" name=\"{stringModifierName}\">");
        textToDisplay = textToDisplay.Replace( "BUTTONPROMPT", $"<sprite=\"{spriteAsset.name}\" name=\"{stringButtonName}\">");

        return textToDisplay;
    }

    public static string ReadAndReplaceCompositeBinding(string textToDisplay, InputBinding actionNeededModifier1, InputBinding actionNeededModifier2, InputBinding actionNeeded,
         TMP_SpriteAsset spriteAsset)
    {
        //Different from christina's in that we are going to use the effective path
        // ToString previously would just be "Keyboard/f" yet
        // newer versions of input system have it "Keyboard/f[Keyboard]"
        string stringButtonName = actionNeeded.effectivePath;
        string stringModifierName1 = actionNeededModifier1.effectivePath;
        string stringModifierName2 = actionNeededModifier2.effectivePath;

        stringModifierName1 = RenameInput(stringModifierName1, actionNeededModifier1.action);
        stringModifierName2 = RenameInput(stringModifierName2, actionNeededModifier2.action);
        stringButtonName = RenameInput(stringButtonName, actionNeeded.action);



        textToDisplay = textToDisplay.Replace("BUTTONMODIFIER1", $"<sprite=\"{spriteAsset.name}\" name=\"{stringModifierName1}\">");
        textToDisplay = textToDisplay.Replace("BUTTONMODIFIER2", $"<sprite=\"{spriteAsset.name}\" name=\"{stringModifierName2}\">");
        textToDisplay = textToDisplay.Replace("BUTTONPROMPT", $"<sprite=\"{spriteAsset.name}\" name=\"{stringButtonName}\">");

        return textToDisplay;
    }


    public static string ReplaceBindings(string textWithActions, CurrentDeviceType deviceType, InputManager inputManager,
            SpriteButtonIconListObject spriteAssets)
    {
        MatchCollection matches = REGEX.Matches(textWithActions);

        // original template
        var replacedText = textWithActions;

        foreach (Match match in matches)
        {
            var withBraces = match.Groups[0].Captures[0].Value;
            var innerPart = match.Groups[1].Captures[0].Value;

            var tagText = GetSpriteTag(innerPart, deviceType, inputManager, spriteAssets);

            replacedText = replacedText.Replace(withBraces, tagText);
        }

        return replacedText;
    }

    public static string ReplaceActiveBindings(string textWithActions, InputManager inputManager,
        SpriteButtonIconListObject spriteAssets)
    {
        return ReplaceBindings(textWithActions, inputManager.GetCurrentDevice(), inputManager, spriteAssets);
    }

    public static string GetSpriteTag(string actionName, CurrentDeviceType deviceType, InputManager inputManager,
            SpriteButtonIconListObject spriteAssets)
    {
        InputBinding dynamicBinding = inputManager.GetBinding(actionName, deviceType);
        TMP_SpriteAsset spriteAsset = spriteAssets.tmpSpriteList[(int)deviceType];
        string stringButtonName = dynamicBinding.effectivePath;
        stringButtonName = RenameInput(stringButtonName,actionName);

        return $"<sprite=\"{spriteAsset.name}\" name=\"{stringButtonName}\">";
    }

    private static string RenameInput(string buttonName, string actioName)
    {
        buttonName = buttonName.Replace(actioName, string.Empty);

        buttonName = buttonName.Replace("<Keyboard>/", "Keyboard_");

        buttonName = buttonName.Replace("<Gamepad>/", "Gamepad_");

        buttonName = buttonName.Replace("<PS4>/", "PS4_");

        buttonName = buttonName.Replace("<Modifier>/", "GamePad_");

        buttonName = buttonName.Replace("<OneModifier>/", "GamePad_");

        buttonName = buttonName.Replace("<OneModifier>/", "Keyboard_");

        return buttonName;
    }
}
