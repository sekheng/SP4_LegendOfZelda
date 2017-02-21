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
    private List<RectTransform> allTheSlots = new List<RectTransform>();
    private int m_AvailableSlots = 0;

    // 1st, we need to find how many slots are there!
    void Awake()
    {
        gameObject.GetComponentsInChildren(allTheSlots);
        // Need to remove it's rect transform from the array!
        allTheSlots.Remove(GetComponent<RectTransform>());
        // Need to know how many slots are there!
        m_AvailableSlots = allTheSlots.Count;
    }

    // Using this function will allows the restart of finding item in the inventory again!
    void OnEnable()
    {
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventoryScript>();
        }
        Debug.Log("Gonna insert items into the UI: " + playerInventory.itemName_Count_Map.Count);
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
                    ItemScript zeItemInform = zeGO.GetComponent<ItemScript>();
                    if (zeItemInform.m_itemInform.item_name.Equals(zeNameAndItem.Value.item_name))
                    {
                        // We will make a copy of the item!
                        zeItemUI = Instantiate(zeGO);
                        break;
                    }
                }
                //allTheSlots[m_AvailableSlots].transform.A;
                Debug.Log("The Items: " + zeItemUI.name);
                zeItemUI.transform.SetParent(allTheSlots[m_AvailableSlots]);
                ++m_AvailableSlots;
            }
        }
    }
    // This is to remove the items from the slot!
    void OnDisable()
    {

    }
}
