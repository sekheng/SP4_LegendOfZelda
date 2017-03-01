using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LocalDataSingleton : MonoBehaviour {

    public static LocalDataSingleton instance = null;

    public GameObject MainCanvas;
    public GameObject OptionsCanvas;
    public GameObject MainMenuCanvas; // only use once, so don't really need to care.
    [Tooltip("Inventory Canvas")]
    public GameObject Inventorycanvas;
    [Tooltip("InGame Pause Canvas")]
    public GameObject InGamePauseCanvas;

    public int previousSceneFrom = -1;
    [HideInInspector]
    public bool talkedToDragon = false;
    [HideInInspector]
    public bool movementDisabled = false;
    [HideInInspector]
    public bool talking = false;

    public float Volume { get; set; }
    private bool Transiting = false;

    //This is used to check if the player can open the ingame pause menu so that he/she wont abuse it!
    private bool isOpeningInPause = false;

    private SoundEffectsManager soundEffects;

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
        soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();
    }

    void Start()
    {
        Volume = MySQLiteHandler.instance.getFloat("GlobalSettingTable", "MusicGlobalVolume");
    }

    void Update()
    {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        AudioListener.volume = Volume;

        if (MainMenuCanvas == null && SceneManager.GetActiveScene().buildIndex == 1)
        {
            MainMenuCanvas = GameObject.Find("MainMenucanvas");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            // for the exit button
#if UNITY_STANDALONE
            MainMenuCanvas.transform.GetChild(2).gameObject.SetActive(true);
#else
            MainMenuCanvas.transform.GetChild(3).gameObject.SetActive(false);
#endif
        }
#if UNITY_STANDALONE
        MainCanvas.transform.GetChild(0).gameObject.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Escape) && 
            SceneManager.GetActiveScene().buildIndex != 0 && //splashpage
            SceneManager.GetActiveScene().buildIndex != 1 && //mainmenu
            SceneManager.GetActiveScene().buildIndex != 2 && //CUTSCENE_1
            SceneManager.GetActiveScene().buildIndex != 4 && //CUTSCENE_2
            SceneManager.GetActiveScene().buildIndex != 10 && //CUTSCENE_3
            SceneManager.GetActiveScene().buildIndex != 11 && //winscreen
            SceneManager.GetActiveScene().buildIndex != 12)
        {
            turnOnInGamePauseMenu();
        }
#else
        if (SceneManager.GetActiveScene().buildIndex != 0 && //splashpage
            SceneManager.GetActiveScene().buildIndex != 1 && //mainmenu
            SceneManager.GetActiveScene().buildIndex != 2 && //CUTSCENE_1
            SceneManager.GetActiveScene().buildIndex != 4 && //CUTSCENE_2
            SceneManager.GetActiveScene().buildIndex != 10 && //CUTSCENE_3
            SceneManager.GetActiveScene().buildIndex != 11 && //winscreen
            SceneManager.GetActiveScene().buildIndex != 12) //losescreen
        {
            MainCanvas.transform.GetChild(0).gameObject.SetActive(true);
            MainCanvas.transform.GetChild(3).gameObject.SetActive(true);
            MainCanvas.transform.GetChild(4).gameObject.SetActive(true);
            MainCanvas.transform.GetChild(5).gameObject.SetActive(true);
            MainCanvas.transform.GetChild(6).gameObject.SetActive(true);
        }
        else
        {
            MainCanvas.transform.GetChild(0).gameObject.SetActive(false);
            MainCanvas.transform.GetChild(3).gameObject.SetActive(false);
            MainCanvas.transform.GetChild(4).gameObject.SetActive(false);
            MainCanvas.transform.GetChild(5).gameObject.SetActive(false);
            MainCanvas.transform.GetChild(6).gameObject.SetActive(false);
        }
#endif

        //if you reach the required number of questitems
        QuestItemScrpt test = FindObjectOfType<QuestItemScrpt>();
        if(test.getCurrenNumOfQuestItems() == test.m_numberOfRelics)
        {
            test.resetQuestItemList();
            if(talkedToDragon)
                talkedToDragon = false;
            onWin();
        }
    }

    public void QuitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
        Application.Quit();
#endif
    }

//FROM HERE ONE ARE OPTIONS TO HELP DO SCENE MOVEMENT
    public void ReturntoMain()
    {
        if(OptionsCanvas.gameObject.activeSelf)
        {
            OptionsCanvas.gameObject.SetActive(false);
        }
        // Because player's pause menu can't be paused.
        InGamePauseCanvas.SetActive(false);
        if (!Transiting)
        {
            StartCoroutine(ChangeLevel(1));
        }
    }

    public void ReturntoPrevious()
    {
        if (!Transiting)
        {
            if(previousSceneFrom == -1)
            {
                StartCoroutine(ChangeLevel(SceneManager.GetActiveScene().buildIndex + 1));
            }
            else
            {
                StartCoroutine(ChangeLevel(previousSceneFrom));
            }
        }
    }

    public void GoNext()
    {
        if (!Transiting)
        {
            StartCoroutine(ChangeLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    public void onWin()
    {
        if (!Transiting)
        {
            StartCoroutine(ChangeLevel(9));
        }
    }

    public void onLose()
    {
        if (!Transiting)
        {
            StartCoroutine(ChangeLevel(12));
        }
    }

    IEnumerator ChangeLevel(int index)
    {
        Transiting = true;
        if (SceneManager.GetActiveScene().buildIndex != 0 && //splashpage
            SceneManager.GetActiveScene().buildIndex != 1 && //mainmenu
            SceneManager.GetActiveScene().buildIndex != 2 && //CUTSCENE_1
            SceneManager.GetActiveScene().buildIndex != 4 && //CUTSCENE_2
            SceneManager.GetActiveScene().buildIndex != 10 && //CUTSCENE_3
            SceneManager.GetActiveScene().buildIndex != 11 && //winscreen
            SceneManager.GetActiveScene().buildIndex != 12) //losescreen
        {
            previousSceneFrom = SceneManager.GetActiveScene().buildIndex;
        }
        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        if (talking)
            talking = false;
        Inventorycanvas.SetActive(false);
        SceneManager.LoadScene(index);
        Transiting = false;
    }

    public void turnOnInGamePauseMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            //activate the child canvas
            //OptionsCanvas.gameObject.SetActive(!OptionsCanvas.gameObject.activeSelf);
            //InGamePauseCanvas.SetActive(!InGamePauseCanvas.activeSelf);
            switch (isOpeningInPause)
            {
                case true:
                    // If the player happens to be openning in game pause menu, turn it off!
                    if (soundEffects != null)
                    {
                        soundEffects.playNegativeSound();

                    }
                    InGamePauseCanvas.SetActive(false);
                    isOpeningInPause = false;
                    talking = false;
                    break;
                default:
                    // If the player is not talking, then player can access the in game pause menu
                    if (!talking)
                    {
                        if (soundEffects != null)
                        {
                            soundEffects.playPositiveSound();

                        }
                        InGamePauseCanvas.SetActive(true);
                        isOpeningInPause = true;
                    }
                    break;
            }
        }
    }
}
