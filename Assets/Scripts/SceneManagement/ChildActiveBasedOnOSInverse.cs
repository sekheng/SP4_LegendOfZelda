using UnityEngine;
using System.Collections;

public class ChildActiveBasedOnOSInverse : MonoBehaviour {

    public GameObject[] children;
    private bool inverse = false;

	// Use this for initialization
	void Start () {
        
#if UNITY_STANDALONE
        inverse = true;
#else
        inverse = false;
        
#endif
        foreach (GameObject i in children)
        {
            i.SetActive(inverse ? true : false);
        }
	}
}
