using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeBoneSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_bone;
    [SerializeField]
    private Transform m_spawnPont;
    [SerializeField]
    private Boss_1_Bone_Waggon m_waggon;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DamageCollider"))
        {
         
                m_waggon.spawnBungo(m_spawnPont, m_bone);
         
  
        }
         
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    private void Awake()
    {
       
    }
}
