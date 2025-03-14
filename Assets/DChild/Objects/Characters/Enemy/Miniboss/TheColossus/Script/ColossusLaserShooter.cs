using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static PixelCrushers.DialogueSystem.UnityGUI.GUIProgressBar;

namespace DChild.Gameplay.Characters.Enemies
{
    public class ColossusLaserShooter : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer m_lineRenderer;
        [SerializeField]
        private ParticleFX m_impactFX;
        [SerializeField]
        private ParticleFX m_chargeFX;
        [SerializeField]
        private LayerMask m_collideWithLayers;
        [SerializeField]
        private float m_maxLaserDistance = 300f;
        [SerializeField]
        private List<Collider2D> m_toIgnore = new List<Collider2D>();
        [SerializeField]
        private EdgeCollider2D m_edgeCollider;
        [SerializeField, TabGroup("Laser Start Positions")]
        private Transform m_laserLeftStartPosition;
        [SerializeField, TabGroup("Laser Start Positions")]
        private Transform m_laserRightStartPosition;

        private RaycastHit2D[] hitBuffers = new RaycastHit2D[16];

        private List<Vector2> m_edgeColliderPoints = new List<Vector2>()
        {
            Vector2.zero,
            Vector2.right
        };


        private void Start()
        {
            m_impactFX.Stop();
            m_chargeFX.Stop();
            m_lineRenderer.enabled = false;
            m_edgeCollider.enabled = false;
        }

        public void FireLaser(bool isLaserClockwise, float laserDuration)
        {
            StartCoroutine(FireLaserRoutine(isLaserClockwise, laserDuration));
        }

        private IEnumerator FireLaserRoutine(bool isLaserClockwise, float laserDuration)
        {
            Vector2 direction = new Vector2();
            RaycastHit2D[] hit = new RaycastHit2D[16];
            var laserStartPos = isLaserClockwise ? m_laserRightStartPosition.position : m_laserLeftStartPosition.position;

            float laserInitialTimer = 2f;

            var speed = 360 / laserDuration;

            m_lineRenderer.enabled = true;
            m_lineRenderer.SetPosition(0, transform.position);

            m_edgeCollider.enabled = true;

            m_chargeFX.Play();

            direction = laserStartPos - transform.position;
            transform.right = direction.normalized;

            while (laserInitialTimer > 0)
            {
                UpdateLaser(transform.position, transform.right, m_maxLaserDistance);

                laserInitialTimer -= Time.deltaTime;
                yield return null;
            }

            while (laserDuration > 0)
            {
                //rotate laser here
                //Use Lerp? rotate within laser duration
                if (isLaserClockwise)
                {
                    transform.Rotate(Vector3.forward, speed * Time.deltaTime);
                }
                else
                {
                    transform.Rotate(Vector3.forward, -speed * Time.deltaTime);
                }

                UpdateLaser(transform.position, transform.right, m_maxLaserDistance);

                laserDuration -= Time.deltaTime;
                yield return null;
            }

            m_edgeCollider.enabled = false;
            m_lineRenderer.enabled = false;
            m_chargeFX.Stop();
            m_impactFX.Stop();

            yield return null;
        }


        private RaycastHit2D RaycastLaser(Vector2 origin, Vector2 direction, float distance, int layerMask)
        {
            List<Collider2D> toIgnore = m_toIgnore;

            hitBuffers = Physics2D.RaycastAll(origin, direction, distance, layerMask);

            m_impactFX.Play();

            for (int i = 0; i < hitBuffers.Length; i++)
            {
                var hitBuffer = hitBuffers[i];
                if (toIgnore.Contains(hitBuffer.collider))
                    continue;

                return hitBuffer;
            }

            Debug.Log("Laser saw nothing");
            return new RaycastHit2D();
        }

        private void UpdateLaser(Vector2 origin, Vector2 direction, float distance)
        {
            var hitBuffer = RaycastLaser(origin, direction, distance, m_collideWithLayers);
            if (hitBuffer.collider == null)
            {
                Debug.Log("Laser is Too Short");

                var point = origin + (direction * distance);
                SetLaserEndPoint(point);
                m_edgeColliderPoints[1] = Vector2.right * distance;
                m_edgeCollider.SetPoints(m_edgeColliderPoints);
                return;
            }

            var edgeDistance = Vector2.Distance(origin, hitBuffer.point);
            m_edgeColliderPoints[1] = Vector2.right * edgeDistance;
            m_edgeCollider.SetPoints(m_edgeColliderPoints);

            SetLaserEndPoint(hitBuffer.point);
        }

        private void SetLaserEndPoint(Vector2 point)
        {
            m_lineRenderer.SetPosition(1, point);
            m_impactFX.transform.position = point;
        }
    }
}

