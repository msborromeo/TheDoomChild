using System.Collections;
using UnityEngine;

public class PlayerSkinPrevis : MonoBehaviour
{
    [Header("Preview Data")]
    [Tooltip("String IDs (skin names, IDs, etc.)")]
    public string[] previewIds;

    [Header("Timing")]
    [Tooltip("Time in seconds between each preview")]
    public float timeDuration = 2f;

    [Header("Target")]
    [Tooltip("Drag the component that receives the string ID")]
    public MonoBehaviour targetComponent;

    [Tooltip("Public method name that accepts ONE string parameter")]
    public string methodName;

    [Header("Runtime (Read Only)")]
    [SerializeField] private string currentRandomId;

    private Coroutine previewRoutine;

    void OnEnable()
    {
        StartPreview();
    }

    void OnDisable()
    {
        StopPreview();
    }

    public void StartPreview()
    {
        if (previewRoutine == null)
            previewRoutine = StartCoroutine(PreviewLoop());
    }

    public void StopPreview()
    {
        if (previewRoutine != null)
        {
            StopCoroutine(previewRoutine);
            previewRoutine = null;
        }
    }

    private IEnumerator PreviewLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeDuration);

            GenerateRandomId();
            CallTargetMethod();
        }
    }

    private void GenerateRandomId()
    {
        if (previewIds == null || previewIds.Length == 0)
            return;

        int randomIndex = Random.Range(0, previewIds.Length);
        currentRandomId = previewIds[randomIndex];
    }

    private void CallTargetMethod()
    {
        if (targetComponent == null || string.IsNullOrEmpty(methodName))
            return;

        // Calls a PUBLIC method with a string parameter
        targetComponent.SendMessage(
            methodName,
            currentRandomId,
            SendMessageOptions.DontRequireReceiver
        );
    }
}
