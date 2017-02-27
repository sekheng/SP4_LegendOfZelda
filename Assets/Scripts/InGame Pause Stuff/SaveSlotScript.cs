using UnityEngine;
using System.Collections;

/// <summary>
/// This will be used to save the slot the moment the player pressed it!
/// </summary>
public class SaveSlotScript : PauseButtonScript {
    // We will needthe LoadGameScript so as to save the data!
    private LoadGameScript toSaveToSQLite;

	// Use this for initialization
	void Start () {
        toSaveToSQLite = GetComponent<LoadGameScript>();
	}

    public override bool executeButton()
    {
        Debug.Log("Saving to the button: " + toSaveToSQLite.m_loadNumber);
        return true;
    }
}
