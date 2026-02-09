using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holysoft.Event;
using DChild.Menu;
using DChild.Gameplay.Systems;
using System;
using DChild;

public class OverworldvCamInitializer : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_vCam;

    private void Awake()
    {
        //m_vCam.enabled = true;
        //    LoadingHandle.LoadingDone += OnLoadingDone;
        OverworldGameplaySystem.SetupVCam += OnSetUpVCam;
    }

    private void OnDisable()
    {
        //LoadingHandle.LoadingDone -= OnLoadingDone;
        OverworldGameplaySystem.SetupVCam -= OnSetUpVCam;
    }

    private void OnLoadingDone(object sender, EventActionArgs eventArgs)
    {
        m_vCam.enabled = true;
    }

    private void OnSetUpVCam()
    {
        m_vCam.enabled = true;
    }
}