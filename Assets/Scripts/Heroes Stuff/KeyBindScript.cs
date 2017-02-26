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
    private static string theSQLiteTable = "GlobalSettingTable";

	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
        // Turn off this component since it doesn't matter in android phone!
        enabled = false;
#else
        // We shall begin reading of the Keybind from SQLite!
        //Debug.Log("Reading from SQLite for keybinds");
        // hardcoding at ze best!
        upKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, "UpKeycode");
        downKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, "DownKeycode");
        rightKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, "RightKeycode");
        leftKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, "LeftKeycode");
        attackKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, "AttackKeycode");
        rangeKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, "RangeKeycode");
        inventoryKey = (KeyCode)MySQLiteHandler.instance.getInteger(theSQLiteTable, "InventoryKeycode");
        //Debug.Log("Successful Reading from SQLite for keybinds");
#endif
	}
}
