using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Inventories.QuickItem;
using Doozy.Runtime.UIManager.Containers;
using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.Rendering;

public class TheOneThirdFormQuickItemLockdown : MonoBehaviour
{
    [SerializeField]
    private TheOneThirdFormAI m_reference;
    [SerializeField]
    private QuickItemController m_quickItemController;

    [SerializeField]
    private UIContainer m_indicator;
    [SerializeField]
    private int m_duration;
    [SerializeField]
    private bool resetDurationOnRetrigger;

    private bool m_isInLockdown;

    private void MoveIndicatorToUnderworldUI()
    {

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
        m_isInLockdown = true;
        m_quickItemController?.SetEnable(false);
        m_indicator?.Show();
        yield return new WaitForSeconds(m_duration);
        m_indicator?.Hide();
        m_quickItemController?.SetEnable(true);
        m_isInLockdown = false;
    }

    private void Start()
    {
        MoveIndicatorToUnderworldUI();
        m_quickItemController = FindObjectOfType<QuickItemController>();
        m_reference.LockPlayerQuickItem += OnLockdownTriggered;
    }

    
}
