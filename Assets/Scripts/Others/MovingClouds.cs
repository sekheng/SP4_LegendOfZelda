using UnityEngine;
using System.Collections;

public class MovingClouds : MonoBehaviour {


    public float distToTravel;
    public bool goRightFirst = true;

    private int direction = 1;
    private float distTravelled;
    private float startingPoint;
    // Use this for initialization
    void Start () {
	    if(goRightFirst)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        distTravelled = 0;
        startingPoint = transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
	    if(distTravelled * direction < distToTravel)
        {
            distTravelled += Time.deltaTime * direction;
            transform.position = new Vector3(startingPoint + distTravelled, transform.position.y, transform.position.z);
        }
        else
        {
            direction = -direction;
            transform.localScale =  new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); 
        }
	}
}
