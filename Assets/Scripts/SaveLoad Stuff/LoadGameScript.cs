using UnityEngine;
using System.Collections;

/// <summary>
/// This will based on what the player decide becuz ain't no time
/// 0 will be the new game while numbers between 1 and 3 will be the load data!
/// </summary>
public class LoadGameScript : MonoBehaviour {
    [Tooltip("What save data do you want to load")]
    public int m_loadNumber = 0;

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
        Debug.Log("Player Controller: " + zeDebugPlayerController.name);
        zeDebugPlayerController.BroadcastMessage("Start");
        switch (m_loadNumber)
        {
            case 0:
                // when the player starts a new game, then just go next!
                LocalDataSingleton.instance.GoNext();
                break;
            default:
                // For now, the data should be proceeding to the next scene
                    LocalDataSingleton.instance.GoNext();
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

    }
}
