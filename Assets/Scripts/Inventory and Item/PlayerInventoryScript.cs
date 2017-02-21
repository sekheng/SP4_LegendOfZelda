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
    
    [Tooltip("Set the name of the player's inventory table from the database")]
    public  string PlayerInventoryTable = "PlayerInventoryTable";

    // To know what item does the player has on hand
    public Dictionary<string, itemInformation> itemName_Count_Map = new Dictionary<string, itemInformation>();

    // This is to notify that the quest is completed if all the relics have been collected!
    private QuestItemScrpt checkForRelics;

    void Start()
    {
        checkForRelics = GetComponent<QuestItemScrpt>();
        List<string> myConditions = new List<string>();
        myConditions.Add("PlayerID = " + HeroDataScript.m_playerID);
        List<object> theFieldToTake = new List<object>();
        theFieldToTake.Add((int)1);
        theFieldToTake.Add("LOL");
        theFieldToTake.Add((int)1);
        string[] allZeStuff = MySQLiteHandler.instance.getAllStringFromTable(PlayerInventoryTable, 3, theFieldToTake, myConditions);
        foreach (string zeStr in allZeStuff)
        {
            //Debug.Log(PlayerInventoryTable + ": " + zeStr);
            string[] allZeItemStr = zeStr.Split(',');
            // Item Count is int 3rd row, we shall check if that is more than 0. If so, add it to the inventory!
            int zeItemCount;
            int.TryParse(allZeItemStr[2], out zeItemCount);
            if (zeItemCount > 0)
            {
                // Since the string will be in the 2nd row, we will take that!
                itemInformation zeNewItem = new itemInformation(ItemGeneratorScript.instance.getItemInform(allZeItemStr[1]));
                zeNewItem.item_count = zeItemCount;
                passInInventory(zeNewItem);
            }
        }
        //Debug.Log("Total Inventory space: " + itemName_Count_Map.Count);
    }

    public bool passInInventory(itemInformation zeItem)
    {
        itemInformation toCheckWhetherItExistInsideInventory;
        // Add to the stacks of items!
        if (itemName_Count_Map.TryGetValue(zeItem.item_name, out toCheckWhetherItExistInsideInventory))
        {
            toCheckWhetherItExistInsideInventory.item_count++;
            //Debug.Log("Stacking item: " + zeItem.item_name);
            checkForRelics.notifyItemCollected(zeItem);
            return true;
        }
        // If still can contain more inventory space, then add it in!
        if (itemName_Count_Map.Count < max_InventorySpace)
        {
            //Debug.Log("Adding item: " + zeItem.item_name);
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
