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
    private ItemScript theItemStuff;
    [Tooltip("The particle system for item interaction")]
    public string m_ParticleSystemName = "ParticleFX_ItemCollected";
    // The particle system
    private ParticleScript itemInteractParticles;
    [Tooltip("How much health do you want to heal the player")]
    public float m_heal = 20.0f;

    void Start()
    {
        // Since there will only be 1 inventory at any point of time
        if (playerInventory == null)
            playerInventory = FindObjectOfType<PlayerInventoryScript>();
        if (theItemStuff == null)
            theItemStuff = GetComponent<ItemScript>();
        if (itemInteractParticles == null)
            itemInteractParticles = GameObject.Find(m_ParticleSystemName).GetComponent<ParticleScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Items is for player ultimately. It will be trouble if monster can even pick up the stuff!
        if (other.tag.Equals("Player"))
        {
            // Need to find the inventory again if can't find it!
            if (playerInventory == null)
                Start();
            if (theItemStuff == null)
                Start();
            Debug.Log("Interacting with: " + other.name);
            playerInventory.passInInventory(theItemStuff.m_itemInform);

            // We also need to give the player back some health!
            HealthScript zePlayerHealth = other.GetComponent<HealthScript>();
            zePlayerHealth.modifyHealth(m_heal);

            itemInteractParticles.transform.position = transform.position;
            itemInteractParticles.playEffect();
            Destroy(gameObject);
        }
    }
}
