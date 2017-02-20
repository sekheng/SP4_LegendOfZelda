using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class enableOBJ : MonoBehaviour {

    public GameObject[] Buttons;

	// Update is called once per frame
	void Update () {
	    foreach (GameObject i in Buttons)
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
                i.SetActive(true);
            else
                i.SetActive(false);
        }
	}
}
