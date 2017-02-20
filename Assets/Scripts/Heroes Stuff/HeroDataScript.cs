﻿using UnityEngine;
using System.Collections;

/// <summary>
/// To Load Heroe's information from the database!
/// </summary>
public class HeroDataScript : MonoBehaviour {
    [Tooltip("The PlayerID in the database")]
    public static int m_playerID = 1;
    // Table name for the player information in the database!
    private static string playerTableName = "PlayerStuff";
    // Hero's health component
    private HealthScript herosHealth;

	// Use this for initialization
	void Start () {
        herosHealth = GetComponent<HealthScript>();
        herosHealth.resetToMaxHealth(PersistentHealthScript.instance.getMaxHealth());
        herosHealth.m_health = PersistentHealthScript.instance.currHealth;
        //string[] zeCondition = {"PlayerID = " + m_playerID};
        //herosHealth.m_health = MySQLiteHandler.instance.getFloat(playerTableName, "PlayerHealth", zeCondition);
	}

    // When this script is about to destroy, send it to the singleton!
    void OnDisable()
    {
        PersistentHealthScript.instance.currHealth = herosHealth.m_health;
    }
}
