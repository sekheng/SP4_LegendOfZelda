using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private GameObject player; //drag in prefab thanks
    private bool foundPlayer = false;

	// Update is called once per frame
	void Update () {
        if (!foundPlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            foundPlayer = true;
        }
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}
}
