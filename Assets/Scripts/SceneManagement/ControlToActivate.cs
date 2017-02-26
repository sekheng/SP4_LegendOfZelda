using UnityEngine;
using System.Collections;

public class ControlToActivate : MonoBehaviour {

    public GameObject PCCONTROLS, MOBILECONTROLS;

	// Use this for initialization
	void Start () {
#if UNITY_STANDALONE
        PCCONTROLS.SetActive(true);
#else
        MOBILECONTROLS.SetActive(true);
#endif
	}
}
