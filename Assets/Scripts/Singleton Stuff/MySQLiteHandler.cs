﻿using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

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
                cantTouchThiz = FindObjectOfType<MySQLiteHandler>();
                cantTouchThiz.Awake();
            }
            return cantTouchThiz;
        }
    }

    private bool hasInitialized = false;

	// Use this for initialization
	void Awake () {
        if (!hasInitialized)
            hasInitialized = true;
        else
            return;
        //Debug.Log(String.Format("INSERT INTO PlaceSequence(PlayerDamage,Name) VALUES(\"{0}\",\"{1}\")", 100, String.Format("YOLO")));
        string actualDBFilePath;
#if UNITY_ANDROID
        //Debug.Log(Application.persistentDataPath);
        actualDBFilePath = Application.persistentDataPath + "/AllData.db";
        connectionDB = "URI=file:" + actualDBFilePath;
        Debug.Log("Android filepath: " + actualDBFilePath);
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
        }
        //Debug.Log("Initializing the DB connection in Android");
#else
        actualDBFilePath = Application.dataPath + "/StreamingAssets/AllData.db";
        connectionDB = "URI=file:" + actualDBFilePath;
#endif
        dbconn = (IDbConnection)new SqliteConnection(connectionDB);
        //dbconn.Open(); //Open connection to the database.
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
    public int getInteger(string zeTableName, string zeCol, string[] zeCondition = null)
    {
        dbconn.Open();
        int zeVal = 0;
        dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT " + zeCol + " FROM " + zeTableName;
        if (zeCondition != null)
        {
            sqlQuery += " WHERE ";
            foreach (string zeCond in zeCondition)
            {
                sqlQuery += zeCond;
            }
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
        dbconn.Close();
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
    /// the specific condition to get the table! Default to be null
    /// </param>
    /// <returns></returns>
    public float getFloat(string zeTableName, string zeCol, string[] zeCondition = null)
    {
        //Debug.Log(zeTableName + "," + zeCol);
        float zeVal = 0;
        dbconn.Open();
        dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT " + zeCol + " FROM " + zeTableName;
        if (zeCondition != null)
        {
            sqlQuery += " WHERE ";
            foreach (string zeCond in zeCondition)
            {
                sqlQuery += zeCond;
            }
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
        dbconn.Close();
        return zeVal;
    }

    /// <summary>
    /// Get the whole string array from the table
    /// </summary>
    /// <param name="zeTable">
    ///  The Table to get the string
    /// </param>
    /// <param name="numOfField">
    /// Number of coloumns are there
    /// </param>
    /// <param name="allTheField">
    /// The field type depending on the given number of fields. Can't think of any other simper ways to make life easier
    /// </param>
    /// <param name="zeConditions">
    /// The condition that you have put in. It will be treated as AND operator for every condition put in.
    /// </param>
    /// <returns>
    /// For every row, there will be a string.
    /// For every coloumn in the row, it will be separated with a comma
    /// return the whole string array
    /// </returns>
    public string[] getAllStringFromTable(string zeTable, int numOfField, List<object> allTheField, List<string> zeConditions = null)
    {
        //Text zeDebugginText = GameObject.Find("DEBUGGINGTEXTUI").GetComponent<Text>();

        List<string> AllTheResult = new List<string>();
        dbconn.Open();
        dbcmd = dbconn.CreateCommand();
        //if (zeTable.Equals("ItemStuff"))
        //{
        //zeDebugginText.text = "Creating Command";
        //}

        string sqlQuery = "SELECT * FROM " + zeTable;
        if (zeConditions != null)
        {
            sqlQuery += " WHERE ";
            bool gotMoreConditions = false; // So that subsequent conditions aside the 1st one, will have the AND operator!
            foreach (string zeCond in zeConditions)
            {
                if (gotMoreConditions)
                    sqlQuery += "AND ";
                sqlQuery += zeCond + " ";
                gotMoreConditions = true;
            }
        }
        //Debug.Log("The Command in getAllStringFromTable: " + sqlQuery);
        //zeDebugginText.text = sqlQuery;
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        //if (zeTable.Equals("ItemStuff"))
        //{
        //    zeDebugginText.text = zeTable + ", " + numOfField;
        //}
        while (reader.Read())
        {
            string zeWholeRow = "";
            for (int zeNum = 0; zeNum < numOfField; ++zeNum)
            {
                //if (zeTable.Equals("ItemStuff"))
                //{
                //    zeDebugginText.text = "Going through the table: " + zeNum;
                //}
                // Need to make sure that the subsequent field are divided by comma
                if (zeNum != 0)
                {
                    zeWholeRow += ",";
                }
                if (allTheField[zeNum] is string)
                {
                    //if (zeTable.Equals("ItemStuff"))
                    //{
                    //    zeDebugginText.text = "Getting string from " + zeTable + " at " + zeNum + ", " + allTheField.Count;
                    //}
                    zeWholeRow += reader.GetString(zeNum);
                    //if (zeTable.Equals("ItemStuff"))
                    //    zeDebugginText.text = zeWholeRow;
                }
                else if (allTheField[zeNum] is int)
                {
                    zeWholeRow += reader.GetInt32(zeNum);
                    //if (zeTable.Equals("ItemStuff"))
                    //    zeDebugginText.text = zeWholeRow;
                }
                else if (allTheField[zeNum] is float)
                {
                    zeWholeRow += reader.GetFloat(zeNum);
                    //if (zeTable.Equals("ItemStuff"))
                    //    zeDebugginText.text = zeWholeRow;
                }
            }
            //if (zeTable.Equals("ItemStuff"))
                //zeDebugginText.text = zeWholeRow;
            AllTheResult.Add(zeWholeRow);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        return AllTheResult.ToArray();
    }

    /// <summary>
    /// Updating the specific result to the table 
    /// </summary>
    /// <param name="zeTable">
    /// The exact table
    /// </param>
    /// <param name="zeCol">
    /// The exact Coloumn
    /// </param>
    /// <param name="zeVal">
    /// The value to be stored
    /// </param>
    /// <param name="zeCondition">
    /// The Condition. Null as default
    /// All the conditions will be treated as AND operator!
    /// </param>
    public void saveSpecificResult(string zeTable, string zeCol, string zeVal, List<string> zeCondition = null)
    {
        dbconn.Open();
        dbcmd = dbconn.CreateCommand();
        string sqlQuery = "UPDATE " + zeTable + " SET " + zeCol + " = " + zeVal;
        // If the condition is not empty, then put in the condition!
        if (zeCondition != null)
        {
            sqlQuery += " WHERE ";
            bool checkWhetherIsAnotherCondition = false;
            foreach (string zeStr in zeCondition)
            {
                // This will helps to check for whether is there 2nd or more conditions
                if (checkWhetherIsAnotherCondition)
                {
                    sqlQuery += "AND ";
                }
                sqlQuery += zeStr + " ";
                checkWhetherIsAnotherCondition = true;
            }
        }
        //Debug.Log("Executing saveSpecificResult Query: " + sqlQuery);
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteScalar();
        dbcmd.Dispose();
        dbconn.Close();
    }
    /// <summary>
    /// This will help to convert string usable in SQL
    /// </summary>
    /// <param name="zeStr"></param>
    /// <returns></returns>
    public string helpToConvertToSQLString(string zeStr)
    {
        return String.Format("\"{0}\"", zeStr);
    }

    //void OnDestroy()
    //{
    //    dbconn.Close();
    //    dbconn = null;
    //}
}

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

