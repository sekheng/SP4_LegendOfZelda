using UnityEngine;
using System.Collections;

/// <summary>
/// Used for keybinds! Combined with SQLite! Hardcoding is the fastest way for keybinding and good enough for SP
/// </summary>
public class KeyBindScript : MonoBehaviour {
    // It doesn't matter to show it in inspector since we are reading it from SQLite!
    [HideInInspector]
    public static KeyCode upKey = KeyCode.UpArrow;
    // For going down!
    [HideInInspector]
    public static KeyCode downKey = KeyCode.DownArrow;
    // For going left
    [HideInInspector]
    public static KeyCode leftKey = KeyCode.LeftArrow;
    // For going right
    [HideInInspector]
    public static KeyCode rightKey = KeyCode.RightArrow;
    // For melee attack
    [HideInInspector]
    public static KeyCode attackKey = KeyCode.Z;
    // For range attack
    [HideInInspector]
    public static KeyCode rangeKey = KeyCode.X;
    [HideInInspector]
    public static KeyCode inventoryKey = KeyCode.C;
    // The Table to read from the SQLite!
    [HideInInspector]
    public static string theSQLiteTable = "GlobalSettingTable";
    // The field for upkey
    [HideInInspector]
    public static string upKeyField = "UpKeycode";
    // The field for downkey
    [HideInInspector]
    public static string downKeyField = "DownKeycode";
    // The field for rightkey
    [HideInInspector]
    public static string rightKeyField = "RightKeycode";
    // The field for leftkey
    [HideInInspector]
    public static string leftKeyField = "LeftKeycode";
    // The field for attack Key
    [HideInInspector]
    public static string attackKeyField = "AttackKeycode";
    // The field for range Key
    [HideInInspector]
    public static string rangeKeyField = "RangeKeycode";
    // The field for inventory key
    [HideInInspector]
    public static string inventoryKeyField = "InventoryKeycode";

	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
        // Turn off this component since it doesn't matter in android phone!
        enabled = false;
#else
        // We shall begin reading of the Keybind from SQLite!
        //Debug.Log("Reading from SQLite for keybinds");
        // hardcoding at ze best!
        upKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, upKeyField);
        downKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, downKeyField);
        rightKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, rightKeyField);
        leftKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, leftKeyField);
        attackKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, attackKeyField);
        rangeKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, rangeKeyField);
        inventoryKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, inventoryKeyField);
        //Debug.Log("Successful Reading from SQLite for keybinds");
#endif
	}
}
