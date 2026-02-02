using UnityEngine;

[ExecuteAlways]
public class LerpGlowMPB : MonoBehaviour
{
    [Header("Renderer")]
    public Renderer targetRenderer;

    [Header("Shader Property")]
    [Tooltip("Example: _GlowColor, _HitColor, _OutlineColor")]
    public string colorPropertyName = "_GlowColor";

    [Header("Glow Settings")]
    public Color glowColor = Color.white;
    public float glowIntensity = 2f;
    public float duration = 0.25f;

    MaterialPropertyBlock mpb;
    Color originalColor;

    float timer;
    bool playing;

    void OnEnable()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        if (targetRenderer == null)
            return;

        if (mpb == null)
            mpb = new MaterialPropertyBlock();

        // Cache original value
        targetRenderer.GetPropertyBlock(mpb);

        if (mpb.HasColor(colorPropertyName))
            originalColor = mpb.GetColor(colorPropertyName);
        else
            originalColor = Color.black;
    }

    void Update()
    {
        if (!playing || targetRenderer == null)
            return;

        // Editor-safe time
        timer += Application.isPlaying ? Time.deltaTime : 0.016f;

        float t = Mathf.Clamp01(timer / duration);

        // Smooth pulse: 0 → 1 → 0
        float pulse = Mathf.Sin(t * Mathf.PI);

        Color current = Color.Lerp(
            originalColor,
            glowColor * glowIntensity,
            pulse
        );

        mpb.SetColor(colorPropertyName, current);
        targetRenderer.SetPropertyBlock(mpb);

        if (t >= 1f)
        {
            mpb.SetColor(colorPropertyName, originalColor);
            targetRenderer.SetPropertyBlock(mpb);
            playing = false;
        }
    }

    /// <summary>
    /// Call this from Animator, PlayMaker, or code
    /// </summary>
    public void PlayGlow()
    {
        if (targetRenderer == null)
            OnEnable();

        timer = 0f;
        playing = true;
    }
}
