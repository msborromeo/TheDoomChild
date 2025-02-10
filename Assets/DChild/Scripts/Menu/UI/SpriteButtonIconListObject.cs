using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteButtonListData", menuName = "DChild/Debug/Sprite Button List Data")]
public class SpriteButtonIconListObject : ScriptableObject
{
    [SerializeField]
    private List<TMP_SpriteAsset> m_tmpSpriteList;

    public List<TMP_SpriteAsset> tmpSpriteList => m_tmpSpriteList;
}
