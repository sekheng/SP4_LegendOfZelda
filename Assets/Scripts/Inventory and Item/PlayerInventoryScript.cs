using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Keep track of what item does the player has!
/// </summary>
public class PlayerInventoryScript : MonoBehaviour {
    private static string PlayerInventoryTable = "PlayerInventoryTable";

    // To know what item does the player has on hand
    private Dictionary<string, uint> itemName_Count_Map = new Dictionary<string,uint>();

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
