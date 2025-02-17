using DChild.Gameplay.Pooling;
using DChild;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay.Characters.Enemies;

public class SphereBomb : MonoBehaviour
{
    [SerializeField]
    private GameObject m_spherebombFX;
    [SerializeField]
    private GameObject[] m_smallSphereBomb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) 
        {
            var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_spherebombFX, gameObject.scene);
            instance.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);

            var sphere1 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[0], gameObject.scene);
            sphere1.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            sphere1.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(17, 3.5f);

            var sphere2 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[1], gameObject.scene);
            sphere2.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            sphere2.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(-17, 3.5f);

            var sphere3 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[2], gameObject.scene);
            sphere3.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            sphere3.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(9, 7);

            var sphere4 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[3], gameObject.scene);
            sphere4.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            sphere4.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(-9, 7);

            var sphere5 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[4], gameObject.scene);
            sphere5.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            sphere5.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(0.1f, 10);
            //var sphere2 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[1], gameObject.scene);
            //sphere2.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            //sphere2.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(3.5f, 4.5f);


            // instance.GetComponent<ParticleSystem>().Play();
            Debug.Log("Kaboom");

        }
    }
}
