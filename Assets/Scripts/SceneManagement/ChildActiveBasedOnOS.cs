using UnityEngine;
using System.Collections;

public class ChildActiveBasedOnOS : MonoBehaviour {

    public GameObject[] children;
    private bool inverse = false;

	// Use this for initialization
	void Start () {
        
#if UNITY_STANDALONE
        inverse = false;
#else
        inverse = true;
        
#endif
        foreach (GameObject i in children)
        {
            i.SetActive(inverse ? true : false);
        }
	}
}
