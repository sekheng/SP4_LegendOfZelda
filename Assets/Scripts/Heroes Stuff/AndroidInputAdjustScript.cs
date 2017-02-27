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
    // We will need to know what is the finger's ID
    private int theFingerTouchedID = 0;

	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
        UI_position = GetComponent<RectTransform>();
        theScaleOfCanvas = GameObject.FindGameObjectWithTag("CustomizeAndroidInput").GetComponent<CanvasScaler>();
        //Debug.Log("Canvas Res: " + theScaleOfCanvas.referenceResolution);
#else
        enabled = false;
#endif
	}
	
	// Update is called once per frame
	void Update () {
	    switch (hasFingerPressed)
        {
            case true:
                //detectMousePosition();
                detectFingerPosition();
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
        Touch[] allZeTouch = Input.touches;
        foreach (Touch zeTouch in allZeTouch)
        {
            if (zeTouch.position.x < (UI_position.position.x + Mathf.Abs(UI_position.sizeDelta.x * 0.5f)) && zeTouch.position.x > (UI_position.position.x - Mathf.Abs(UI_position.sizeDelta.x * 0.5f))
                && zeTouch.position.y < (UI_position.position.y + Mathf.Abs(UI_position.sizeDelta.y * 0.5f)) && zeTouch.position.y > (UI_position.position.y - Mathf.Abs(UI_position.sizeDelta.y * 0.5f)))
            {
                theFingerTouchedID = zeTouch.fingerId;
                break;
            }
        }
    }

    // When letting go of the finger, then save to the database!
    public void fingerLetGo()
    {
        hasFingerPressed = false;
        // And then we shall save the position of this input to the database!
        MySQLiteHandler.instance.saveSpecificResult(AndroidLoadDataScript.m_SQLiteTable, m_AndroidInputName + "X", UI_position.anchoredPosition.x.ToString());
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
        //UI_position.anchoredPosition = new Vector2(Input.mousePosition.x * theScaleOfCanvas.referenceResolution.x / Screen.width, Input.mousePosition.y * theScaleOfCanvas.referenceResolution.y / Screen.height);
        UI_position.anchoredPosition = new Vector2((Input.mousePosition.x * theScaleOfCanvas.referenceResolution.x / Screen.width) - (theScaleOfCanvas.referenceResolution.x * UI_position.anchorMax.x), (Input.mousePosition.y * theScaleOfCanvas.referenceResolution.y / Screen.height) - (theScaleOfCanvas.referenceResolution.y * UI_position.anchorMax.y));
    }

    void detectFingerPosition()
    {
        Touch theFingerTouched = Input.GetTouch(theFingerTouchedID);
        UI_position.anchoredPosition = new Vector2((theFingerTouched.position.x * theScaleOfCanvas.referenceResolution.x / Screen.width) - (theScaleOfCanvas.referenceResolution.x * UI_position.anchorMax.x), (theFingerTouched.position.y * theScaleOfCanvas.referenceResolution.y / Screen.height) - (theScaleOfCanvas.referenceResolution.y * UI_position.anchorMax.y));
    }
}
