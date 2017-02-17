using UnityEngine;
using System.Collections;

public class PressEnterToClsoe : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Return) && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
	}
}
