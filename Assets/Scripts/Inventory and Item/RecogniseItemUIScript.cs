using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// This will be used to display how many items in the UI based on the number of OBJs the player has
/// </summary>
public class RecogniseItemUIScript : MonoBehaviour {
    [Tooltip("You will have to put the items here then it can display the items in the Inventory UI")]
    public List<GameObject> allItemPrefabs = new List<GameObject>();
    // To access the player's inventory!
    private PlayerInventoryScript playerInventory;
    // need to know how many slots are there!
    [HideInInspector]
    public List<slotstuff> allTheSlots = new List<slotstuff>();
    private int m_AvailableSlots = 0;

    // 1st, we need to find how many slots are there!
    void Awake()
    {
        gameObject.GetComponentsInChildren(allTheSlots);
        // Need to know how many slots are there!
        //m_AvailableSlots = allTheSlots.Count;
        //Debug.Log("The No. of slots: " + m_AvailableSlots);
    }

    // Using this function will allows the restart of finding item in the inventory again!
    void OnEnable()
    {
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventoryScript>();
        }
        //Debug.Log("Gonna insert items into the UI: " + playerInventory.itemName_Count_Map.Count);
        // Trying to get the key and value then put it in the slot!
        foreach (KeyValuePair<string,itemInformation> zeNameAndItem in playerInventory.itemName_Count_Map)
        {
            Debug.Log("Trying to insert items into the UI");
            // Need to make sure that the items count are more than 0!
            if (zeNameAndItem.Value.item_count > 0)
            {
                GameObject zeItemUI = null;
                foreach (GameObject zeGO in allItemPrefabs)
                {
                    zeItemUI = Instantiate(zeGO);
                    zeItemUI.BroadcastMessage("Start"); // Need to initialize the gameobject
                    ItemScript zeItemInform = zeItemUI.GetComponentInChildren<ItemScript>();
                    //Debug.Log("The GO: " + zeItemUI.name);
                    //Debug.Log("The item's name: " + zeNameAndItem.Value.item_name);
                    //Debug.Log("GO item's inform: " + zeItemInform.m_itemInform.item_name);
                    if (zeItemInform.m_itemInform.item_name.Equals(zeNameAndItem.Value.item_name))
                    {
                        // We will make a copy of the item!
                        break;
                    }
                    else
                    {
                        DestroyObject(zeItemUI);
                    }
                }
                //allTheSlots[m_AvailableSlots].transform.A;
                //Debug.Log("The Items: " + zeItemUI.name);
                zeItemUI.GetComponent<RectTransform>().SetParent(allTheSlots[m_AvailableSlots].GetComponent<RectTransform>(),false);
                ++m_AvailableSlots;
            }
        }
    }
    // This is to remove the items from the slot!
    void OnDisable()
    {
        m_AvailableSlots = 0;
        // We will also need to delete all the UI GameObject 
        foreach (slotstuff zeSlot in allTheSlots)
        {
            ItemScript zeItem = zeSlot.gameObject.GetComponentInChildren<ItemScript>();
            // If there is such an item, destroy the gameobject
            if (zeItem != null)
            {
                DestroyObject(zeItem.gameObject);
            }
        }
    }
}
