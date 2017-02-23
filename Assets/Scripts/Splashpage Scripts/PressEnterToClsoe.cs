using UnityEngine;
using UnityEngine.SceneManagement;

public class PressEnterToClsoe : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return) && gameObject.activeSelf && (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1))
        {
            gameObject.SetActive(false);
        }
	}
}
