using DChild;
using DChild.Gameplay;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TheOneMiniLevelLaser : MonoBehaviour
{
    [SerializeField, TabGroup("Laser")]
    private Transform laserStart;
    [SerializeField, TabGroup("Laser")]
    private Transform laserEndEffect;
    [SerializeField, TabGroup("Laser")]
    private LineRenderer lineRenderer;
    [SerializeField, TabGroup("Laser")]
    private LayerMask environmentLayer;
    [SerializeField]
    private EdgeCollider2D m_laserCollider;
    private Coroutine laserCoroutine;
    private Animator m_anim;

    public bool stop;
    [SerializeField]
    private bool m_dynamicRay;
    [SerializeField]
    private bool m_leanDroSkyTown;
    [SerializeField]
    private bool m_noAnticipation;
    public GameObject m_wallMouth;

    [SerializeField]
    private float m_idleLoopValue;
    [SerializeField]
    private float m_attackLoopValue;
    [SerializeField]
    private bool m_forCeilingBlastAttackDPagTandugaStephenTigAGid;
    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }
    public void SetDynamicLaserValue(bool value)
    {
        m_dynamicRay = value;
    }
    private void OnActivate(object sender, EventActionArgs eventArgs)
    {
        laserCoroutine = StartCoroutine(LaserLogic());
        if (m_leanDroSkyTown)
        {
            stop = false;
            StartCoroutine(AnimationHandlerSkyTown());
        }
        else
        {
            StartCoroutine(AnimationHandler());
        }
    }

    private void Start()
    {
        m_wallMouth.GetComponent<WallMouth>().OnActivate += OnActivate;
    }

    private void OnDisable()
    {
        if (laserCoroutine != null)
        {
            StopCoroutine(laserCoroutine);
        }

        m_wallMouth.GetComponent<WallMouth>().OnActivate -= OnActivate;
    }
    [SerializeField, ShowIf("m_dynamicRay"), CustomValueDrawer("PenetrationPower")]
    private float m_penetrationPower;
# if UNITY_EDITOR
    private static float PenetrationPower(float value, GUIContent label)
    {
        return EditorGUILayout.Slider(label, value, 1f, 10000f);
    }
