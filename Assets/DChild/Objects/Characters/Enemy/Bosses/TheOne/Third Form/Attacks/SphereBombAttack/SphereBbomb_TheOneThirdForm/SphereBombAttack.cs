using DChild.Gameplay.Pooling;
using DChild.Gameplay;
using DChild;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SphereBombAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject m_mainSphereBomb;
    [SerializeField]
    private Transform m_spawnSpot;
    [SerializeField]
    private GameObject[] m_sphereBombTwo;
    [SerializeField]
    private Transform[] m_spawnSpotSphereBombTwo;

    private void Start()
    {
    }

    public IEnumerator SphereBombTwo()
    {
        yield return null;
    }
    public IEnumerator SpawnSphereBomb()
    {
        var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_mainSphereBomb, gameObject.scene);
        instance.SpawnAt(new Vector2(m_spawnSpot.position.x, m_spawnSpot.position.y), Quaternion.identity);
        yield return null;

    }
}
