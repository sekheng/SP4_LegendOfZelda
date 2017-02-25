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

	// Use this for initialization
	void Start () {
        arrowTransform = GetComponent<RectTransform>();
        GameObject[] allTheInGamePauseButtons = GameObject.FindGameObjectsWithTag("InGamePauseOptions");
        foreach (GameObject zeGO in allTheInGamePauseButtons)
        {
            Debug.Log("Pause button name: " + zeGO.name);
            PauseButtonScript zePause = zeGO.GetComponent<PauseButtonScript>();
            if (zePause != null)
            {
                allThePauseButtons.Add(zePause.m_Number, zePause);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
#if UNIY_ANDROID
        
#else
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpdateUI((short)(whichButtonIsAt - 1));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            UpdateUI((short)(whichButtonIsAt + 1));
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PauseButtonScript zePauseButton;
            // if can find the pause button, then execute that button
            if (allThePauseButtons.TryGetValue(whichButtonIsAt, out zePauseButton))
            {
                zePauseButton.executeButton();
            }
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
    }
}
