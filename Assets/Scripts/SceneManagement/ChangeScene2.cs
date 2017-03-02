using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene2 : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) //mostly is just for compass
    {
        if (other.CompareTag("Player"))
        {
            LocalDataSingleton.instance.StartCoroutine(LocalDataSingleton.instance.ChangeLevel(SceneManager.GetActiveScene().buildIndex - 1));
        }
    }
}
