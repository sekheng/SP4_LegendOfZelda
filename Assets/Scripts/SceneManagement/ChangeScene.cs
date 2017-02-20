using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    IEnumerator test;

    void OnTriggerEnter2D(Collider2D other) //mostly is just for compass
    {
        if (other.CompareTag("Player"))
        {
            test = LocalDataSingleton.instance.ChangeLevel(SceneManager.GetActiveScene().buildIndex + 1);
            StartCoroutine(test);
        }
    }
}
