using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LocalDataSingleton : MonoBehaviour {

    public static LocalDataSingleton instance = null;

    [HideInInspector]
    public int previousSceneFrom = -1;
    [HideInInspector]
    public bool talkedToDragon = false;
    [HideInInspector]
    public bool movementDisabled = false;
    [HideInInspector]
    public bool talking = false;

    private float Volume { get; set; }

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

    void Update()
    {
        AudioListener.volume = Volume;

#if UNITY_STANDALONE
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().buildIndex != 0)
            {
                //activate the child canvas
                transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
            }
        }
#endif
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
