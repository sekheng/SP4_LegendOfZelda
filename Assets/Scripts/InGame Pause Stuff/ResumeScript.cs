using UnityEngine;
using System.Collections;

/// <summary>
/// Used when 
/// </summary>
public class ResumeScript : PauseButtonScript {

	// Use this for initialization
	void Start () {
	
	}

    public override bool executeButton()
    {
        //Debug.Log("RESUME");
        // Since this is resume button, we will just make it so that player will resume controls
        LocalDataSingleton.instance.InGamePauseCanvas.SetActive(false);
        return true;
    } 
}
