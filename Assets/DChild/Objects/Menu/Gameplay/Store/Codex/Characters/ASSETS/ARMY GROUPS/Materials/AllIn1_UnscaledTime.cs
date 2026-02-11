using UnityEngine;

[ExecuteAlways]
[DisallowMultipleComponent]
public class AllIn1_UnscaledTime : MonoBehaviour
{
    [Header("Shader Time Property")]
    [Tooltip("Name of the float property used as time inside the shader")]
    [SerializeField] private string timeProperty = "_UnscaledTime";

    [Header("Optional")]
    [Tooltip("Multiply the unscaled time (speed control)")]
    [SerializeField] private float timeMultiplier = 1f;

    private Renderer targetRenderer;
    private MaterialPropertyBlock mpb;

    void Awake()
    {
        Cache();
    }

    void OnEnable()
    {
        Cache();
        UpdateTime();
    }

    void Cache()
    {
        if (mpb == null)
            mpb = new MaterialPropertyBlock();

        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        if (targetRenderer == null)
            return;

        targetRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat(timeProperty, Time.unscaledTime * timeMultiplier);
        targetRenderer.SetPropertyBlock(mpb);
    }
}
