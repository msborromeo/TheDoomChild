using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class AllIn1VfxUnscaledTimeUI : MonoBehaviour
{
    [Tooltip("The UI Image or RawImage using AllIn1Vfx shader")]
    public Graphic targetGraphic;

    [Tooltip("Speed multiplier for unscaled time")]
    public float timeScale = 1f;

    private Material runtimeMaterial;

    void Awake()
    {
        if (targetGraphic == null)
            targetGraphic = GetComponent<Graphic>();

        if (targetGraphic != null)
        {
            // Make a unique instance of the material for this UI element
            runtimeMaterial = targetGraphic.material;
        }
    }

    void Update()
    {
        if (runtimeMaterial == null) return;

        // Update the _UnscaledTime property directly on the material
        runtimeMaterial.SetFloat("_UnscaledTime", Time.unscaledTime * timeScale);
    }
}
