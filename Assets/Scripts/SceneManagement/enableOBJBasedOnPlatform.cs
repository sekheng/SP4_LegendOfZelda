using UnityEngine;
using System.Collections;

public class enableOBJBasedOnPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if UNITY_STANDALONE
        gameObject.SetActive(true);
#else
        gameObject.SetActive(false);
#endif
	}
}
