using DChild.Gameplay;
using DChild.Gameplay.Characters.AI;
using DChild.Gameplay.Characters.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniLevelTeleport : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> m_teleportPoints;
    [SerializeField]
    private TheOneThirdFormAI m_thirdForm;
    [SerializeField]
    private int teleportationsDone;
    private int teleportCount;
    [SerializeField]
    private Vector2 m_thirdFormLocation;
    [SerializeField]
    private Vector2 m_bossArenaLoc;

    private IEnumerator TeleportPlayer()
    {
        var player = GameplaySystem.playerManager.player.character;
        var phase = m_thirdForm.phaseHandle.currentPhase;

        switch (phase)
        {
            case TheOneThirdFormAI.Phase.PhaseThree:
                teleportCount = 3;
                break;
            case TheOneThirdFormAI.Phase.PhaseFour:
                teleportCount = 4;
                break;
            case TheOneThirdFormAI.Phase.PhaseFive:
                teleportCount = 2;
                break;
        }
        int randomIndex = (phase == TheOneThirdFormAI.Phase.PhaseFour) ? 0 :
            Random.Range(0, m_teleportPoints.Count - 1);
        while (teleportationsDone <= 10)
        {
            if (m_teleportPoints.Count == 0)
            {
                break;
            }

            yield return new WaitForSeconds(3f);
            if (phase == TheOneThirdFormAI.Phase.PhaseThree && teleportationsDone != 3)
            {
                player.transform.position = m_teleportPoints[randomIndex];
                m_teleportPoints.RemoveAt(randomIndex);
            }
            else if (phase == TheOneThirdFormAI.Phase.PhaseFour && teleportationsDone != 7)
            {
                if (m_teleportPoints.Count == 1)
                {
                    player.transform.position = m_teleportPoints[0];

                }
                else
                {
                    player.transform.position = m_teleportPoints[randomIndex];
                    m_teleportPoints.RemoveAt(randomIndex);
                }
            }
            else if (phase == TheOneThirdFormAI.Phase.PhaseFive && teleportationsDone != 9)
            {
                player.transform.position = m_bossArenaLoc;
            }
            else
            {
                m_thirdForm.m_isPlayerBackArena = true;
                player.transform.position = m_thirdFormLocation;
            }
            this.gameObject.SetActive(false);
            
        }
    }

    void OnEnable()
    {
        if (m_teleportPoints.Count > 0)
        {
            teleportationsDone++;
            StartCoroutine(TeleportPlayer());
        }
        else
        {
            Debug.LogWarning("Not enough teleport points to start the teleportation process.");
        }
    }
}
