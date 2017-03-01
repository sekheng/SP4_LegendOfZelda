using UnityEngine;
using System.Collections;

/// <summary>
/// This script is to track the number of health the player has.
/// Make sure the player's health will not restart!
/// </summary>
public class PersistentHealthScript : MonoBehaviour {

    private static PersistentHealthScript CantTouchThis;

    public static PersistentHealthScript instance
    {
        get
        {
            if (CantTouchThis == null)
            {
                CantTouchThis = FindObjectOfType<PersistentHealthScript>();
                CantTouchThis.Start();
            }
            return CantTouchThis;
        }
    }

    // Make sure the player's health won't change!
    private float max_health = 100;
    public float currHealth = 100;
    // The table where player can load their health from in SQLite
    [HideInInspector]
    public static string playerTableName = "PlayerStuff";
    private bool hasInitialized = false;

	// Use this for initialization
	void Start () {
        //Debug.Log("Starting the persistent health");
        //if (!hasInitialized)
        //{
        //    string[] zeCondition = { "PlayerID = " + HeroDataScript.m_playerID };
        //    max_health = MySQLiteHandler.instance.getFloat(playerTableName, "PlayerMaxHealth", zeCondition);
        //    currHealth = MySQLiteHandler.instance.getFloat(playerTableName, "PlayerHealth", zeCondition);
        //    hasInitialized = true;
        //}

        switch (HeroDataScript.m_playerID)
        {
            case 0:
                // We shall default the health to be 100 if it is a new game!
                currHealth = max_health = 100;
                break;
            default:
                // load data from SQLite
                loadPlayerHealthFromSQLite();
                break;
        }
    }

    public float getMaxHealth()
    {
        return max_health;
    }

    /// <summary>
    /// Use this when u begin to load data from the Load Game!
    /// </summary>
    public void loadPlayerHealthFromSQLite()
    {
        string[] zeCondition = { "PlayerID = " + HeroDataScript.m_playerID };
        max_health = MySQLiteHandler.instance.getFloat(playerTableName, "PlayerMaxHealth", zeCondition);
        currHealth = MySQLiteHandler.instance.getFloat(playerTableName, "PlayerHealth", zeCondition);
        hasInitialized = true;
    }
}
