using UnityEngine;
using System.Collections;

public class DestroyIfNoChild : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	if(transform.GetComponentInChildren<GolemProjectile>() == null)
        {
            Destroy(gameObject);
        }
	}
}
