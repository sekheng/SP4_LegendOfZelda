using UnityEngine;
using System.Collections;

public class Wolf_EscapeState : State {

    private Wolf_Statemanager manager;
    private Vector3 direction;
    //private int whichDir;
    // Use this for initialization
    void Start () {
        if (thePlayer == null)
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        manager = transform.parent.GetComponent<Wolf_Statemanager>();
        whichDir = 0;
    }

    // Update is called once per frame
    public override void UpdateState()
    {
        if (thePlayer == null)
            thePlayer = GameObject.FindGameObjectWithTag("Player");

        direction = (monsterTransform.position - thePlayer.transform.position).normalized;
        if(checkForPlayerInRange(thePlayer.transform.position, 5))
        {
            monsterRigidbody2D.velocity = direction * monsterInfo.speed * Time.deltaTime;
            if (direction.x > 0 && direction.x > Mathf.Abs(direction.y))
            {
                //if (monsterInfo.speed == monsterInfo.maxSpeed)
                whichDir = 3;
                
            }
            else if (direction.x < 0 && direction.x < -Mathf.Abs(direction.y))
            {
                whichDir = 2;

            }
            else if (direction.y > 0 && direction.y > Mathf.Abs(direction.x))
            {
                whichDir = 0;

            }
            else if (direction.y < 0 && direction.y < -Mathf.Abs(direction.x))
            {
                whichDir = 1;
            }

            if(monsterInfo.speed == monsterInfo.maxSpeed)
            {
                manager.changeAnim(whichDir);
            }
            else
            {
                manager.changeAnim(whichDir + 4);
            }
        }
        else
        {
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeState("roam");
        }

        if (health.m_health <= 0)
        {
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeState("dead");//change state
        }
    }

}
