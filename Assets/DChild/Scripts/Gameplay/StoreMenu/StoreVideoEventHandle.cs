using Doozy.Runtime.Signals;
using UnityEngine;
using UnityEngine.Video;

namespace DChild.Gameplay.Systems
{
    public class StoreVideoEventHandle : MonoBehaviour
    {
        [SerializeField]
        private SignalSender m_transistionSignal;
        [SerializeField]
        private VideoPlayer m_openVideo;
        [SerializeField]
        private VideoPlayer m_closeVideo;
        [SerializeField]
        private RenderTexture m_texture;
        [SerializeField]
        private Texture m_blank;

        public void ClearRenderer()
        {
            Graphics.Blit(m_blank, m_texture);
        }

        private void OnEnable()
        {
            m_openVideo.loopPointReached += OnVideoDone;
            m_closeVideo.loopPointReached += OnVideoDone;
        }

        private void OnDisable()
        {
            m_openVideo.loopPointReached -= OnVideoDone;
            m_closeVideo.loopPointReached -= OnVideoDone;
        }

        private void OnVideoDone(VideoPlayer source)
        {
            m_transistionSignal.SendSignal();
        }
    }
}