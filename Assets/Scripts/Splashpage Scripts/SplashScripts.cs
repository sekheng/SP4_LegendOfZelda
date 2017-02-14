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

    private string connectionString = "URI=file:" + Application.dataPath + "/DataBase/AllData.db";   //Path to database.

	// Use this for initialization
	void Start () {
        //IDbConnection dbconn;
        //dbconn = (IDbConnection)new SqliteConnection(connectionString);
        //dbconn.Open(); //Open connection to the database.
        //IDbCommand dbcmd = dbconn.CreateCommand();
        //string sqlQuery = "INSERT INTO PlaceSequence(Value,PlayerDamage,Name) VALUES(20,100,'YOLO');";
        //dbcmd.CommandText = sqlQuery;
        //IDataReader reader = dbcmd.ExecuteReader();
        //Debug.Log("Saving to SQL Table");
        //reader.Close();
        //reader = null;
        //dbcmd.Dispose();
        //dbcmd = null;
        //dbconn.Close();
        //dbconn = null;

        // Reading from DataBase
        //IDbConnection dbconn;
        //dbconn = (IDbConnection)new SqliteConnection(connectionString);
        //dbconn.Open(); //Open connection to the database.
        ////IDbCommand dbcmd = dbconn.CreateCommand();
        //dbcmd = dbconn.CreateCommand();
        ////string sqlQuery = "SELECT * " + "FROM PlaceSequence";
        //string sqlQuery2 = "SELECT * " + "FROM PlaceSequence";
        //dbcmd.CommandText = sqlQuery2;
        //reader = dbcmd.ExecuteReader();
        //Debug.Log("Reading SQL Table");
        //while (reader.Read())
        //{
        //    int value = reader.GetInt32(0);
        //     int rand = reader.GetInt32(1);
        //   string name = reader.GetString(2);
        //    //string allTheStuff = reader.GetString(3)
        //    Debug.Log("value= " + value + ", Score=" +rand + ", name=" + name);
        //}
        //reader.Close();
        //reader = null;
        //dbcmd.Dispose();
        //dbcmd = null;
        //dbconn.Close();
        //dbconn = null;
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;
        if (timeCounter > m_timer)
            gameObject.GetComponent<GoSceneScript>().GoNextScene(); // Will change soon!
	}
}
