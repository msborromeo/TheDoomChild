using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holysoft.Event;
using DChild.Menu;

public class OverworldvCamInitializer : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_vCam;

    private void Awake()
    {
        LoadingHandle.LoadingDone += OnLoadingDone;
    }

    private void OnDestroy()
    {
        LoadingHandle.LoadingDone -= OnLoadingDone;
    }

    private void OnLoadingDone(object sender, EventActionArgs eventArgs)
    {
        m_vCam.enabled = true;
    }
}