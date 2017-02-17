using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PressEnterToClsoe : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Return) && gameObject.activeSelf && SceneManager.GetActiveScene().buildIndex == 0)
        {
            gameObject.SetActive(false);
        }
	}
}
