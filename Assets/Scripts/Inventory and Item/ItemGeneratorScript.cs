using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// This will be used to generate items and pass it to the player so that it won't place too much burden on the table!
/// </summary>
public class ItemGeneratorScript : MonoBehaviour {
    // To access this singleton easily
    public static ItemGeneratorScript instance
    {
     get
        {
            if (cantTouchThis == null)
            {
                cantTouchThis = FindObjectOfType<ItemGeneratorScript>();
                // Need to initialize it!
                cantTouchThis.Start();
            }
            return cantTouchThis;
        }
    }
    private static ItemGeneratorScript  cantTouchThis;

    [Tooltip("Name of the table!")]
    public string m_tableName = "ItemStuff";
    // To see whether it has been initialized before!
    private bool isItInitialized = false;
    // To contain all the item's informatiom. There won't be anything missing!
    private Dictionary<string, itemInformation> allTheItemThatWillExist = new Dictionary<string, itemInformation>();

    public List<GameObject> allThePossibleItems = new List<GameObject>();

	void Start () {
        if (!isItInitialized)
        {
            // an alternative way than loading from database
            foreach (GameObject zeGO in allThePossibleItems)
            {
                //GameObject zeOtherGO = Instantiate(zeGO);
                //Debug.Log(zeOtherGO.name);
                ItemScript zeItem = Instantiate(zeGO.GetComponent<ItemScript>());
                zeItem.SendMessage("Start");
                if (!allTheItemThatWillExist.ContainsKey(zeItem.m_itemName))
                    allTheItemThatWillExist.Add(zeItem.m_itemName, new itemInformation(zeItem.m_itemInform));
                Destroy(zeItem.gameObject);
            }
            //Debug.Log("Total Item exists: " + allTheItemThatWillExist.Count);
            //Text zeDebugginText = GameObject.Find("DEBUGGINGTEXTUI").GetComponent<Text>();
            //List<object> zeFields = new List<object>();
            //zeFields.Add("LOL");
            //zeFields.Add("LOL");
            //zeFields.Add("LOL");
            //zeDebugginText.text = "Trying to get table: " + m_tableName + ", Number of fields: " + zeFields.Count;
            //Debug.Log("Trying to get table: " + m_tableName + ", Number of fields: " + zeFields.Count);
            //string[] allZeItem = MySQLiteHandler.instance.getAllStringFromTable(m_tableName, 3, zeFields);
            //zeDebugginText.text = "Successful getting from table";
            //Debug.Log("Successful getting from table");
            //foreach (string zeItemStr in allZeItem)
            //{
            //    //Debug.Log("Item recorded: " + zeItemStr);
            //    string[] allzeItemStuff = zeItemStr.Split(',');
            //    // There are a total of 3 hardcoded fields so it will be simper
            //    // 1st: ItemName
            //    itemInformation zeItemInform = new itemInformation();
            //    zeItemInform.item_name = allzeItemStuff[0];
            //    // 2nd: Item Type
            //    zeItemInform.item_type = allzeItemStuff[1];
            //    // 3rd: Item description / effect
            //    zeItemInform.item_effect = allzeItemStuff[2];
            //    allTheItemThatWillExist.Add(zeItemInform.item_name, zeItemInform);
            //    //Debug.Log("Item Name: " + zeItemInform.item_name);
            //    //Debug.Log("Item Type: " + zeItemInform.item_type);
            //    //Debug.Log("Item Effect: " + zeItemInform.item_effect);
            //}
            ////Debug.Log("Total Unique items: " + allTheItemThatWillExist.Count);
            //isItInitialized = true;
            //zeDebugginText.text = "Total recorded items: " + allTheItemThatWillExist.Count;

        }
    }

    //void FixedUpdate()
    //{
    //    Start();
    //}

    public itemInformation getItemInform(string zeName)
    {
        // The unfortunate thing to do
        //Start();
        itemInformation zeItemInform;
        //Debug.Log("The key: " + zeName);
        //Debug.Log(allTheItemThatWillExist.Count);
        //foreach (KeyValuePair<string, itemInformation> zePair in allTheItemThatWillExist)
        //{
        //    Debug.Log("Item's name: " + zePair.Key);
        //}
        allTheItemThatWillExist.TryGetValue(zeName, out zeItemInform);
        //Debug.Log("item name: " + zeItemInform.item_name);
        return zeItemInform;
    }
}
