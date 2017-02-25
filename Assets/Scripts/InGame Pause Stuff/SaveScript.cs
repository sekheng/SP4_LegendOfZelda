using UnityEngine;
using System.Collections;

public class SaveScript : PauseButtonScript {

	// Use this for initialization
	void Start () {
	
	}


    public override bool executeButton()
    {
        Debug.Log("SAVE");
        return true;
    }
}
