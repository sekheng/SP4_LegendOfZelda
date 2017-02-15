using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;

/// <summary>
/// Used to load data from SQLite Table into the Player data!
/// </summary>
public class LoadPlayerScript : MonoBehaviour {
    private string playerTableStr = "PlayerStuff";
    [Tooltip("The Player Save ID so as to load from the table")]
    public int playerID = 1;

    private string connectionString;

	// Use this for initialization and loading of data from SQLite Table!
	void Start () {
        connectionString = "URI=file:" + Application.dataPath + "/DataBase/AllData.db";
        GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");

        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(connectionString);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery2 = "SELECT * FROM " + playerTableStr + " WHERE PlayerID = " + playerID;
        dbcmd.CommandText = sqlQuery2;
        IDataReader reader = dbcmd.ExecuteReader();
        //Debug.Log("Reading SQL Table");
        if (reader.Read())
        {
            // The Order is Important!
            float theHealth = reader.GetFloat(1);
            thePlayer.GetComponent<HealthScript>().m_health = theHealth;
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
