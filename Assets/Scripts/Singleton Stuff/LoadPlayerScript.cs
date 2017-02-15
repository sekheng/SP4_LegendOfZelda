using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;

/// <summary>
/// Used to load data from SQLite Table into the Player data!
/// </summary>
public class LoadPlayerScript : MonoBehaviour {
    private string playerTableStr = "PlayerStuff";
    [Tooltip("The Player Save ID so as to load from the table")]
    public int playerID = 1;

    private string connectionString;

    [Tooltip("Removing it soon!")]
    public GameObject zeDebugMesh;

	// Use this for initialization and loading of data from SQLite Table!
	void Start () {
        string actualDBFilePath;
#if UNITY_ANDROID
        actualDBFilePath = Application.dataPath + "/DataBase/AllData.db";
        if (!File.Exists(actualDBFilePath))
        {
            //int countLoop = 0;
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/DatabBase/AllData.db");
            zeDebugMesh.GetComponent<TextMesh>().text = "going into API";
            // Need to make sure it is not stuck forever!
            while (!loadDB.isDone
                //&& countLoop < 10000
                )
            {
                //countLoop++;
            }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(actualDBFilePath, loadDB.bytes);
            zeDebugMesh.GetComponent<TextMesh>().text = "Succesful";
        }
#else
        actualDBFilePath = Application.dataPath + "/DataBase/AllData.db";
#endif
        connectionString = "URI=file:" + actualDBFilePath;
        GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");

        IDbConnection dbconn;
        zeDebugMesh.GetComponent<TextMesh>().text = "Trying to read from DB";
        dbconn = (IDbConnection)new SqliteConnection(connectionString);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery2 = "SELECT * FROM " + playerTableStr + " WHERE PlayerID = " + playerID;
        dbcmd.CommandText = sqlQuery2;
        IDataReader reader = dbcmd.ExecuteReader();
        //Debug.Log("Reading SQL Table");
        zeDebugMesh.GetComponent<TextMesh>().text = "Reading SQL Table";
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
