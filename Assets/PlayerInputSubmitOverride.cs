using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerInputSubmitOverride : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_actionReference;

    private InputActionReference m_preArmyInput;

    InputSystemUIInputModule m_inputModule => (InputSystemUIInputModule)EventSystem.current.currentInputModule;

    private void Awake() => m_preArmyInput = m_inputModule.submit;

    public void OverrideInput() => m_inputModule.submit = m_actionReference;

    private void OnDestroy() => m_inputModule.submit = m_preArmyInput;
}
