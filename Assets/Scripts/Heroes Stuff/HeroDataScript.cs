using UnityEngine;
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
    // Unfortunately, everything has become so hardcoded to the point that it has to come to this!
    private bool m_GotHealth = false;

	// Use this for initialization
	void Start () {
        herosHealth = GetComponent<HealthScript>();
        //Debug.Log("Get the persistent health");
        //herosHealth.resetToMaxHealth(PersistentHealthScript.instance.getMaxHealth());
        //herosHealth.m_health = PersistentHealthScript.instance.currHealth;
        //Debug.Log("Succeed in getting the persistent health");
        //string[] zeCondition = {"PlayerID = " + m_playerID};
        //herosHealth.m_health = MySQLiteHandler.instance.getFloat(playerTableName, "PlayerHealth", zeCondition);
        getHealthFromPersistent();
	}

    // So that it won't be so intensive on the Update loop!
    void FixedUpdate()
    {
        getHealthFromPersistent();
    }

    // When this script is about to destroy, send it to the singleton!
    void OnDisable()
    {
        PersistentHealthScript.instance.currHealth = herosHealth.m_health;
    }

    void getHealthFromPersistent()
    {
        switch (m_GotHealth)
        {
            case false:
                if (PersistentHealthScript.instance != null)
                {
                    m_GotHealth = true;
                    herosHealth.resetToMaxHealth(PersistentHealthScript.instance.getMaxHealth());
                    herosHealth.m_health = PersistentHealthScript.instance.currHealth;
                }
                break;
            default:
                break;
        }
    }
}
