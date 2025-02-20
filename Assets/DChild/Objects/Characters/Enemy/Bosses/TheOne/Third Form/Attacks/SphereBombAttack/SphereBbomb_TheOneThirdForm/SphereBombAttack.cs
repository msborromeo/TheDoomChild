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
    private GameObject m_sphereBombTwo;
    [SerializeField]
    private Transform[] m_spawnSpotSphereBombTwo;
    public List<GameObject> m_sphereList;

    private void Start()
    {
    }

    [Button]
    public void StartSphereTwo()
    {
        //StartCoroutine(SphereBombTwo());
    }
    
    public IEnumerator SphereBombTwo()
    {
        for (int i = 0; i < m_spawnSpotSphereBombTwo.Length; i++)
        {
            var instance = GameSystem.poolManager.GetPool<ProjectilePool>().GetOrCreateItem(m_sphereBombTwo, gameObject.scene);
            instance.SpawnAt(new Vector2(m_spawnSpotSphereBombTwo[i].position.x, m_spawnSpotSphereBombTwo[i].position.y), Quaternion.identity);
            yield return new WaitForSeconds(1f);
            m_sphereList.Add(instance.gameObject);
        }
        yield return null;
    }
    public IEnumerator SpawnSphereBomb()
    {
        var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_mainSphereBomb, gameObject.scene);
        instance.SpawnAt(new Vector2(m_spawnSpot.position.x, m_spawnSpot.position.y), Quaternion.identity);
        yield return null;

    }
}
