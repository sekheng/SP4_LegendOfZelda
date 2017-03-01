using UnityEngine;
using System.Collections;

/// <summary>
/// This will help to close the Save UI only!
/// </summary>
public class CloseSaveUIScript : PauseButtonScript {
    // The Save Canvas
    private GameObject theSaveCanvas;

    [Tooltip("The tagname of the canvas")]
    public string m_saveCanvasTagname = "SaveCanvas";

    void Start()
    {
        theSaveCanvas = GameObject.FindGameObjectWithTag(m_saveCanvasTagname);
    }

    public override bool executeButton()
    {
        //Debug.Log("Close save UI");
        // Resume the update for In Game Pause menu!
        //if (LocalDataSingleton.instance.InGamePauseCanvas.activeSelf)
            //LocalDataSingleton.instance.InGamePauseCanvas.SetActive(true);
        // Set the parent to be inactive
        theSaveCanvas.SetActive(false);
        // The quickest fix to the bug in InGamePauseMenu
        LocalDataSingleton.instance.talking = true;
        // Need to know is the in Game Pause menu is active or not!
        if (LocalDataSingleton.instance.InGamePauseCanvas.activeSelf)
        {
            LocalDataSingleton.instance.InGamePauseCanvas.GetComponentInChildren<InGamePauseScript>().m_disableUpdate = false;
        }
        return true;
    }
}
