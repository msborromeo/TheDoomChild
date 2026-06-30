using DChild.Gameplay.Cinematics.Cameras;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Cinematics
{
    public class NarrativeCamShakeHandle : MonoBehaviour
    {
        [SerializeField, Min(0)]
        private int m_shakeIndex;
        [SerializeField, ListDrawerSettings (ShowIndexLabels = true)]
        private CameraShakeData[] m_shakeData;

        public void ExecuteCamShake(int index)
        {
            if (isIndexOutOfBounds(index))
            {
                Debug.LogWarning("NO CAMERA SHAKE IS APPLIED");
                return;
            }
            GameplaySystem.cinema.ExecuteCameraShake(m_shakeData[index]);
        }

        private bool isIndexOutOfBounds(int index)
        {
            return index < 0 || index >= m_shakeData.Length;
        }

        private void Start()
        {
            ExecuteCamShake(m_shakeIndex);
        }
    }
}

