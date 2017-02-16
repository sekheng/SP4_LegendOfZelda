using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene2 : MonoBehaviour {

    IEnumerator test;

    void OnTriggerEnter2D(Collider2D other) //mostly is just for compass
    {
        if (other.CompareTag("Player"))
        {
            test = ChangeLevel(SceneManager.GetActiveScene().buildIndex - 1);
            StartCoroutine(test);
        }
    }

    IEnumerator ChangeLevel(int index)
    {
        LocalDataSingleton.instance.previousSceneFrom = SceneManager.GetActiveScene().buildIndex;

        float fadeTime = LocalDataSingleton.instance.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(index);
    }
}
