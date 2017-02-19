using UnityEngine;
using System.Collections;

/// <summary>
/// A simper item interaction with the player.
/// The issue will be how will the player receive an item and where will it be placed
/// </summary>
public class ItemInteractionScript : MonoBehaviour {
    // This is for player to interact with the item!
    private PlayerInventoryScript playerInventory;
    // All item's has their own item information to pass it to the inventory!
    private itemInformation theItemStuff;

    void Start()
    {
        // Since there will only be 1 inventory at any point of time
        playerInventory = FindObjectOfType<PlayerInventoryScript>();
        theItemStuff = GetComponent<itemInformation>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Items is for player ultimately. It will be trouble if monster can even pick up the stuff!
        if (other.tag.Equals("Player"))
        {
            // Need to find the inventory again if can't find it!
            if (playerInventory == null)
                Start();
            Debug.Log("Interacting with: " + other.name);
        }
    }
}
