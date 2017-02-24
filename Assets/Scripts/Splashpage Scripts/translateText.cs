using UnityEngine;
using System.Collections;

public class translateText : MonoBehaviour {

    public float speed = 1.0f;
    private float doubleTime;

    void Start()
    {
        doubleTime = speed * 2;
    }
	// Update is called once per frame
	void FixedUpdate () {
        float speedToUse = Input.GetKey(KeyCode.Space) ? doubleTime : speed;
        transform.localPosition += new Vector3(0, speedToUse * Time.deltaTime, speedToUse * Time.deltaTime);
	}
}
