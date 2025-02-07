using DChild.Gameplay;
using DChild.Gameplay.Characters.Players;
using UnityEngine;

public class SpawnPowent: MonoBehaviour
{
    [SerializeField]
    private GameObject m_sectionArea;
    [SerializeField]
    private GameObject[] m_hideTommi;

    [SerializeField]    
    private bool m_toEnter;

    [SerializeField]
    private bool m_heltisaTommi;
    private void Awake()
    {
        if(m_sectionArea != null) 
        {
            m_sectionArea.SetActive(false);
        }
       
    }

    private void Update()
    {
        #region MyReligion
        //if (toPress)
        //{
        //    if (Input.GetKeyDown(KeyCode.DownArrow))
        //    {
        //        toPress = false;
        //        m_speedPic.SetActive(true);

        //    }
        //}


        //if (m_speedPic.activeInHierarchy == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.Delete))
        //    {
        //        m_speedPic.SetActive(false);
        //    }
        /*} */
        #endregion

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_heltisaTommi)
        {
            //var playerObject = collision.gameObject.GetComponentInParent<PlayerControlledObject>();
            if (collision.CompareTag("Hitbox"))
            {
                for (int i = 0; i < m_hideTommi.Length; i++)
                {
                    m_hideTommi[i].SetActive(false);

                }
            }

        }
        else
        {
            if (collision.CompareTag("Sensor") && collision.gameObject.layer == 8)
            {
                if (m_toEnter)
                {
                    m_sectionArea.SetActive(true);
                }
                else
                {
                    m_sectionArea.SetActive(false);
                }
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_heltisaTommi)
        {
            //var playerObject = collision.gameObject.GetComponentInParent<PlayerControlledObject>();
            if (collision.CompareTag("Hitbox"))
            {
                for (int i = 0; i < m_hideTommi.Length; i++)
                {
                    m_hideTommi[i].SetActive(true);

                }
            }

        }
        else
        {
            if (collision.CompareTag("Sensor") && collision.gameObject.layer == 8)
            {
                if (m_sectionArea.activeInHierarchy == true)
                {
                    m_sectionArea.SetActive(false);
                }
                else
                {
                    m_sectionArea.SetActive(true);
                }
            }
        }

        //if (collision.CompareTag("Sensor") && collision.gameObject.layer == 8)
        //{
        //    if (m_isTommi)
        //    {
        //        for (int i = 0; i < m_hideTommi.Length; i++)
        //        {
        //            m_hideTommi[i].SetActive(true);

        //        }
        //    }
        //    else
        //    {
        //        if (m_sectionArea.activeInHierarchy == true)
        //        {
        //            m_sectionArea.SetActive(false);
        //        }
        //        else
        //        {
        //            m_sectionArea.SetActive(true);
        //        }
        //    }

           
           
        //}


    }

}
