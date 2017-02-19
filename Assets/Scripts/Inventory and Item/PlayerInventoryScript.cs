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
        itemInformation toCheckWhetherItExistInsideInventory;
        // Add to the stacks of items!
        if (itemName_Count_Map.TryGetValue(zeItem.item_name, out toCheckWhetherItExistInsideInventory))
        {
            toCheckWhetherItExistInsideInventory.item_count++;
            Debug.Log("Stacking item: " + zeItem.item_name);
            return true;
        }
        // If still can contain more inventory space, then add it in!
        if (itemName_Count_Map.Count < max_InventorySpace)
        {
            Debug.Log("Adding item: " + zeItem.item_name);
            itemName_Count_Map.Add(zeItem.item_name, zeItem);
            return true;
        }
        return false;
    }
}
