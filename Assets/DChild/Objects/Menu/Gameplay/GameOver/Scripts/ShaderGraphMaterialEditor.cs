using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ShaderGraphMaterialEditor : MonoBehaviour
{
    [SerializeField] public string propertyName1;
    [SerializeField] public float propertyValue1;
    [SerializeField] public string propertyName2;
    [SerializeField] public float propertyValue2;
    [SerializeField] public string propertyName3;
    [SerializeField] public float propertyValue3;
    [SerializeField] public string propertyName4;
    [SerializeField] public float propertyValue4;
    [SerializeField] public string propertyName5;
    [SerializeField] public float propertyValue5;

    private Image uiImage;

    void Awake()
    {
        uiImage = GetComponent<Image>();
    }

    void Update()
    {
        ApplyProperties();
    }

    private void OnValidate()
    {
        ApplyProperties();
    }

    private void ApplyProperties()
    {
        if (uiImage != null && uiImage.material != null)
        {
            if (!string.IsNullOrEmpty(propertyName1)) uiImage.material.SetFloat(propertyName1, propertyValue1);
            if (!string.IsNullOrEmpty(propertyName2)) uiImage.material.SetFloat(propertyName2, propertyValue2);
            if (!string.IsNullOrEmpty(propertyName3)) uiImage.material.SetFloat(propertyName3, propertyValue3);
            if (!string.IsNullOrEmpty(propertyName4)) uiImage.material.SetFloat(propertyName4, propertyValue4);
            if (!string.IsNullOrEmpty(propertyName5)) uiImage.material.SetFloat(propertyName5, propertyValue5);
        }
    }
}
