using DChild.Gameplay.Combat;
using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BeeHiveAI : MonoBehaviour
{
    [SerializeField]
    private Transform m_spawnPoint;
    [SerializeField]
    private float m_spawnStartDelay;
    [SerializeField]
    private Damageable m_health;
    [SerializeField]
    private GameObject m_SpawnZone;
    private bool m_spawn=false;
    [SerializeField, TabGroup("Summons")]
    private List<GameObject> m_minions = new List<GameObject>();
    private List<Damageable> m_summons = new List<Damageable>();

    private bool m_firstEnter = false;
    // Start is called before the first frame update
    public void SpawnBee()
    {
        StartCoroutine(SpawnRoutine());

    }
    public void StopSpawn()
    {
        StopCoroutine(SpawnRoutine());
        m_spawn = false;
    }
    private float spawnRadius = 0.5f;
    private IEnumerator SpawnRoutine()
    {
   
        if (m_summons.Count >= m_minions.Count)
            yield break;
        
        for (int i = 0; i < m_minions.Count; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = m_spawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

            if (m_summons.Count < m_minions.Count)
            {
                int index = m_firstEnter ? Random.Range(0, m_minions.Count) : i;

                var bee = Instantiate(m_minions[index], spawnPosition, Quaternion.identity);

                var damageableBee = bee.GetComponent<Damageable>();
                m_summons.Add(damageableBee);

                damageableBee.Destroyed += BeeHiveAI_Destroyed1;
            }

            yield return new WaitForSeconds(m_spawnStartDelay);

            //  
        }
        m_firstEnter = true;
       //_spawn = true;

    }

    private void BeeHiveAI_Destroyed1(object sender, Holysoft.Event.EventActionArgs eventArgs)
    {
        Debug.Log("dead beehive");
        for (int i = 0; i < m_summons.Count; i++)
        {
            var healthBee = m_summons[i];
            if(healthBee.health.currentValue == 0)
            {
                m_summons.Remove(healthBee);
            }
        }
    }

    void Awake()
        {
        m_health.Destroyed += M_health_Destroyed;


        }

    private void M_health_Destroyed(object sender, Holysoft.Event.EventActionArgs eventArgs)
    {
        m_SpawnZone.SetActive(false);
        StopSpawn();
    }

    private void Start()
    {
       
        if (m_spawn == true)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    void Update()
    {
     
    }
}
