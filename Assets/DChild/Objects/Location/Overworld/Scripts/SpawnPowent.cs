using UnityEngine;

public class SpawnPowent: MonoBehaviour
{
    [SerializeField]
    private GameObject m_speedPic;



    private void Update()
    {
        #region MyRegion
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
        if (collision.CompareTag("Hitbox"))
        {
            m_speedPic.SetActive(false);
        }

        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            m_speedPic.SetActive(true);
        }


    }

}
