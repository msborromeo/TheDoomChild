using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace DChild.Gameplay.Characters.Enemies
{
    public class KingPusGrapplerHandle : MonoBehaviour
    {
        [SerializeField]
        private KingPusGrappler[] m_grapplers;

        public KingPusGrappler[] GetGrapplers() => m_grapplers;

        public IEnumerator RetractRoutine(float speed)
        {
            for (int i = 0; i < m_grapplers.Length; i++)
            {
                m_grapplers[i].Retract(speed);
            }

            bool hasExtendedGrappler = true;
            do
            {
                hasExtendedGrappler = false;
                for (int i = 0; i < m_grapplers.Length; i++)
                {
                    if (m_grapplers[i].isExtended)
                    {
                        hasExtendedGrappler = true;
                        break;
                    }
                }

                yield return null;

            } while (hasExtendedGrappler);
        }

        public IEnumerator ExtendRoutine(float speed, int tentacleCount, bool activatePhysicsAtEnd = false)
        {
            if (tentacleCount < 0 || tentacleCount >= m_grapplers.Length)
                yield break;

            for (int i = 0; i < tentacleCount; i++)
            {
                m_grapplers[i].Extend(speed, activatePhysicsAtEnd);
            }

            bool hasExtendedGrappler = true;
            do
            {
                hasExtendedGrappler = false;
                for (int i = 0; i < tentacleCount; i++)
                {
                    if (m_grapplers[i].isExtended)
                    {
                        hasExtendedGrappler = true;
                        break;
                    }
                }

                yield return null;

            } while (hasExtendedGrappler);
        }

        public void OverrideIKs(Vector3[] positions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                m_grapplers[i].OverrideIK(positions[i]);
            }
        }

        [Button]
        /// <summary>
        /// Will Only affect Extended Grapplers
        /// </summary>
        /// <param name="active"></param>
        public void SetPhysicsActive(bool active)
        {
            if (active)
            {
                foreach (var grappler in m_grapplers)
                {
                    if (grappler.isExtended)
                    {
                        grappler.SetPhysicsActive(true);
                    }
                }
            }
            else
            {
                for (int i = 0; i < m_grapplers.Length; i++)
                {
                    m_grapplers[i].SetPhysicsActive(false);
                }
            }
        }

        [Button]
        public void StopIKOverrides()
        {
            for (int i = 0; i < m_grapplers.Length; i++)
            {
                m_grapplers[i].StopIKOverride();
            }
        }

#if UNITY_EDITOR
        [Button]
        public void OverrideIKs(Transform[] positions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                m_grapplers[i].OverrideIK(positions[i]);
            }
        }

        [Button]
        private void Retract(float speed)
        {
            StopAllCoroutines();
            StartCoroutine(RetractRoutine(speed));
        }

        [Button]
        private void Extend(float speed, int tentacleCount, bool activatePhysicsAtEnd)
        {
            StopAllCoroutines();
            StartCoroutine(ExtendRoutine(speed, tentacleCount, activatePhysicsAtEnd));
        }
#endif
    }
}

