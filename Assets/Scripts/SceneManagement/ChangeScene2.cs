using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene2 : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) //mostly is just for compass
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
