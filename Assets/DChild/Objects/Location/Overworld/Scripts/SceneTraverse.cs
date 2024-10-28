using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTraverse: MonoBehaviour
{
    [SerializeField]
    private string m_sceneName;

    public bool m_isfromisolatedwindmill;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hitbox")
        {
            m_isfromisolatedwindmill = true;
            SceneManager.LoadScene(m_sceneName);
           
        }
    }
}
