using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldvCamInitializer : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_vCam;

    private void Awake()
    {
        m_vCam.enabled=true;
    }

    private void Start()
    {
        m_vCam.enabled = true;
    }
}
