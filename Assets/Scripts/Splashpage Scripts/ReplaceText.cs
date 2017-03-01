using UnityEngine;
using System.Collections;

public class ReplaceText : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if UNITY_STANDALONE
	    GetComponent<TextMesh>().text = "Press " + KeyBindScript.attackKey + " to skip.";
#else
        GetComponent<TextMesh>().text = "Press Melee to skip.";
#endif
    }
}
