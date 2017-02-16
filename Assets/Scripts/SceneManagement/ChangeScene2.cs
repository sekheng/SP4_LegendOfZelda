using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene2 : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) //go back to previous scene;
    {
        if (other.CompareTag("Player"))
        {
            //LocalDataSingleton.instance.previousSceneFrom = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(LocalDataSingleton.instance.previousSceneFrom);
        }
    }
}
