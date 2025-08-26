using Holysoft.Event;
using UnityEngine;

namespace DChild.Gameplay.Environment
{
    public class CelestialCube : MonoBehaviour
    {
        [SerializeField]
        private MovableObject m_moveableObject;
        [SerializeField]
        private Rigidbody2D m_rigidbody;

        [SerializeField]
        private Transform m_parentPlatform;

        public Transform parentPlatform { get { return m_parentPlatform; } set { m_parentPlatform = value; } }

        public void AttachSelfToParentPlatform()
        {
            gameObject.transform.SetParent(m_parentPlatform);
        }

    }
}
