using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {
    // For the item information
    public itemInformation m_itemInform = new itemInformation();

    [Tooltip("Item's name")]
    public string m_itemName;
    [Tooltip("Item's type")]
    public string m_itemType;
    [Tooltip("Item's effect")]
    public string m_itemEffect;
    [Tooltip("How many items")]
    public int m_numOfItems = 1;

	// Use this for initialization
	void Start () {
        m_itemInform.item_name = m_itemName;
        m_itemInform.item_type = m_itemType;
        m_itemInform.item_effect = m_itemEffect;
        m_itemInform.item_count = m_numOfItems;
	}
}
