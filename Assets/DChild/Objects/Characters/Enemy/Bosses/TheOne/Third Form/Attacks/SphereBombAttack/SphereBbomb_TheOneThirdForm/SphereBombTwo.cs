using DChild.Gameplay.Pooling;
using DChild;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Projectiles;

public class SphereBombTwo : MonoBehaviour
{
    [SerializeField]
    private GameObject m_sphereBombFX;
    public bool m_objectToDestroy;
    private void Start()
    {
        m_objectToDestroy = false;
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
       
        Destroy(gameObject);
    }

    public void ShowFX()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11 || collision.CompareTag("Hitbox"))
        {
            //Destroy(gameObject);    
            gameObject.SetActive(false);
            var spherebomb = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_sphereBombFX, gameObject.scene);
            spherebomb.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
            
    }
}
