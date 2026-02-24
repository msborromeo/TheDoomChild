using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxAssetAtmosphere : MonoBehaviour
{
    [Header("Camera")]
    public Transform targetCamera;

    [Header("Distance Blur")]
    public float nearDistance = 2f;
    public float farDistance = 20f;

    [Header("Atmosphere Preset")]
    public AtmosphereAssetPreset preset;

    [Header("Overrides")]
    public bool overrideTemperature = false;

    [Range(-1f, 1f)]
    public float temperatureOverride = 0f;

    [Header("Optimization")]
    [Tooltip("Update every N frames (2–4 recommended for backgrounds)")]
    [SerializeField] private int updateEveryNFrames = 2;

    // Runtime (NOT serialized – safe)
    private Material runtimeMat;
    private float zoneTemperature = 0f;
    private bool insideZone = false;

    // Optimization state
    private bool isVisible = true;
    private int frameCounter = 0;

    void Awake()
    {
        if (!targetCamera)
        {
            Camera cam = Camera.main;
            if (cam)
                targetCamera = cam.transform;
        }

        var sr = GetComponent<SpriteRenderer>();

        if (sr.sharedMaterial == null)
        {
            Debug.LogError(
                $"[{name}] SpriteRenderer has no material assigned.",
                this
            );
            enabled = false;
            return;
        }

        // IMPORTANT: instance material per asset
        runtimeMat = Instantiate(sr.sharedMaterial);
        sr.material = runtimeMat;
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }

    void OnBecameInvisible()
    {
        isVisible = false;
    }

    void LateUpdate()
    {
        // Skip if not visible or invalid
        if (!isVisible || !preset || !runtimeMat || !targetCamera)
            return;

        // Throttle updates
        frameCounter++;
        if (frameCounter % updateEveryNFrames != 0)
            return;

        // -------------------------
        // Distance-based blur
        // -------------------------
        float distance = Vector3.Distance(
            transform.position,
            targetCamera.position
        );

        float distanceFactor = Mathf.InverseLerp(
            nearDistance,
            farDistance,
            Mathf.Max(distance, nearDistance)
        );

        float blur =
            preset.maxBlur *
            distanceFactor *
            preset.blurMultiplier;

        // Distance LOD (VERY IMPORTANT FOR FPS)
        if (distanceFactor < 0.25f)
        {
            // Near objects: no blur, no heat
            runtimeMat.SetFloat("_BlurSize", 0f);
            runtimeMat.SetFloat("_HeatStrength", 0f);
        }
        else if (distanceFactor < 0.6f)
        {
            // Mid distance: reduced blur
            runtimeMat.SetFloat("_BlurSize", blur * 0.5f);
            runtimeMat.SetFloat("_HeatStrength", preset.heatStrength * 0.5f);
        }
        else
        {
            // Far distance: full effect
            runtimeMat.SetFloat("_BlurSize", blur);
            runtimeMat.SetFloat("_HeatStrength", preset.heatStrength);
        }

        runtimeMat.SetFloat("_DistanceBlur", distanceFactor);

        // -------------------------
        // Edge softness / center
        // -------------------------
        runtimeMat.SetFloat("_Softness", preset.edgeSoftness);
        runtimeMat.SetFloat("_Threshold", preset.edgeCenter);

        // -------------------------
        // Temperature
        // -------------------------
        float globalTemp = preset.ignoreGlobalTemperature
            ? 0f
            : Shader.GetGlobalFloat("_GlobalTemp");

        float temp = globalTemp + preset.temperature;

        if (insideZone)
            temp += zoneTemperature;

        if (overrideTemperature)
            temp = temperatureOverride;

        temp *= preset.temperatureMultiplier;

        runtimeMat.SetFloat("_Temperature", temp);

        // -------------------------
        // Wind & Heat speed
        // -------------------------
        runtimeMat.SetFloat("_HeatSpeed", preset.heatSpeed);
        runtimeMat.SetVector(
            "_Wind",
            preset.windDirection * preset.windStrength
        );
    }

    // Called by WeatherZone
    public void SetZoneTemperature(float value)
    {
        zoneTemperature = value;
        insideZone = true;
    }

    public void ClearZoneTemperature()
    {
        zoneTemperature = 0f;
        insideZone = false;
    }
}
