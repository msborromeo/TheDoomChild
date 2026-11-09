using DChild;
using DChild.Gameplay.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileImpactWaveSlash : MonoBehaviour
{
    [SerializeField]
    private GameObject m_projectleImpact;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InvisibleWall"))
        {
            var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_projectleImpact, gameObject.scene);
            instance.SpawnAt(transform.position, Quaternion.identity);
            Debug.Log("hit");
            Destroy(this.gameObject);
        }
    }
}
