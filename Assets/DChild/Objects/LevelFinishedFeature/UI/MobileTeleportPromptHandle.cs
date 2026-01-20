using DChild.Gameplay.Systems;
using DChild.UI;
using DChildDebug.Cutscene;
using Doozy.Runtime.UIManager.Containers;
using Holysoft.Collections;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace DChild.Gameplay.LevelFinish.UI
{

    public class MobileTeleportPromptHandle : MonoBehaviour
    {
        [SerializeField] private DialogueSkipProgressionUI m_ui;
        [SerializeField] private float m_holdButtonDuration;
        [SerializeField] private TextMeshProUGUI m_targetLocation;

        private InputAction m_currentInput;
        private UIContainer m_view;
        private float m_holdButtonDurationTimer;

        private bool m_overworldAvailable;
        private bool m_throneRoomAvailable;

        public void Reset()
        {
            m_holdButtonDurationTimer = 0;
            m_ui.SetProgression(0);
        }

        public void SetCurrentInput(InputAction value) => m_currentInput = value;

        public void HidePrompt()
        {
            m_view.Hide();
            StopAllCoroutines();
            Reset();
        }

        public void SetupLocationPrompt(InputAction action)
        {
            bool locationUnlocked;
            switch (action.name)
            {
                case "OverworldTeleport":
                    locationUnlocked = UnderworldGameplaySystem.overworldTeleportHandle.CanTeleportToOverworld();
                    break;
                case "MordenThroneRoomTeleport":
                    locationUnlocked = UnderworldGameplaySystem.overworldTeleportHandle.CanTeleportToThroneRoom();
                    break;
                default:
                    locationUnlocked = false;
                    return;
            }

            ShowPrompt(locationUnlocked);
        }

        private void ShowPrompt(bool locationUnlocked)
        {
//EDITOR ONLY; TO BE REMOVED
#if UNITY_EDITOR
            if (UnderworldGameplaySystem.overworldTeleportHandle.allowTeleport)
            {
                StopAllCoroutines();
                StartCoroutine(TeleportRoutine());
                return;
            }
#endif

            if (!locationUnlocked)
                return;

            StopAllCoroutines();
            StartCoroutine(TeleportRoutine());
        }

        public IEnumerator TeleportRoutine()
        {
            m_view.Show();

            Debug.Log("Teleport Routine Start");
            Reset();
            yield return null;
            do
            {
                m_holdButtonDurationTimer += Time.unscaledDeltaTime;
                m_ui.SetProgression(m_holdButtonDurationTimer / m_holdButtonDuration);
                yield return null;
            } while (m_holdButtonDurationTimer < m_holdButtonDuration);

            m_view.Hide();
        }

        private void Awake()
        {
            m_view = GetComponent<UIContainer>();
            m_view.Hide();
        }
    }
}