using DChildDebug.Cutscene;
using Doozy.Runtime.Signals;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace DChild.Gameplay.Systems
{
    public class CinematicVideoHandle : MonoBehaviour
    {
        [SerializeField]
        private SignalSender m_videoCinemaStartSignal;
        [SerializeField]
        private SignalSender m_videoCinemaEndSignal;
        [SerializeField]
        private VideoPlayer m_videoPlayer;
        [SerializeField, Min(0)]
        private float m_fadeBufferTime = 1;


        //used to ease in of in game sounds before cinematic ends
        [SerializeField]
        private bool m_hasEventOnVideoEnd;
        [SerializeField,ShowIf("m_hasEventOnVideoEnd")]
        private float m_secondsBeforeVideoEnd;
        [SerializeField]
        private float m_audioVolumeTransistion;

        private bool m_isPlaying;
        private bool m_videoClipPlaying;
        private Func<IEnumerator> m_behindTheSceneRoutine;
        private Action OnVideoDone;

        private Coroutine m_videoPlayingRoutine;

        public bool hasEventOnVideoEnd { get { return m_hasEventOnVideoEnd; } set { m_hasEventOnVideoEnd = value; } }
        public float secondsBeforeVideoEnd { get { return m_secondsBeforeVideoEnd; } set { m_secondsBeforeVideoEnd = value; } }

        public float audioVolumeTransistionDuration { get { return m_audioVolumeTransistion; } set { m_audioVolumeTransistion = value; } }


        public void ShowCinematicVideo(VideoClip clip, Func<IEnumerator> behindTheSceneRoutine = null, Action OnVideoDone = null)
        {
            if (m_isPlaying == false)
            {
                if (clip == null)
                {
                    Debug.LogWarning("There was an attempt to play a null video cinematic");
                    return;
                }

                m_videoPlayer.clip = clip;
                CalculateVideoLength(m_videoPlayer.clip);
                m_behindTheSceneRoutine = behindTheSceneRoutine;
                this.OnVideoDone = OnVideoDone;

                m_videoPlayingRoutine = StartCoroutine(VideoPlayingRoutine());
                SequenceSkipHandle.SkipExecute += SequenceSkipHandle_SkipExecute;
            }
            else
            {
                Debug.LogWarning("Attempting to Play a Video Clip while theres already a video clip being played");
            }
        }

        public void ForceStopCinematicVideo()
        {
            StopAllCoroutines();
            m_videoPlayer.Stop();
            m_videoCinemaEndSignal?.SendSignal();
            GameplaySystem.playerManager.StopCharacterControlOverride();
            GameplaySystem.gamplayUIHandle.ToggleFadeUI(false);
            m_isPlaying = false;
            m_videoPlayingRoutine = null;
        }

        public void Initialize()
        {
            m_videoPlayer.loopPointReached += OnVideoClipDone;

        }

        private void SequenceSkipHandle_SkipExecute()
        {
            m_videoClipPlaying = false;
            OnVideoDone?.Invoke();
            SequenceSkipHandle.SkipExecute -= SequenceSkipHandle_SkipExecute;
        }

        private void OnVideoClipDone(VideoPlayer source)
        {
            OnVideoDone?.Invoke();
            SequenceSkipHandle.SkipExecute -= SequenceSkipHandle_SkipExecute;
            m_videoClipPlaying = false;
        }

        private IEnumerator VideoPlayingRoutine()
        {
            var waitForFade = new WaitForSeconds(m_fadeBufferTime); 
        
            MuteAllSounds();
            GameplaySystem.playerManager.OverrideCharacterControls();
            m_isPlaying = true;
            GameplaySystem.gamplayUIHandle.ToggleFadeUI(true);
            yield return waitForFade;
            m_videoCinemaStartSignal?.SendSignal();
            m_videoClipPlaying = true;
            m_videoPlayer.Play();
           


            if (m_behindTheSceneRoutine != null)
            {

              

                yield return m_behindTheSceneRoutine();
           
            }
            while (m_videoClipPlaying)
            {
                var vidLength = m_videoPlayer.clip.length;
                var currentTime = m_videoPlayer.time;
                var remainingTime = vidLength - currentTime;
                Debug.Log("Seconds: " + remainingTime);
                if (m_hasEventOnVideoEnd)
                {
                    if (remainingTime <= m_secondsBeforeVideoEnd)
                    {
                        BaseGameplaySystem.UnMuteAllSounds(audioVolumeTransistionDuration);
                    }
                }
                yield return null;
            }
            yield return null;

            m_videoPlayer.Stop();
            m_videoCinemaEndSignal?.SendSignal();
            OnVideoDone?.Invoke();
            yield return waitForFade;
            GameplaySystem.playerManager.StopCharacterControlOverride();
            GameplaySystem.gamplayUIHandle.ToggleFadeUI(false);
            m_isPlaying = false;
            m_videoPlayingRoutine = null;
        }
        void CalculateVideoLength(VideoClip clip)
        {
            // Get frame count and frame rate from the VideoPlayer. 
            ulong frameCount = m_videoPlayer.frameCount;
            double frameRate = m_videoPlayer.frameRate;

            // Calculate the length in seconds. 
            double lengthInSeconds = frameCount / frameRate;
        }
        private void MuteAllSounds()
        {
            // this function should handle the mute logic of sounds except the video.
            //for future reference, put this function to the VideoPlayingRoutine()
            BaseGameplaySystem.MuteAllSounds();

        }

        private void OnDisable()
        {
            m_videoPlayer.loopPointReached -= OnVideoClipDone;
        }
    }
}
