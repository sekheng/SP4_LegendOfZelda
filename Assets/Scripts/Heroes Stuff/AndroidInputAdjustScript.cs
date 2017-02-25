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

	// Use this for initialization
	void Start () {
        UI_position = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	    switch (hasFingerPressed)
        {
            case true:
                break;
            default:
                break;
        }
	}

    // Need to track down the finger's ID!
    public void fingerPressed()
    {
        hasFingerPressed = true;
    }

    // When letting go of the finger, then save to the database!
    public void fingerLetGo()
    {
        hasFingerPressed = false;
    }
}
