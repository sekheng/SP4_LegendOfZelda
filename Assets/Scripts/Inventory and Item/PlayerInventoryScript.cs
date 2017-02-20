using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Keep track of what item does the player has!
/// All similar items can stack for now!
/// </summary>
[RequireComponent(typeof(QuestItemScrpt))]
public class PlayerInventoryScript : MonoBehaviour {
    [Tooltip("For now, just in case we want limited number of inventory in the space")]
    public int max_InventorySpace = 10;
    
    private static string PlayerInventoryTable = "PlayerInventoryTable";

    // To know what item does the player has on hand
    private Dictionary<string, itemInformation> itemName_Count_Map = new Dictionary<string, itemInformation>();

    // This is to notify that the quest is completed if all the relics have been collected!
    private QuestItemScrpt checkForRelics;

    void Start()
    {
        checkForRelics = GetComponent<QuestItemScrpt>();
    }

    public bool passInInventory(itemInformation zeItem)
    {
        itemInformation toCheckWhetherItExistInsideInventory;
        // Add to the stacks of items!
        if (itemName_Count_Map.TryGetValue(zeItem.item_name, out toCheckWhetherItExistInsideInventory))
        {
            toCheckWhetherItExistInsideInventory.item_count++;
            Debug.Log("Stacking item: " + zeItem.item_name);
            checkForRelics.notifyItemCollected(zeItem);
            return true;
        }
        // If still can contain more inventory space, then add it in!
        if (itemName_Count_Map.Count < max_InventorySpace)
        {
            Debug.Log("Adding item: " + zeItem.item_name);
            checkForRelics.notifyItemCollected(zeItem);
            itemName_Count_Map.Add(zeItem.item_name, zeItem);
            return true;
        }
        return false;
    }
    public bool checkForAvailableInventory(string zeItemName)
    {
        itemInformation zeItemInform;
        // Need to check if the item is still thr
        if (itemName_Count_Map.TryGetValue(zeItemName, out zeItemInform))
        {
            // Then we need to check whether he has any items!
            if (zeItemInform.item_count > 0)
                return true;
        }
        return false;
    }
    public itemInformation getItem(string zeItemName)
    {
        itemInformation zeItemInform;
        itemName_Count_Map.TryGetValue(zeItemName, out zeItemInform);
        return zeItemInform;
    }
}
