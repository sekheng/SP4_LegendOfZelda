using UnityEngine;
using System.Collections;

/// <summary>
/// This will be used to open a load data canvas then the player can choose which loads to save!
/// </summary>
public class SaveScript : PauseButtonScript {
    [Tooltip("The Canvas to be opened by this button")]
    public GameObject m_saveCanvas;
    // The InGamePauseUpdate
    private InGamePauseScript m_InGamePauseCanvas;

    void Start()
    {
        m_InGamePauseCanvas = GameObject.FindGameObjectWithTag("InGamePause").GetComponentInChildren<InGamePauseScript>();
    }

    /// <summary>
    /// In this case, opens up the load data canvas!
    /// </summary>
    /// <returns>
    /// Returns true if this is a workable button
    /// </returns>
    public override bool executeButton()
    {
        //Debug.Log("SAVE");
        m_saveCanvas.SetActive(true);
        m_InGamePauseCanvas.m_disableUpdate = true;
        return true;
    }
}
