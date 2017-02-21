using UnityEngine;
using System.Collections;

public class Wolf_RoamState : State
{

    private Vector3[] directions;
    private IntRange rngTime;
    private IntRange rngDir;
    private IntRange rngChangeState;
    private Wolf_Statemanager manager;
    private int whichDir;
    private int toChangeState;
    private float roamingTime;
    private float timeToStopRoaming;
    private float damageTimer;
    //private float timeToCheckCollision;


    void Start()
    {
        directions = new Vector3[4];
        directions[0] = Vector3.up;
        directions[1] = Vector3.down;
        directions[2] = Vector3.left;
        directions[3] = Vector3.right;
        //rngTime = new IntRange(1, 2);
        //rngDir = new IntRange(0, 4);
        //rngChangeState = new IntRange(0, 2);
        manager = transform.parent.GetComponent<Wolf_Statemanager>();
        roamingTime = 0;
        //if (rngTime != null)
        //    timeToStopRoaming = rngTime.Random;
        //if (rngDir != null)
        //    whichDir = rngDir.Random;
        //if(rngChangeState != null)
        //    toChangeState = rngChangeState.Random;
        damageTimer = 0;
        manager.changeAnim(whichDir);
    }

    public override void UpdateState()
    {
        if (rngTime == null)
        {
            rngTime = new IntRange(1, 3);
            timeToStopRoaming = rngTime.Random;
        }
        if (rngDir == null)
        {
            rngDir = new IntRange(0, 4);
            whichDir = rngDir.Random;
        }
        if (rngChangeState == null)
        {
            rngChangeState = new IntRange(0, 2);
            toChangeState = rngChangeState.Random;
        }
        roamingTime += Time.deltaTime;
        damageTimer += Time.deltaTime;
        timeToCheckCollision += Time.deltaTime;
        Vector3 tempToCheckCollision = transform.TransformDirection(directions[whichDir]);
        tempToCheckCollision *= monsterInfo.speed * Time.deltaTime;
        monsterRigidbody2D.velocity = tempToCheckCollision;
        manager.changeAnim(whichDir);
        if (timeToCheckCollision > 1)//check and do damage every one second
        {
            if (checkForCollision())
            {
                timeToCheckCollision = 0;
            }
        }
        //if (!Physics.Raycast(monsterTransform.position, tempToCheckCollision, monsterSprite.bounds.size.x))
        //{
        //    monsterRigidbody2D.velocity = tempToCheckCollision;
        //    //monsterTransform.position += tempToCheckCollision;
        //}
        if (roamingTime > timeToStopRoaming && toChangeState == 0)
        {
            whichDir = rngDir.Random;
            roamingTime = 0;//reset the time
            timeToStopRoaming = rngTime.Random;
            toChangeState = rngChangeState.Random;
            monsterRigidbody2D.velocity = Vector3.zero;
            //manager.changeAnim(whichDir);
            manager.changeState("search");//change state
        }
        else if(roamingTime > timeToStopRoaming && toChangeState == 1)
        {
            whichDir = rngDir.Random;
            roamingTime = 0;//reset the time
            timeToStopRoaming = rngTime.Random;
            toChangeState = rngChangeState.Random;
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeAnim(whichDir);
        }


        //check if the player is in the range
        if (checkForPlayerInRange())
        {
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeState("chase");//change state
        }
        //When Health < 0, change to dead state
        if (health.m_health <= 0)
        {
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeState("dead");//change state
        }
    }
}
