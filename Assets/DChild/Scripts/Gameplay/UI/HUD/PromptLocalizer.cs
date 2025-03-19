using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Localization;
using I2.Loc;

[RequireComponent(typeof(IPromptLocalizer))]
public class PromptLocalizer : MonoBehaviour
{

    private IPromptLocalizer m_Injector;

    [SerializeField]
    private Localize ValidPrompt;

    [SerializeField]
    private Localize InvalidPrompt;

    private void Awake()
    {
        m_Injector = GetComponent<IPromptLocalizer>();
        m_Injector.LocalizeText += onUpdate;
    }

    void onUpdate(string text)
    {
        ValidPrompt?.SetTerm("ActionPrompts/" + text);
        InvalidPrompt?.SetTerm("ActionPrompts/" + text);
    }

    private void OnDestroy()
    {
        m_Injector.LocalizeText -= onUpdate;
    }
}