#endif
    private IEnumerator LaserLogic()
    {
        while (!stop)
        {
            Vector3 startPoint = laserStart.position;
            Vector2 direction = laserStart.TransformDirection(Vector2.right);
            Vector3 endPoint = Vector3.zero;

            if (m_dynamicRay)
            {
                List<Vector3> hitPoints = new List<Vector3>();
                RaycastHit2D currentHit = Physics2D.Raycast(startPoint, direction, Mathf.Infinity, environmentLayer);

                for (int i = 0; i < m_penetrationPower && currentHit.collider != null; i++)
                {
                    hitPoints.Add(currentHit.point);
                    Vector3 nextStartPoint = (Vector3)currentHit.point + (Vector3)(direction * 0.01f);
                    startPoint = nextStartPoint;
                    currentHit = Physics2D.Raycast(nextStartPoint, direction, Mathf.Infinity, environmentLayer);
                }

                if (hitPoints.Count > 0)
                {
                    endPoint = hitPoints[hitPoints.Count - 1];
                    UpdateLaser(laserStart.position, endPoint);
                    UpdateEndEffect(endPoint);
                }
                else
                {
                    endPoint = laserStart.position + (Vector3)direction * 100f;
                    UpdateLaser(laserStart.position, endPoint);
                    UpdateEndEffect(endPoint);
                }
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, Mathf.Infinity, environmentLayer);

                if (hit.collider != null)
                {
                    endPoint = hit.point;
                    UpdateLaser(startPoint, endPoint);
                    UpdateEndEffect(endPoint);
                }
                else
                {
                    endPoint = startPoint + (Vector3)direction * 100f;
                    UpdateLaser(startPoint, endPoint);
                    UpdateEndEffect(endPoint);
                }
            }
            UpdateEdgeCollider(laserStart.position, endPoint);
            yield return null;
        }
    }

    private void UpdateEdgeCollider(Vector3 startPoint, Vector3 endPoint)
    {
        if (m_laserCollider != null)
        {
            Vector2 localStartPoint = m_laserCollider.transform.InverseTransformPoint(startPoint);
            Vector2 localEndPoint = m_laserCollider.transform.InverseTransformPoint(endPoint);
            m_laserCollider.points = new Vector2[] { localStartPoint, localEndPoint };
        }
    }

    /*
        private IEnumerator LaserLogic()
        {
            while (!stop)
            {
                Vector3 startPoint = laserStart.position;
                Vector2 direction = laserStart.TransformDirection(Vector2.right);
                RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, Mathf.Infinity, environmentLayer);

                if (hit.collider != null)
                {
                    if (m_dynamicRay)
                    {
                        Collider2D firstCollider = hit.collider;
                        Vector3 nextStartPoint = (Vector3)hit.point + (Vector3)(direction * 0.01f);
                        RaycastHit2D secondHit = Physics2D.Raycast(nextStartPoint, direction, Mathf.Infinity, environmentLayer);

                        while (secondHit.collider == firstCollider)
                        {
                            nextStartPoint = (Vector3)secondHit.point + (Vector3)(direction * 0.01f);
                            secondHit = Physics2D.Raycast(nextStartPoint, direction, Mathf.Infinity, environmentLayer);
                        }

                        if (secondHit.collider != null)
                        {
                            Debug.Log($"Second Hit: {secondHit.collider.name}, Point: {secondHit.point}");
                            Vector3 endPoint = secondHit.point;
                            UpdateLaser(startPoint, secondHit.point);
                            UpdateEndEffect(endPoint);
                        }
                        else
                        {
                            Vector3 endPoint = nextStartPoint + (Vector3)direction * 100f;
                            UpdateLaser(startPoint, endPoint);
                            UpdateEndEffect(endPoint);
                        }
                    }
                    else
                    {
                        Vector3 endPoint = hit.point;
                        UpdateLaser(startPoint, endPoint);
                        UpdateEndEffect(endPoint);
                    }
                }
                else
                {
                    Vector3 endPoint = startPoint + (Vector3)direction * 100f;
                    UpdateLaser(startPoint, endPoint);
                    UpdateEndEffect(endPoint);
                }

                yield return null;
            }
        }*/

    private IEnumerator AnimationHandler()
    {
        while (!stop)
        {
            if (m_noAnticipation)
            {
                //yield return new WaitForSeconds(5f);
                m_anim.SetTrigger("NoAnticipationCeiling");
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(m_idleLoopValue);
                m_anim.SetTrigger("WallMouthBlastAnticipation");
                yield return new WaitForSeconds(m_attackLoopValue);
                m_anim.SetTrigger("TentacleBlastDissipation");
            }
        }
    }
    private IEnumerator AnimationHandlerSkyTown()
    {
        while (!stop)
        {
            if (m_noAnticipation)
            {
                //yield return new WaitForSeconds(5f);
                m_anim.SetTrigger("NoAnticipationCeiling");
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(m_idleLoopValue);
                m_anim.SetTrigger("WallMouthBlastAnticipation");
                if (m_forCeilingBlastAttackDPagTandugaStephenTigAGid)
                {
                     yield return new WaitForSeconds(6f);
                }
                else
                {
                    yield return new WaitForSeconds(m_attackLoopValue);
                }
                
                m_anim.SetTrigger("TentacleBlastDissipation");
                stop = true;
            }
        }
    }

    private void UpdateLaser(Vector3 startPoint, Vector3 endPoint)
    {
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    private void UpdateEndEffect(Vector3 position)
    {
        laserEndEffect.transform.position = position;
    }
    public void ColliderDamageOn()
    {
        Debug.Log("On");
        m_laserCollider.enabled = true;
    }
    public void ColliderDamageOff()
    {
        Debug.Log("Off");
        m_laserCollider.enabled = false;
    }
}
