using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LocalDataSingleton : MonoBehaviour {

    public static LocalDataSingleton instance = null;

    public int previousSceneFrom = -1;
    public bool talkedToDragon = false;

    IEnumerator test;

    // Earlier than start
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
    }

    public void GoNext()
    {
        test = ChangeLevel(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(test);
    }

    IEnumerator ChangeLevel(int index)
    {
        LocalDataSingleton.instance.previousSceneFrom = SceneManager.GetActiveScene().buildIndex;

        float fadeTime = LocalDataSingleton.instance.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(index);
    }
}
