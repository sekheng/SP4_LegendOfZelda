using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This will help to adjust the position of all android inputs using SQLite
/// </summary>
public class AndroidLoadDataScript : MonoBehaviour {
    [Tooltip("Change the joystick button with SQLite!")]
    public GameObject m_joystick;
    [Tooltip("Change the attack button with SQLite!")]
    public GameObject m_attackButton;
    [Tooltip("Change the range button with SQLite!")]
    public GameObject m_rangeButton;
    [Tooltip("Change the inventory button with SQLite!")]
    public GameObject m_InventoryButton;
    [Tooltip("Change the in-game pause button with SQLite!")]
    public GameObject m_PauseButton;
    [Tooltip("The Table which it will be reading from the SQLite. Don't change it if u don't know!")]
    public string m_SQLiteTable = "GlobalSettingTable";

    // This is because only Android phone will be accessing this stuff
#if UNITY_ANDROID
	// Use this for initialization
	void Start () {
        Debug.Log("Beginning reading of android input position");
        if (m_joystick != null)
        {
            string zeJoystickStr = "JoystickPos";
            m_joystick.GetComponent<RectTransform>().localPosition = new Vector3(MySQLiteHandler.instance.getFloat(m_SQLiteTable, zeJoystickStr + "X", null)
                , MySQLiteHandler.instance.getFloat(m_SQLiteTable, zeJoystickStr + "Y", null), 0);
        }
        if (m_attackButton != null)
        {
            string zeAttackStr = "AttackButtonPos";
            m_attackButton.GetComponent<RectTransform>().localPosition = new Vector3(MySQLiteHandler.instance.getFloat(m_SQLiteTable, zeAttackStr + "X", null)
                , MySQLiteHandler.instance.getFloat(m_SQLiteTable, zeAttackStr + "Y", null), 0);
        }
        if (m_rangeButton != null)
        {
            string zeRangeStr = "RangeButtonPos";
            m_rangeButton.GetComponent<RectTransform>().localPosition = new Vector3(MySQLiteHandler.instance.getFloat(m_SQLiteTable, zeRangeStr + "X", null)
                , MySQLiteHandler.instance.getFloat(m_SQLiteTable, zeRangeStr + "Y", null), 0);
        }
        if (m_InventoryButton != null)
        {
            string zeInventoryStr = "InventoryButtonPos";
            m_InventoryButton.GetComponent<RectTransform>().localPosition = new Vector3(MySQLiteHandler.instance.getFloat(m_SQLiteTable, zeInventoryStr + "X", null)
                , MySQLiteHandler.instance.getFloat(m_SQLiteTable, zeInventoryStr + "Y", null), 0);
        }
        if (m_PauseButton != null)
        {
            string zePauseStr = "PauseButtonPos";
            m_PauseButton.GetComponent<RectTransform>().localPosition = new Vector3(MySQLiteHandler.instance.getFloat(m_SQLiteTable, zePauseStr + "X", null)
                , MySQLiteHandler.instance.getFloat(m_SQLiteTable, zePauseStr + "Y", null), 0);
        }

        Debug.Log("Successful reading of android input position");
	}
#endif
}
