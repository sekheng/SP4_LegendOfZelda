using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This will be used to display how many items in the UI based on the number of OBJs the player has
/// </summary>
public class RecogniseItemUIScript : MonoBehaviour {
    [Tooltip("You will have to put the items here then it can display the items in the Inventory UI")]
    public List<GameObject> allItemPrefabs = new List<GameObject>();
    // To access the player's inventory!
    private PlayerInventoryScript playerInventory;

    // 1st, we need to find how many slots are there!
    void Awake()
    {
        
    }

    // Using this function will allows the restart of finding item in the inventory again!
    void OnEnable()
    {
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventoryScript>();
        }

    }
}
