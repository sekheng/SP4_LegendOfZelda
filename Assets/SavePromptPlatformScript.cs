using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SavePromptPlatformScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
       Text m_SaveAlertText = GetComponent<Text>();
       // We need to change the text
#if UNITY_ANDROID
       m_SaveAlertText.text = "Press Pause button to go to the in-game pause menu before the imminent boss fight";
#else
         m_SaveAlertText.text = "Press Escape to go to the in-game pause menu before the imminent boss fight";
#endif
    }
}
