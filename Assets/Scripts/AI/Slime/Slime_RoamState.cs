using UnityEngine;
using System.Collections;

public class Slime_RoamState : State {
    //public Transform slimeTransform;
    private Vector3[] directions;
    private IntRange rngTime;
    private IntRange rngDir;
    private Slime_Statemanager manager;
    private int whichDir;
    private float roamingTime;
    private float timeToStopRoaming;
    private float damageTimer;
    private float timeToCheckCollision;
    

    void Start()
    {
        directions = new Vector3[4];
        directions[0] = Vector3.up;
        directions[1] = Vector3.down;
        directions[2] = Vector3.left;
        directions[3] = Vector3.right;
        rngTime = new IntRange(1, 3);
        rngDir = new IntRange(0, 4);
        manager = transform.parent.GetComponent<Slime_Statemanager>();
        roamingTime = 0;
        timeToStopRoaming = rngTime.Random;
        whichDir = rngDir.Random;
        damageTimer = 0;
    }

    public override void UpdateState()
    {
        roamingTime += Time.deltaTime;
        damageTimer += Time.deltaTime;
        timeToCheckCollision += Time.deltaTime;
        Vector3 tempToCheckCollision = transform.TransformDirection(directions[whichDir]);
        tempToCheckCollision *= monsterInfo.speed * Time.deltaTime;
        monsterRigidbody2D.velocity = tempToCheckCollision;
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
        if (roamingTime > timeToStopRoaming)
        {
            whichDir = rngDir.Random;
            roamingTime = 0;//reset the time
            timeToStopRoaming = rngTime.Random;
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeState("idle");//change state
        }

        //When Health < 0, change to dead state
        if(health.m_health <= 0 )
        {
            whichDir = rngDir.Random;
            roamingTime = 0;//change rand time
            timeToStopRoaming = rngTime.Random;
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeState("dead");//change state
        }
    }

    //public override bool checkForCollision()
    //{
    //    return base.checkForCollision();
         
    //}
}
