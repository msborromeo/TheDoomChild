using UnityEngine;

[CreateAssetMenu(
    fileName = "AtmosphereAssetPreset",
    menuName = "Atmosphere/Asset Preset",
    order = 1)]
public class AtmosphereAssetPreset : ScriptableObject
{
    [Header("Blur")]
    [Range(0f, 0.01f)]
    public float maxBlur = 0.003f;

    [Range(0f, 2f)]
    public float blurMultiplier = 1f;

    [Header("Edge (SDF)")]
    [Tooltip("Controls how soft the edge transition is")]
    [Range(0f, 1f)]
    public float edgeSoftness = 0.3f;

    [Tooltip("Controls where the edge cutoff happens")]
    [Range(0f, 1f)]
    public float edgeCenter = 0.5f;

    [Header("Temperature")]
    [Range(-1f, 1f)]
    public float temperature = 0f;

    [Range(0f, 2f)]
    public float temperatureMultiplier = 1f;

    public bool ignoreGlobalTemperature = false;

    [Header("Heat Distortion")]
    [Range(0f, 0.02f)]
    public float heatStrength = 0f;

    [Range(0f, 5f)]
    public float heatSpeed = 1f;

    [Header("Wind")]
    public Vector2 windDirection = Vector2.right;

    [Range(0f, 0.02f)]
    public float windStrength = 0f;
}
