using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// This is used for the navigation of arrows for keyboard. And touch inputs for mobile
/// </summary>
public class InGamePauseScript : MonoBehaviour {
    // To keep track of which button the in game pause menu is at!
    private Dictionary<short, PauseButtonScript> allThePauseButtons = new Dictionary<short, PauseButtonScript>();
    // Need to know which button 
    private short whichButtonIsAt = 0;
    // Need to know it's own rectTransform
    private RectTransform arrowTransform;
    [Tooltip("The buttons' tag for it")]
    public string m_buttonTags = "InGamePauseOptions";
    // We will need to disable the enter and other keys
    [HideInInspector]
    public bool m_disableUpdate = false;
    [Tooltip("The tagname of the canvas that holds these InGamePause Stuff")]
    public string m_canvasTagname = "SaveCanvas";
    // We need to know which canvas is holding these in game pause stuff!
#if UNITY_STANDALONE
    private GameObject m_InGamePauseCanvas;
#endif
    private SoundEffectsManager soundEffects;

#if UNITY_ANDROID
    // To keep track of player's joystick
    private PlayerDrag thePlayerJoystick;
#endif

    // Use this for initialization
    void Start () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();
        }
#if UNITY_STANDALONE
     m_InGamePauseCanvas = GameObject.FindGameObjectWithTag(m_canvasTagname);
#endif
#if UNITY_ANDROID
        // Since there will only be 1 joystick!
        thePlayerJoystick = FindObjectOfType<PlayerDrag>();
#endif
        arrowTransform = GetComponent<RectTransform>();
        GameObject[] allTheInGamePauseButtons = GameObject.FindGameObjectsWithTag(m_buttonTags);
        foreach (GameObject zeGO in allTheInGamePauseButtons)
        {
            //Debug.Log("Pause button name: " + zeGO.name);
            PauseButtonScript zePause = zeGO.GetComponent<PauseButtonScript>();
            if (zePause != null)
            {
                allThePauseButtons.Add(zePause.m_Number, zePause);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        if (m_disableUpdate)
            return;
        // In the mean time, we need to make sure the player can't even move for sure!
        switch (LocalDataSingleton.instance.talking)
        {
            case false:
                // If the player can still move, just force it to stop!
                LocalDataSingleton.instance.talking = true;
                break;
            default:
                break;
        }
#if UNITY_ANDROID
        if (thePlayerJoystick.movingInYDirection == 1)
        {
        if (soundEffects != null)
            {
                soundEffects.playPositiveSound();

            }
            UpdateUI((short)(whichButtonIsAt - 1));
        }
        else if (thePlayerJoystick.movingInYDirection == -1)
        {
        if (soundEffects != null)
            {
                soundEffects.playPositiveSound();

            }
            UpdateUI((short)(whichButtonIsAt + 1));
        }
#else
        if (Input.GetKeyDown(KeyBindScript.upKey))
        {
            UpdateUI((short)(whichButtonIsAt - 1));
            if (soundEffects != null)
            {
                soundEffects.playPositiveSound();

            }
        }
        if (Input.GetKeyDown(KeyBindScript.downKey))
        {
            UpdateUI((short)(whichButtonIsAt + 1));
            if (soundEffects != null)
            {
                soundEffects.playPositiveSound();

            }
        }
        if (Input.GetKeyDown(KeyBindScript.attackKey) || Input.GetKeyDown(KeyCode.Return))
        {
            PauseButtonScript zePauseButton;
            // if can find the pause button, then execute that button
            if (allThePauseButtons.TryGetValue(whichButtonIsAt, out zePauseButton))
            {
                zePauseButton.executeButton();
                if (soundEffects != null)
                {
                    soundEffects.playPositiveSound();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_InGamePauseCanvas.SetActive(false);
        }
#endif
    }

    /// <summary>
    /// To move the arrow down
    /// </summary>
    /// <param name="zeNum">
    /// The position which the UI is going to be at
    /// </param>
    void UpdateUI(short zeNum)
    {
        if (zeNum >= 0 && zeNum < allThePauseButtons.Count)
        {
            whichButtonIsAt = zeNum;
            PauseButtonScript zePausebutton;
            if (allThePauseButtons.TryGetValue(whichButtonIsAt, out zePausebutton))
            {
                RectTransform zeButtontransform = zePausebutton.GetComponent<RectTransform>();
                arrowTransform.localPosition = new Vector3(arrowTransform.localPosition.x, zeButtontransform.localPosition.y, arrowTransform.localPosition.z);
            }
        }
    }

    void OnEnable()
    {
        LocalDataSingleton.instance.talking = true;
        // Need to restart the position of the arrow to be at the very top!
        UpdateUI(0);
    }

    void OnDisable()
    {
        // We will need to disable the talking and movement!
        LocalDataSingleton.instance.talking = false;
        m_disableUpdate = false;
    }

    /// <summary>
    /// This will means based on which slot the arrow is at, execute that button!
    /// </summary>
    public void selectedTheButton()
    {
        PauseButtonScript zePauseButton;
        // if can find the pause button, then execute that button
        if (allThePauseButtons.TryGetValue(whichButtonIsAt, out zePauseButton))
        {
            zePauseButton.executeButton();
        }
    }
}
