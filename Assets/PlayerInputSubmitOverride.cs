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

    private InputAction m_preArmyInput;

    InputSystemUIInputModule m_inputModule = (InputSystemUIInputModule)EventSystem.current.currentInputModule;

    void Start()
    { 
        m_preArmyInput = m_inputModule.submit.action;
        m_inputModule.submit.Set(m_actionReference.action);
    }

    private void OnDestroy()
    {
        m_inputModule.submit.Set(m_preArmyInput);
    }
}
