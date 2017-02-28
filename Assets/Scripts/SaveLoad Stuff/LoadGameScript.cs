using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// This will based on what the player decide becuz ain't no time
/// 0 will be the new game while numbers between 1 and 3 will be the load data!
/// </summary>
public class LoadGameScript : MonoBehaviour {
    [Tooltip("What save data do you want to load")]
    public int m_loadNumber = 0;

    // We need to know what scene index field to be saved
    private static string m_SceneIndexField = "SceneIndex";

    /// <summary>
    /// Can be used to load the data!
    /// </summary>
    public void beginLoadingData()
    {
        // If the designer decides to set it to be more than 3 or less than 0, we will put it to 0!
        if (m_loadNumber > 3 || m_loadNumber < 0)
            m_loadNumber = 0;
        HeroDataScript.m_playerID = m_loadNumber;
        // We will also need to restart the data!
        GameObject zeDebugPlayerController = GameObject.FindGameObjectWithTag("GameController");
        //Debug.Log("Player Controller: " + zeDebugPlayerController.name);
        zeDebugPlayerController.BroadcastMessage("Start");
        switch (m_loadNumber)
        {
            case 0:
                // when the player starts a new game, then just go next!
                LocalDataSingleton.instance.GoNext();
                break;
            default:
                // For now, the data should be proceeding to the next scene
                    //LocalDataSingleton.instance.GoNext();
                string []zeConditions = {"PlayerID = " + m_loadNumber};
                LocalDataSingleton.instance.previousSceneFrom = MySQLiteHandler.instance.getInteger(PersistentHealthScript.playerTableName, m_SceneIndexField, zeConditions);
                LocalDataSingleton.instance.ReturntoPrevious();
            break;
        }
    }

    /// <summary>
    /// Used to save data based on the m_loadNumber!
    /// </summary>
    public void saveData()
    {
        // If it is not within the save file, dont save!
        if (m_loadNumber > 3 || m_loadNumber <= 0)
            return;
        // We will go get the player's control and player's current health so that we can save the health and what items the player is carrying!
        GameObject zePlayerController = GameObject.FindGameObjectWithTag("GameController");
        GameObject zePlayer = GameObject.FindGameObjectWithTag("Player");
        // Need to make sure the object exists otherwise it is meaningless!
        if (zePlayer != null && zePlayerController != null)
        {
            // Here is where we save the player's health
            List<string> zeCondtions = new List<string>();
            zeCondtions.Add("PlayerID = " + m_loadNumber);
            // Based on the m_loadNumber, we will save the data to there as player's ID!
            HealthScript zePlayerHealth = zePlayer.GetComponent<HealthScript>();
            MySQLiteHandler.instance.saveSpecificResult(PersistentHealthScript.playerTableName, "PlayerHealth", zePlayerHealth.m_health.ToString(), zeCondtions);
            MySQLiteHandler.instance.saveSpecificResult(PersistentHealthScript.playerTableName, "PlayerMaxHealth", zePlayerHealth.max_health.ToString(), zeCondtions);
            // Followed by the Player's inventory!
            PlayerInventoryScript zePlayerStuff = zePlayerController.GetComponent<PlayerInventoryScript>();
            // We shall then iterate through the itemlist and see if there is any item to be saved!
            foreach (KeyValuePair<string,itemInformation> zeItemNameAndInform in zePlayerStuff.itemName_Count_Map)
            {
                zeCondtions.Clear();
                //Debug.Log("PlayerID = " + m_loadNumber);
                //Debug.Log("ItemName = " + zeItemNameAndInform.Key);
                //Debug.Log("ItemCount = " + zeItemNameAndInform.Value.item_count);
                zeCondtions.Add("PlayerID = " + m_loadNumber);
                zeCondtions.Add("ItemName = " + MySQLiteHandler.instance.helpToConvertToSQLString(zeItemNameAndInform.Key));
                MySQLiteHandler.instance.saveSpecificResult(zePlayerStuff.PlayerInventoryTable, "ItemCount", zeItemNameAndInform.Value.item_count.ToString(), zeCondtions);
            }
            // Finally, we will save the scene index to the SQLite
            zeCondtions.Add("PlayerID = " + m_loadNumber);
            MySQLiteHandler.instance.saveSpecificResult(PersistentHealthScript.playerTableName, m_SceneIndexField, SceneManager.GetActiveScene().buildIndex.ToString(), zeCondtions);
        }
    }
}
