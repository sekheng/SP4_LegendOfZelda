using UnityEngine;
using System.Collections;

/// <summary>
/// This will help to close the Save UI only!
/// </summary>
public class CloseSaveUIScript : PauseButtonScript {
    // We will need the InGamePause Menu to resume update!
    private InGamePauseScript theInGamePauseUpdate;
    // The Save Canvas
    private GameObject theSaveCanvas;

    void Start()
    {
        theInGamePauseUpdate = GameObject.FindGameObjectWithTag("InGamePause").GetComponentInChildren<InGamePauseScript>();
        theSaveCanvas = GameObject.FindGameObjectWithTag("SaveCanvas");
    }

    public override bool executeButton()
    {
        //Debug.Log("Close save UI");
        // Resume the update for In Game Pause menu!
        theInGamePauseUpdate.m_disableUpdate = false;
        // Set the parent to be inactive
        theSaveCanvas.SetActive(false);
        // The quickest fix to the bug in InGamePauseMenu
        LocalDataSingleton.instance.talking = true;
        return true;
    }
}
