using DChild.Menu;
using Doozy.Runtime.Signals;
using System.Collections;
using System.Collections.Generic;
using DChild.Gameplay;
using DChild.Gameplay.Systems;
using UnityEngine;

public class PauseGameGuard : MonoBehaviour
{
    [SerializeField]
    private SignalSender m_pauseGameSignal;

    [SerializeField]
    private bool m_UITransitionInProgress;

    public void SetUITransitionInProgress(bool IsInProgress)
    {
        m_UITransitionInProgress = IsInProgress;
    }
    
    public void CanPauseGame()
    {
        if (GameplaySystem.GetCurrentWorldType() == WorldType.ArmyBattle) return;
        if (LoadingHandle.isLoading) return;
        if(m_UITransitionInProgress) return;
        
        m_pauseGameSignal.SendSignal();
    }
}
