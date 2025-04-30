using DChild.Gameplay.Systems;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MinimapPrefabReplacer : MonoBehaviour
{
    [SerializeField]
    private GameObject m_newPrefab;
    [SerializeField]
    private bool m_copyPlaceholder;
    [SerializeField]
    private GameObject[] toBeReplaced;

#if UNITY_EDITOR
    [Button]
    private void ReplaceStuff()
    {
        foreach (var item in toBeReplaced)
        {
            if (item == null)
                continue;

            var instance = PrefabUtility.InstantiatePrefab(m_newPrefab) as GameObject;
            instance.name = item.name;
            instance.transform.parent = item.transform.parent;
            instance.transform.position = item.transform.position;

            var posterData = item.GetComponentInChildren<LocationPoster>().data;
            instance.GetComponentInChildren<LocationPoster>().SetData(posterData);

            var destinationData = item.GetComponentInChildren<LocationSwitcher>().locationData;
            instance.GetComponentInChildren<LocationSwitcher>().SetData(destinationData);

            if (m_copyPlaceholder)
            {
                var placeHolder = item.GetComponentInChildren<SpriteRenderer>();
                var renderer = instance.GetComponentInChildren<SpriteRenderer>();

                renderer.sprite = placeHolder.sprite;
                renderer.color = placeHolder.color;
            }


            DestroyImmediate(item.gameObject);
        }

        toBeReplaced = null;
    } 
#endif
}