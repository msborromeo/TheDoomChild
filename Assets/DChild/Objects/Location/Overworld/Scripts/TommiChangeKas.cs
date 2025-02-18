
using DChild.Gameplay;
using DChild.Gameplay.Characters.Players;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class TommiChangeKas : MonoBehaviour
{
    [SerializeField, ShowIf("m_isTotoSkyTown")]
    private bool m_isTotoJakRC;

    [SerializeField, ShowIf("m_isTotoJakRC")]
    private bool m_youLoveTommi;

    [SerializeField, ShowIf("m_isTotoSkyTown")]
    public bool m_isTotoSkyTown;
    [SerializeField, ShowIf("m_youLoveTommi")]
    private int m_zeeSortingLayerBefore;
    [SerializeField, ShowIf("m_youLoveTommi")]
    private int m_zeeSortingLayerAfter;
    [SerializeField, ShowIf("m_youLoveTommi")]
    private List<SpriteRenderer> m_renderers;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            for (int i = 0; i < m_renderers.Count; i++)
            {
                m_renderers[i].sortingOrder = m_zeeSortingLayerAfter;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            for (int i = 0; i < m_renderers.Count; i++)
            {
                m_renderers[i].sortingOrder = m_zeeSortingLayerBefore;
            }
        }
    }
}

//collision.CompareTag("Sensor") && collision.gameObject.layer == 8