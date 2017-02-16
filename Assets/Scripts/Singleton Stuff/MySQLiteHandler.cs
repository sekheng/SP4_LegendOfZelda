using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;

/// <summary>
/// For now, this SQLite will only work for this game!
/// Because it only satisfy my needs
/// There can only be 1 database at any point of time!
/// This will only be compatible with PC and Android
/// </summary>
public class MySQLiteHandler : MonoBehaviour {
    private string connectionDB;
    private IDbConnection dbconn;
    private IDataReader reader;
    private IDbCommand dbcmd;

    // The Single instance that will exists throughout the game!
    private static MySQLiteHandler cantTouchThiz;

    public static MySQLiteHandler instance
    {
        get
        {
            if (cantTouchThiz == null)
            {
                cantTouchThiz = GameObject.FindObjectOfType<MySQLiteHandler>();
            }
            return cantTouchThiz;
        }
    }

	// Use this for initialization
	void Awake () {
        string actualDBFilePath;
#if UNITY_ANDROID
        Debug.Log(Application.persistentDataPath);
        actualDBFilePath = Application.persistentDataPath + "/AllData.db";
        connectionDB = "URI=file:" + actualDBFilePath;
        if (!File.Exists(actualDBFilePath))
        {
            //int countLoop = 0;
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/AllData.db");
            // Need to make sure it is not stuck forever!
            while (!loadDB.isDone
                //&& countLoop < 10000
                )
            {
                //countLoop++;
            }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(actualDBFilePath, loadDB.bytes);

            dbconn = (IDbConnection)new SqliteConnection(connectionDB);
            dbconn.Open(); //Open connection to the database.

            //dbcmd.Dispose();
            //dbcmd = null;

        }
        else
        {
            dbconn = (IDbConnection)new SqliteConnection(connectionDB);
            dbconn.Open(); //Open connection to the database.
        }
#else
        actualDBFilePath = Application.dataPath + "/StreamingAssets/AllData.db";
        connectionDB = "URI=file:" + actualDBFilePath;
        dbconn = (IDbConnection)new SqliteConnection(connectionDB);
        dbconn.Open(); //Open connection to the database.
#endif
    }
	
    /// <summary>
    /// Returns the integer value from specific table and coloumn
    /// </summary>
    /// <param name="zeTableName">
    /// the table name from the database!
    /// </param>
    /// <param name="zeCol">
    /// the specific coloumn
    /// </param>
    /// <param name="zeCondition">
    /// The specific conditions
    /// </param>
    /// <returns>
    /// returns an integer value if found. return 0 if nothing is found!
    /// </returns>
    public int getInteger(string zeTableName, string zeCol, string[] zeCondition)
    {
        int zeVal = 0;
        dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT " + zeCol + " FROM " + zeTableName;
        sqlQuery += " WHERE ";
        foreach (string zeCond in zeCondition)
        {
            sqlQuery += zeCond;
        }
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        if (reader.Read())
        {
            zeVal = reader.GetInt32(0);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        return zeVal;
    }

    /// <summary>
    /// Get a float value from a specific table and specific coloumn
    /// </summary>
    /// <param name="zeTableName">
    /// Table name for it
    /// </param>
    /// <param name="zeCol">
    /// The specific coloumn
    /// </param>
    /// <param name="zeCondition">
    /// </param>
    /// <returns></returns>
    public float getFloat(string zeTableName, string zeCol, string[] zeCondition)
    {
        float zeVal = 0;
        dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT " + zeCol + " FROM " + zeTableName;
        sqlQuery += " WHERE ";
        foreach (string zeCond in zeCondition)
        {
            sqlQuery += zeCond;
        }
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        if (reader.Read())
        {
            zeVal = reader.GetFloat(0);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        return zeVal;
    }


    void OnDestroy()
    {
        if (dbconn != null)
            dbconn.Close();
    }
}
