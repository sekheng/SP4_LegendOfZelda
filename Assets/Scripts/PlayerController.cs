using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    private int sumOfHorizontal = 0;
    private int sumOfVertical = 0;

    private int up, down, left, right;

	// Use this for initialization
	void Start () {
	    up = down = left = right = 0;
	}
	
	// Update is called once per frame
	void Update () {

        up = Input.GetKey("w") ? 1 : 0;
        down = Input.GetKey("s") ? -1 : 0;
        left = Input.GetKey("a") ? -1 : 0;
        right = Input.GetKey("d") ? 1 : 0;

        sumOfHorizontal = left + right;
        sumOfVertical = up + down;

        transform.position += new Vector3(sumOfHorizontal * 5, sumOfVertical * 5, 0) * Time.deltaTime;
	}
}
