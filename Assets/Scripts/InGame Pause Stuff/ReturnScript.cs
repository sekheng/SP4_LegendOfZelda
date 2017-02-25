using UnityEngine;
using System.Collections;

public class ReturnScript : PauseButtonScript {

	// Use this for initialization
	void Start () {
	
	}

    public override bool executeButton()
    {
        Debug.Log("RETURN");
        // Have to set the InGamePauseCanvase to be pause!
        LocalDataSingleton.instance.InGamePauseCanvas.SetActive(false);
        // Player wants to return to main menu
        LocalDataSingleton.instance.ReturntoMain();
        return true;
    }
}
