using UnityEngine;
using System.Collections;

/// <summary>
/// This script is to track the number of health the player has.
/// Make sure the player's health will not restart!
/// </summary>
public class PersistentHealthScript : MonoBehaviour {
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
    private static PersistentHealthScript CantTouchThis;

    // Make sure the player's health won't change!
    private float max_health = 100;
    public float currHealth = 100;
    private static string playerTableName = "PlayerStuff";
    private bool hasInitialized = false;

	// Use this for initialization
	void Start () {
        //Debug.Log("Starting the persistent health");
        if (!hasInitialized)
        {
            string[] zeCondition = { "PlayerID = " + HeroDataScript.m_playerID };
            max_health = MySQLiteHandler.instance.getFloat(playerTableName, "PlayerHealth", zeCondition);
            currHealth = max_health;
            hasInitialized = true;
        }
    }

    public float getMaxHealth()
    {
        return max_health;
    }
}
