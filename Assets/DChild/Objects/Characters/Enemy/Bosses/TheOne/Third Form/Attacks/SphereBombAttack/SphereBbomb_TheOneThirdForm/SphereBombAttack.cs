using DChild.Gameplay.Pooling;
using DChild.Gameplay;
using DChild;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DChild.Gameplay.Combat;
using Holysoft.Event;
using System;

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
    [SerializeField]
    private GameObject[] m_smallSphereBomb;
    public List<PoolableObject> m_instantiatedSmallBombs;
    public event EventAction<EventActionArgs> HasDamageTarget;
    public event EventAction<EventActionArgs> HasDamageTargetSmallBomb;
    public event EventAction<EventActionArgs> HasDamageTargetSmallBombTwo;
    private PoolableObject instance;
    Vector2[] targetPositions = {
    new Vector2(17, 3.5f),
    new Vector2(-17, 3.5f),
    new Vector2(9, 7),
    new Vector2(-9, 7),
    new Vector2(0.1f, 10)
};

    [Button]
    public void StartSphereTwo()
    {
        //StartCoroutine(SphereBombTwo());
    }
    
    public IEnumerator SphereBombTwo()
    {
        for (int i = 0; i < m_spawnSpotSphereBombTwo.Length; i++)
        {
            var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_sphereBombTwo, gameObject.scene);
            instance.SpawnAt(new Vector2(m_spawnSpotSphereBombTwo[i].position.x, m_spawnSpotSphereBombTwo[i].position.y), Quaternion.identity); 
            yield return new WaitForSeconds(1f);
            m_sphereList.Add(instance.gameObject);
        }
        yield return null;
    }

    private void SmallBombTwo_TargetDamaged(object sender, CombatConclusionEventArgs eventArgs)
    {
        HasDamageTargetSmallBombTwo?.Invoke(this, EventActionArgs.Empty);
        Debug.Log("target has been damaged by small bomb two");
    }

    public IEnumerator SpawnSphereBomb()
    {
        instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_mainSphereBomb, gameObject.scene);
        instance.GetComponent<Attacker>().TargetDamaged -= SphereBombAttack_TargetDamaged;
        instance.GetComponent<SphereBomb>().SpawnSmallSphereBomb -= SphereBombAttack_SpawnSmallSphereBomb;
        instance.SpawnAt(new Vector2(m_spawnSpot.position.x, m_spawnSpot.position.y), Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(3f);
        instance.GetComponent<SphereBomb>().SpawnSmallSphereBomb += SphereBombAttack_SpawnSmallSphereBomb;
        instance.GetComponent<Attacker>().TargetDamaged += SphereBombAttack_TargetDamaged;
        instance.GetComponent<Rigidbody2D>().isKinematic = false;
        yield return null;

    }

    private void SphereBombAttack_SpawnSmallSphereBomb(object sender, EventActionArgs eventArgs)
    {
        for (int i = 0; i < targetPositions.Length; i++)
        {
            var sphere = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_smallSphereBomb[i], gameObject.scene);
            sphere.SpawnAt(instance.transform.position, Quaternion.identity);

            var smallSphere = sphere.GetComponent<SmallSphereBomb>();
            var attacker = sphere.GetComponent<Attacker>();

            smallSphere.targetPosition = targetPositions[i];
            attacker.TargetDamaged += SphereBombAttack_TargetDamaged1;
        }
    }

    private void SphereBombAttack_TargetDamaged1(object sender, CombatConclusionEventArgs eventArgs)
    {
        HasDamageTargetSmallBomb?.Invoke(this, EventActionArgs.Empty);
        Debug.Log("Got hit by sphere bomb small");
    }

    private void SphereBombAttack_TargetDamaged(object sender, CombatConclusionEventArgs eventArgs)
    {
        HasDamageTarget?.Invoke(this, EventActionArgs.Empty);
        Debug.Log("Got hit by sphere bomb big");

        
    }

    private void OnDisable()
    {
        instance.GetComponent<SphereBomb>().SpawnSmallSphereBomb -= SphereBombAttack_SpawnSmallSphereBomb;
        instance.GetComponent<Attacker>().TargetDamaged -= SphereBombAttack_TargetDamaged;
    }


}
