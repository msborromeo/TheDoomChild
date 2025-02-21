using DChild.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniLevelTeleport : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> m_teleportPoints;

    private IEnumerator TeleportPlayer()
    {
        while (m_teleportPoints.Count > 0)
        {
            int random = Random.Range(0, m_teleportPoints.Count - 1);
            yield return new WaitForSeconds(3f);
            var player = GameplaySystem.playerManager.player.character;
            player.transform.position = m_teleportPoints[random];
            m_teleportPoints.RemoveAt(random);
            this.gameObject.SetActive(false);
        }

        yield return null;
    }

    void OnEnable()
    {
        if (m_teleportPoints.Count > 0)
        {
            StartCoroutine(TeleportPlayer());
        }
        else
        {
            Debug.LogWarning("Not enough teleport points to start the teleportation process.");
        }
    }
}
