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
                //cantTouchThis.Start();
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

	void OnEnable () {
        if (!isItInitialized)
        {
            Text zeDebugginText = GameObject.Find("DEBUGGINGTEXTUI").GetComponent<Text>();
            List<object> zeFields = new List<object>();
           zeFields.Add("LOL");
           zeFields.Add("LOL");
           zeFields.Add("LOL");
            zeDebugginText.text = "Trying to get table: " + m_tableName + ", Number of fields: " + zeFields.Count;
            Debug.Log("Trying to get table: " + m_tableName + ", Number of fields: " + zeFields.Count);
           string[] allZeItem = MySQLiteHandler.instance.getAllStringFromTable(m_tableName, 3, zeFields);
           zeDebugginText.text = "Successful getting from table";
           Debug.Log("Successful getting from table");
           foreach (string zeItemStr in allZeItem)
           {
               //Debug.Log("Item recorded: " + zeItemStr);
               string []allzeItemStuff = zeItemStr.Split(',');
               // There are a total of 3 hardcoded fields so it will be simper
               // 1st: ItemName
               itemInformation zeItemInform = new itemInformation();
               zeItemInform.item_name = allzeItemStuff[0];
               // 2nd: Item Type
               zeItemInform.item_type = allzeItemStuff[1];
               // 3rd: Item description / effect
               zeItemInform.item_effect = allzeItemStuff[2];
               allTheItemThatWillExist.Add(zeItemInform.item_name, zeItemInform);
               //Debug.Log("Item Name: " + zeItemInform.item_name);
               //Debug.Log("Item Type: " + zeItemInform.item_type);
               //Debug.Log("Item Effect: " + zeItemInform.item_effect);
           }
           //Debug.Log("Total Unique items: " + allTheItemThatWillExist.Count);
           isItInitialized = true;
           zeDebugginText.text = "Total recorded items: " + allTheItemThatWillExist.Count;
        }
    }

    //void FixedUpdate()
    //{
    //    Start();
    //}

    public itemInformation getItemInform(string zeName)
    {
        itemInformation zeItemInform;
        allTheItemThatWillExist.TryGetValue(zeName, out zeItemInform);
        return zeItemInform;
    }
}
