using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Keep track of what item does the player has!
/// All similar items can stack for now!
/// </summary>
public class PlayerInventoryScript : MonoBehaviour {
    [Tooltip("For now, just in case we want limited number of inventory in the space")]
    public int max_InventorySpace = 10;
    
    private static string PlayerInventoryTable = "PlayerInventoryTable";

    // To know what item does the player has on hand
    private Dictionary<string, itemInformation> itemName_Count_Map = new Dictionary<string, itemInformation>();

    public bool passInInventory(itemInformation zeItem)
    {

        return false;
    }
}
