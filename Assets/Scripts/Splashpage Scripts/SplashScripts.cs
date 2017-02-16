using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;

/// <summary>
/// The logic of the splash scene!
/// TODO: Make it complicated!
/// </summary>
public class SplashScripts : MonoBehaviour {
    [Tooltip("Set the time for this splash")]
    public float m_timer = 3.0f;
    private float timeCounter = 0;

    //private string connectionString;   //Path to database.

    // Use this for initialization
	void Start () {
        // connectionString = "URI=file:" + Application.dataPath + "/AllData.db";
        //Debug.Log(Application.dataPath);
        //SaveToDataBase();
        //TryDeleteToDataBase();
        // ReadFromDataBase();
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;
        if (timeCounter > m_timer)
            gameObject.GetComponent<GoSceneScript>().GoNextScene(); // Will change soon!
	}
    //void ReadFromDataBase()
    //{
    //    // Reading from DataBase
    //    IDbConnection dbconn;
    //    dbconn = (IDbConnection)new SqliteConnection(connectionString);
    //    dbconn.Open(); //Open connection to the database.
    //    IDbCommand dbcmd = dbconn.CreateCommand();
    //    string sqlQuery2 = "SELECT * FROM PlaceSequence";
    //    dbcmd.CommandText = sqlQuery2;
    //    IDataReader reader = dbcmd.ExecuteReader();
    //    Debug.Log("Reading SQL Table");
    //    while (reader.Read())
    //    {
    //        // The Order is Important!
    //        int value = reader.GetInt32(0);
    //        int rand = reader.GetInt32(1);
    //        string name = reader.GetString(2);
    //        Debug.Log("value= " + value + ", Score=" + rand + ", name=" + name);
    //    }
    //    reader.Close();
    //    reader = null;
    //    dbcmd.Dispose();
    //    dbcmd = null;
    //    dbconn.Close();
    //    dbconn = null;
    //}

    //void SaveToDataBase()
    //{
    //    IDbConnection dbconn;
    //    dbconn = (IDbConnection)new SqliteConnection(connectionString);
    //    dbconn.Open(); //Open connection to the database.
    //    using (IDbCommand dbcmd = dbconn.CreateCommand())
    //    {
    //        //Debug.Log("Saving to SQL Table");
    //        // This is basically inserting the Coloumn where there is PlayerDamage and Name.
    //        string sqlQuery = String.Format("INSERT INTO PlaceSequence(PlayerDamage,Name) VALUES(\"{0}\",\"{1}\")", 100, String.Format("YOLO"));

    //        //string sqlQuery = String.Format("INSERT INTO PlaceSequence(Value,PlayerDamage) VALUES(\"{0}\",\"{1}\")", 20, 100);
    //        dbcmd.CommandText = sqlQuery;
    //        //Debug.Log("Trying to Save");
    //        dbcmd.ExecuteScalar();
    //        //Debug.Log("Successful Saving");
    //        dbcmd.Dispose();
    //        //dbcmd = null;
    //    }
    //    dbconn.Close();
    //    dbconn = null;
    //}

    //void TryDeleteToDataBase()
    //{
    //    IDbConnection dbconn;
    //    dbconn = (IDbConnection)new SqliteConnection(connectionString);
    //    dbconn.Open(); //Open connection to the database.
    //    using (IDbCommand dbcmd = dbconn.CreateCommand())
    //    {
    //        string sqlQuery = String.Format("DELETE FROM PlaceSequence WHERE Value = \"{0}\"", 2);
    //        dbcmd.CommandText = sqlQuery;
    //        dbcmd.ExecuteScalar();
    //        dbcmd.Dispose();
    //    }
    //    dbconn.Close();
    //    dbconn = null;
    //}
}
