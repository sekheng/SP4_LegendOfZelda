using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This is only used for when adjusting the position of the android inputs
/// </summary>
public class AndroidInputAdjustScript : MonoBehaviour {
    [Tooltip("The string to be set in the database")]
    public string m_AndroidInputName = "JoystickPos";
    //To know it's own transform
    private RectTransform UI_position;
    // To know whether has finger pressed it!
    private bool hasFingerPressed = false;
    // We will need to know what is the canvas like!
    private CanvasScaler theScaleOfCanvas;

	// Use this for initialization
	void Start () {
        UI_position = GetComponent<RectTransform>();
        theScaleOfCanvas = GameObject.FindGameObjectWithTag("CustomizeAndroidInput").GetComponent<CanvasScaler>();
	}
	
	// Update is called once per frame
	void Update () {
	    switch (hasFingerPressed)
        {
            case true:
                detectMousePosition();
                break;
            default:
                break;
        }
	}

    // Need to track down the finger's ID!
    public void fingerPressed()
    {
        hasFingerPressed = true;
        // Here we shall identify which finger pressed is it!

    }

    // When letting go of the finger, then save to the database!
    public void fingerLetGo()
    {
        hasFingerPressed = false;
        // And then we shall save the position of this input to the database!
        //MySQLiteHandler.instance.saveSpecificResult(AndroidLoadDataScript.m_SQLiteTable, m_AndroidInputName + "X", UI_position.localPosition.x.ToString());
        MySQLiteHandler.instance.saveSpecificResult(AndroidLoadDataScript.m_SQLiteTable, m_AndroidInputName + "X", UI_position.anchoredPosition.x.ToString());
        //MySQLiteHandler.instance.saveSpecificResult(AndroidLoadDataScript.m_SQLiteTable, m_AndroidInputName + "Y", UI_position.localPosition.y.ToString());
        MySQLiteHandler.instance.saveSpecificResult(AndroidLoadDataScript.m_SQLiteTable, m_AndroidInputName + "Y", UI_position.anchoredPosition.y.ToString());
    }

    /// <summary>
    /// This is only be used for debugging with mouse only!
    /// </summary>
    void detectMousePosition()
    {
        //UI_position.localPosition = Input.mousePosition;
        //UI_position.anchoredPosition = Input.mousePosition;
        //Debug.Log("Mouse position: " + Input.mousePosition);
        UI_position.anchoredPosition = new Vector2(Input.mousePosition.x * theScaleOfCanvas.referenceResolution.x / Screen.width, Input.mousePosition.y * theScaleOfCanvas.referenceResolution.y / Screen.height);
    }

    void detectFingerPosition()
    {

    }
}
