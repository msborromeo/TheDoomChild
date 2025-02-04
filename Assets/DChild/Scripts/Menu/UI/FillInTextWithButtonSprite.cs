using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public static class FillInTextWithButtonSprite
{
    public static string ReadAndReplaceBinding(string textToDisplay, InputBinding actionSelected, TMP_SpriteAsset spriteAsset)
    {
        string buttonName = actionSelected.ToString();
        Debug.Log("Action: " + actionSelected.action);
        buttonName = RenameInput(buttonName, actionSelected.action);
        Debug.Log("Button"+buttonName);
        textToDisplay = textToDisplay.Replace("[ButtonPrompt]", $"<sprite=\"{spriteAsset.name}\" name=\"{buttonName}\">");
        //textToDisplay = textToDisplay.Replace("tab", $"<sprite=\"spritesheet\"name=\"Keyboard_Tab\">");

        return textToDisplay;

    }

    private static string RenameInput(string buttonName, string actioName)
    {
        buttonName = buttonName.Replace(actioName, string.Empty);

        buttonName = buttonName.Replace("<Keyboard>/", "Keyboard_");

        buttonName = buttonName.Replace("[Keyboard]", "");

        buttonName = buttonName.Replace(":", "");



        buttonName = buttonName.Replace("<Gamepad>/", "Gamepad_");

        buttonName = buttonName.Replace("[Gamepad]", "");


        buttonName = buttonName.Replace("<PS4>/", "PS4_");

        buttonName = buttonName.Replace("[PS4]", "");


        return buttonName;
    }
}
