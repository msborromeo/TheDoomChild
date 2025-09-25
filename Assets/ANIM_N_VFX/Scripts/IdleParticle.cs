using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class IdleParticle : MonoBehaviour
{
    private ParticleSystem ps;
    private Transform target;
    private Vector3 lastPosition;
  

    [Tooltip("Tolerance for detecting movement. Lower = more sensitive.")]
    public float moveThreshold = 0.001f;

    private bool activeCheck = false; // starts inactive

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        target = transform.parent != null ? transform.parent : transform;

        // Ensure particles are stopped at start
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        activeCheck = false;

        lastPosition = target.position;
    }

    void Update()
    {
        if (!activeCheck) return;

        float distanceMoved = (target.position - lastPosition).sqrMagnitude;

        if (distanceMoved < moveThreshold)
        {
            if (!ps.isPlaying) ps.Play();
        }
        else
        {
            if (ps.isPlaying) ps.Stop();
        }

        lastPosition = target.position;
    }

    // Call this externally to start idle-check logic
    public void Play()
    {
        activeCheck = true;
        lastPosition = target.position; // reset reference position
        ps.Play();
    }

    // Call this externally to stop everything
    public void Stop()
    {
        activeCheck = false;
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
