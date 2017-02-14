using UnityEngine;
using System.Collections;

public class RoomInit : MonoBehaviour {

    public GameObject[] thingsToInit;

	// Use this for initialization
	void Start () {
	    foreach(GameObject i in thingsToInit)
        {
            Instantiate(i, new Vector3(0, 0, 0), Quaternion.identity);
        }
	}
}
