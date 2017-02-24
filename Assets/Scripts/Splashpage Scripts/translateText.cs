using UnityEngine;
using System.Collections;

public class translateText : MonoBehaviour {

    public float speed = 1.0f;

	// Update is called once per frame
	void FixedUpdate () {
        transform.localPosition += new Vector3(0, speed * Time.deltaTime, speed * Time.deltaTime);
	}
}
