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
    
    public void CanPauseGame()
    {
        if (GameplaySystem.GetCurrentWorldType() == WorldType.ArmyBattle) return;
        if (LoadingHandle.isLoading) return;
        
        m_pauseGameSignal.SendSignal();
    }
}
