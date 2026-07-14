using UnityEngine;
using Spine.Unity;

public class SpineTemporaryOverride : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    private MeshRenderer meshRenderer;

    [SerializeField] private Material silhouetteMaterial;

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void EnableWhiteSilhouette()
    {
        if (silhouetteMaterial == null || skeletonAnimation == null || meshRenderer == null) return;

        // Clear any old overrides first
        skeletonAnimation.CustomMaterialOverride.Clear();

        // Loop through every single material/atlas page the character uses
        foreach (Material originalMat in meshRenderer.sharedMaterials)
        {
            if (originalMat == null) continue;

            // Create a temporary copy of the silhouette material for this specific texture page
            Material instanceMaterial = new Material(silhouetteMaterial);
            instanceMaterial.mainTexture = originalMat.mainTexture;

            // Map the original material page to its corresponding silhouette copy
            skeletonAnimation.CustomMaterialOverride[originalMat] = instanceMaterial;
        }
    }

    public void DisableSilhouette()
    {
        if (skeletonAnimation == null) return;
        
        skeletonAnimation.CustomMaterialOverride.Clear();
    }

    public void SetCustomSpeed(float chosenSpeed)
    {
    	if (skeletonAnimation != null)
    	{
        	skeletonAnimation.timeScale = chosenSpeed;
    	}
    }

    public void ResetAnimationSpeed()
    {
    	if (skeletonAnimation != null)
    	{
        	skeletonAnimation.timeScale = 1.0f;
    	}
    }
}