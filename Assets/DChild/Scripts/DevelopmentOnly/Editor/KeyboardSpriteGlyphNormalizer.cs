using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore;

namespace DChildEditor
{
    internal static class KeyboardSpriteGlyphNormalizer
    {
        private const string SpriteAssetPath = "Assets/TextMesh Pro/Resources/Sprite Assets/Keyboard.asset";
        private const float StandardBearingX = 0f;
        private const float StandardBearingY = 24f;

        [MenuItem("Tools/DChild Utility/Normalize Keyboard Glyph Positions")]
        private static void NormalizeGlyphPositions()
        {
            var spriteAsset = AssetDatabase.LoadAssetAtPath<TMP_SpriteAsset>(SpriteAssetPath);
            if (spriteAsset == null)
            {
                Debug.LogError($"Keyboard TMP sprite asset was not found at '{SpriteAssetPath}'.");
                return;
            }

            var glyphTable = spriteAsset.spriteGlyphTable;
            if (glyphTable == null || glyphTable.Count == 0)
            {
                Debug.LogError($"Keyboard TMP sprite asset at '{SpriteAssetPath}' has no glyphs.");
                return;
            }

            var changedGlyphCount = 0;
            foreach (var glyph in glyphTable)
            {
                if (glyph == null)
                {
                    continue;
                }

                var metrics = glyph.metrics;
                if (!Mathf.Approximately(metrics.horizontalBearingX, StandardBearingX) ||
                    !Mathf.Approximately(metrics.horizontalBearingY, StandardBearingY))
                {
                    changedGlyphCount++;
                }
            }

            if (changedGlyphCount == 0)
            {
                Debug.Log("Keyboard TMP sprite glyph positions are already normalized.", spriteAsset);
                return;
            }

            Undo.RecordObject(spriteAsset, "Normalize Keyboard Glyph Positions");

            foreach (var glyph in glyphTable)
            {
                if (glyph == null)
                {
                    continue;
                }

                var metrics = glyph.metrics;
                glyph.metrics = new GlyphMetrics(
                    metrics.width,
                    metrics.height,
                    StandardBearingX,
                    StandardBearingY,
                    metrics.horizontalAdvance);
            }

            spriteAsset.UpdateLookupTables();
            EditorUtility.SetDirty(spriteAsset);
            AssetDatabase.SaveAssets();

            Debug.Log(
                $"Normalized {changedGlyphCount} keyboard TMP sprite glyph position(s) to bearings " +
                $"({StandardBearingX}, {StandardBearingY}).",
                spriteAsset);
        }
    }
}
