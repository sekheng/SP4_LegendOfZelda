using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Hardcoded for now, as there is no time to do other stuff!
/// So there will be 5 relics for now!
/// </summary>
public class QuestItemScrpt : MonoBehaviour {
    [Tooltip("The number of relics")]
    public int m_numberOfRelics = 5;
    [Tooltip("What is the overall QuestName")]
    public string m_questItemName = "Relic";

    // This will help check for unique item name and prevent the same copy!
    private HashSet<string> theQuestItemName = new HashSet<string>();
    // The table to player's inventory!
    private static string tableString = "PlayerInventoryTable";

    public bool checkQuestItemsCollected()
    {
        if (theQuestItemName.Count == m_numberOfRelics)
            return true;
        return false;
    }

    public void notifyItemCollected(itemInformation zeQuestItem)
    {
        Debug.Log("Notifying quest item: " + zeQuestItem.item_name);
        if (zeQuestItem.item_name.Contains(m_questItemName))
        {
            theQuestItemName.Add(zeQuestItem.item_name);
            Debug.Log("Successful notify quest item: " + zeQuestItem.item_name);
            Debug.Log("Total Quest items: " + theQuestItemName.Count);
            List<string> myConditions = new List<string>();
            //myConditions.Add("PlayerID = " + HeroDataScript.m_playerID);
            //myConditions.Add("ItemName = " + MySQLiteHandler.instance.helpToConvertToSQLString(zeQuestItem.item_name));
            //MySQLiteHandler.instance.saveSpecificResult(tableString, "ItemCount", zeQuestItem.item_count.ToString(), myConditions);
            if (checkQuestItemsCollected())
            {
                // Then we shall proceed to the next level!
                LocalDataSingleton.instance.GoNext();
            }
        }
    }

    public void resetQuestItemList()
    {
        theQuestItemName.Clear();
    }
    public int getCurrenNumOfQuestItems()
    {
        return theQuestItemName.Count;
    }
}
