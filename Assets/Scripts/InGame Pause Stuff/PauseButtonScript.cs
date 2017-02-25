using UnityEngine;
using System.Collections;

/// <summary>
/// This is used for command pattern for InGamePause
/// </summary>
public class PauseButtonScript : MonoBehaviour {
    [Tooltip("You will need to indicate which number this pause button is at! 0 starts from the top")]
    public short m_Number = 0;

	// Use this for initialization
	void Start () {
	
	}
	

    public virtual bool executeButton()
    {
        Debug.Log("NO ONE SHOULD BE USING THIS SCRIPT");
        return false;
    }
}
