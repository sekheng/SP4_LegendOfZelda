using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private GameObject player;

	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
	}
}
