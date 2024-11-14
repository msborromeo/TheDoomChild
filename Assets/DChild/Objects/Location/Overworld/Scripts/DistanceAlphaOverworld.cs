using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DistanceAlphaOverworld : MonoBehaviour
{
    [SerializeField]
    private float m_maxAlpha = 1f;
    [SerializeField]
    private float m_minAlpha = 0.3f;
    [SerializeField]
    private float m_fadeSpeed = 2f;
    [SerializeField]
    private bool m_isMultipleObjects;
    [SerializeField, ShowIf("m_isMultipleObjects")]
    private List<SpriteRenderer> m_renderers;
    private SpriteRenderer m_spriteRenderer;
    private bool m_playerNear = false;
    private Color m_color;


    void Start()
    {
        if (!m_isMultipleObjects)
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        else
        {
            for (int i = 0; i < m_renderers.Count; i++)
            {
                m_renderers[i] = m_renderers[i].GetComponent<SpriteRenderer>();
            }
        }
    }

    void Update()
    {
        if (m_playerNear)
        {
            if (m_isMultipleObjects)
            {
                for (int i = 0; i < m_renderers.Count; i++)
                {
                    m_color = m_renderers[i].color;
                    m_color.a = Mathf.Lerp(m_color.a, m_minAlpha, Time.deltaTime * m_fadeSpeed);
                    m_renderers[i].color = m_color;
                }
            }
            else
            {
                m_color = m_spriteRenderer.color;
                m_color.a = Mathf.Lerp(m_color.a, m_minAlpha, Time.deltaTime * m_fadeSpeed);
                m_spriteRenderer.color = m_color;
            }
        }
        else
        {
            if (m_isMultipleObjects)
            {
                for (int i = 0; i < m_renderers.Count; i++)
                {
                    m_color = m_renderers[i].color;
                    m_color.a = Mathf.Lerp(m_color.a, m_maxAlpha, Time.deltaTime * m_fadeSpeed);
                    m_renderers[i].color = m_color;
                }
            }
            else
            {
                m_color = m_spriteRenderer.color;
                m_color.a = Mathf.Lerp(m_color.a, m_maxAlpha, Time.deltaTime * m_fadeSpeed);
                m_spriteRenderer.color = m_color;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            m_playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            m_playerNear = false;
        }
    }
}