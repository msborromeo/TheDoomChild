using DChild.Gameplay;
using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Inventories.QuickItem;
using Doozy.Runtime.UIManager.Containers;
using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class TheOneThirdFormQuickItemLockdown : MonoBehaviour
{
    [SerializeField]
    private TheOneThirdFormAI m_reference;
    /*    [SerializeField]
        private QuickItemController m_quickItemController;*/
    [SerializeField]
    private InputActionReference m_cycleItemInput;
    [SerializeField]
    private InputActionReference m_usedItemInput;
    [SerializeField]
    private UIContainer m_indicator;
    [SerializeField]
    private int m_duration;
    [SerializeField]
    private bool resetDurationOnRetrigger;

    private bool m_isInLockdown;
    private Transform m_originalParent;

    public void MoveToGameplayCanvas()
    {
        m_indicator.transform.parent = GameplaySystem.gamplayUIHandle.GetReference().m_QuickItems;
        m_indicator.transform.localPosition = Vector3.zero;
    }

    public void RemoveFromGameplayCanvas()
    {
        transform.SetParent(m_originalParent);
    }

    private void OnLockdownTriggered(object sender, EventActionArgs args)
    {
        if (m_isInLockdown)
        {
            if (resetDurationOnRetrigger)
            {
                StopAllCoroutines();
            }
            else
            {
                return;
            }
        }


        StartCoroutine(LockdownRoutine());
    }

    private IEnumerator LockdownRoutine()
    {
        var input = GameplaySystem.playerManager.player.GetComponentInChildren<PlayerInput>();
        var actionMap = input.actions.FindActionMap("Underworld");
        var cycleAction = actionMap.FindAction(m_cycleItemInput.action.id);
        var useItemAction = actionMap.FindAction(m_usedItemInput.action.id);

        m_isInLockdown = true;
        cycleAction.Disable();
        useItemAction.Disable();
        m_indicator?.Show();
        yield return new WaitForSeconds(m_duration);
        m_indicator?.Hide();
        cycleAction.Enable();
        useItemAction.Enable();
        m_isInLockdown = false;
    }

    private void Start()
    {
        //m_quickItemController = FindObjectOfType<QuickItemController>();
        m_reference.LockPlayerQuickItem += OnLockdownTriggered;
        m_originalParent = transform.parent;
    }

    private void OnDisable()
    {
        m_reference.LockPlayerQuickItem -= OnLockdownTriggered;
    }

}
