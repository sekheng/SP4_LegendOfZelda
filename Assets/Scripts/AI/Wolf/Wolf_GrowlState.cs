using UnityEngine;
using System.Collections;

public class Wolf_GrowlState : State {


    private RaycastHit2D[] hit;
    private Vector3 direction;
    private float timePassed;
	// Use this for initialization
	void Start () {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        timePassed = 0;
    }

    // Update is called once per frame

    public override void UpdateState()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        direction = (thePlayer.transform.position - monsterTransform.position).normalized;
        timePassed += Time.deltaTime;
        if (timePassed > 3)
        {
            bool havePlayer = false;
            bool haveWall = false;
            hit = Physics2D.RaycastAll(monsterTransform.position, direction, 5);
            foreach(RaycastHit2D temp in hit)
            {
                if (temp.collider != null)
                {
                    if (temp.collider.gameObject.tag == "Player" && temp.collider.gameObject.GetComponent<HealthScript>() != null)
                    {
                        temp.collider.gameObject.GetComponent<HealthScript>().modifyHealth(-monsterInfo.dps);
                        //return true;
                    }
                }
            }
        }
    }
}
