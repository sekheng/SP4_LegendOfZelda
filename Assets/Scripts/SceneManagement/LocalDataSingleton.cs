using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LocalDataSingleton : MonoBehaviour {

    public static LocalDataSingleton instance = null;

    public GameObject MainCanvas;
    public GameObject OptionsCanvas;

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
        if(SceneManager.GetActiveScene().buildIndex != 0 && !transform.GetChild(3).gameObject.activeSelf)
        {
            transform.GetChild(3).gameObject.SetActive(!transform.GetChild(3).gameObject.activeSelf);
        }
#if UNITY_STANDALONE
        MainCanvas.transform.GetChild(0).gameObject.SetActive(false);
        MainCanvas.transform.GetChild(2).gameObject.SetActive(false);
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().buildIndex != 0)
            {
                //activate the child canvas
                OptionsCanvas.gameObject.SetActive(!OptionsCanvas.gameObject.activeSelf);
            }
        }
#endif
#if UNITY_ANDROID
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            MainCanvas.transform.GetChild(2).gameObject.SetActive(true);
            MainCanvas.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            MainCanvas.transform.GetChild(2).gameObject.SetActive(false);
            MainCanvas.transform.GetChild(0).gameObject.SetActive(false);
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

        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        if (talking)
            talking = false;
        SceneManager.LoadScene(index);
    }
}
