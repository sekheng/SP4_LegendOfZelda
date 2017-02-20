using UnityEngine;
using System.Collections;

/// <summary>
/// This script is to track the number of health the player has.
/// Make sure the player's health will not restart!
/// </summary>
public class PersistentHealthScript : MonoBehaviour {
    public static PersistentHealthScript instance = null;

    // Make sure the player's health won't change!
    private float max_health;
    public float currHealth
    {
        get;
        set;
    }
    private static string playerTableName = "PlayerStuff";

	// Use this for initialization
	void Start () {
        instance = this;
        string[] zeCondition = { "PlayerID = " + HeroDataScript.m_playerID };
        max_health = MySQLiteHandler.instance.getFloat(playerTableName, "PlayerHealth", zeCondition);
        currHealth = max_health;
    }

    public float getMaxHealth()
    {
        return max_health;
    }
}
