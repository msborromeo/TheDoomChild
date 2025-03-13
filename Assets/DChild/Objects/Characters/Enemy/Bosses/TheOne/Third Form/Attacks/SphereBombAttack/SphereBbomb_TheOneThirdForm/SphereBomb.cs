using DChild.Gameplay.Pooling;
using DChild;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Combat;
using Holysoft.Event;

public class SphereBomb : MonoBehaviour
{
    [SerializeField]
    private GameObject m_spherebombFX;
    public event EventAction<EventActionArgs> SpawnSmallSphereBomb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11 || collision.CompareTag("Hitbox") && collision.gameObject.layer == 8)
        {
            var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_spherebombFX, gameObject.scene);
            instance.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
            SpawnSmallSphereBomb?.Invoke(this,EventActionArgs.Empty);
            //var sphere1 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[0], gameObject.scene);
            //sphere1.SpawnAt(new Vector2(instance.transform.position.x, instance.transform.position.y), Quaternion.identity);
            //sphere1.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(17, 3.5f);
            //sphere1.GetComponent<Attacker>().TargetDamaged += SphereBombAttack_TargetDamaged1;
            //var sphere2 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[1], gameObject.scene);
            //sphere2.SpawnAt(new Vector2(instance.transform.position.x, instance.transform.position.y), Quaternion.identity);
            //sphere2.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(-17, 3.5f);
            //sphere2.GetComponent<Attacker>().TargetDamaged += SphereBombAttack_TargetDamaged1;
            //var sphere3 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[2], gameObject.scene);
            //sphere3.SpawnAt(new Vector2(instance.transform.position.x, instance.transform.position.y), Quaternion.identity);
            //sphere3.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(9, 7);
            //sphere3.GetComponent<Attacker>().TargetDamaged += SphereBombAttack_TargetDamaged1;
            //var sphere4 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[3], gameObject.scene);
            //sphere4.SpawnAt(new Vector2(instance.transform.position.x, instance.transform.position.y), Quaternion.identity);
            //sphere4.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(-9, 7);
            //sphere4.GetComponent<Attacker>().TargetDamaged += SphereBombAttack_TargetDamaged1;
            //var sphere5 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[4], gameObject.scene);
            //sphere5.SpawnAt(new Vector2(instance.transform.position.x, instance.transform.position.y), Quaternion.identity);
            //sphere5.GetComponent<SmallSphereBomb>().targetPosition = new Vector2(0.1f, 10);
            //sphere5.GetComponent<Attacker>().TargetDamaged += SphereBombAttack_TargetDamaged1;
        }


        // instance.GetComponent<ParticleSystem>().Play();
        Debug.Log("Kaboom");


    }
}

