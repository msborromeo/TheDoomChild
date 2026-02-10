using System;
using UnityEngine;

namespace DChild
{
    public class UIMasterCanvas : MonoBehaviour
    {
        private Canvas m_canvas;

        private void Start()
        {
            m_canvas = GetComponent<Canvas>();
            m_canvas.worldCamera = GameSystem.mainCamera;
        }

        private void OnEnable()
        {
            GameSystem.CameraChange += OnCameraChange;
        }

        private void OnDisable()
        {
            GameSystem.CameraChange -= OnCameraChange;
        }

        private void OnCameraChange(object sender, CameraChangeEventArgs eventArgs)
        {
            m_canvas.worldCamera = eventArgs.camera;
            m_canvas.enabled = eventArgs.camera != null;
#if UNITY_EDITOR
            if(eventArgs.camera != null)
            {
                Debug.LogError("MainCamera Component present in the Scene means No UI");
            }
#endif
        }
    }
}