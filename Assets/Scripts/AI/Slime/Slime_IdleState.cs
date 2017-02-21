using UnityEngine;
using System.Collections;

public class Slime_IdleState : State {
    private float timeAwayToRoam;
    private float timeToRoam;
    private float damageTimer;
    //private float timeToCheckCollision;
    private Slime_Statemanager manager;
    private IntRange rng = new IntRange(1, 5);
    void Start()
    {
        timeAwayToRoam = 0;
        timeToRoam = rng.Random;
        manager = transform.parent.GetComponent<Slime_Statemanager>();
        //Debug.Log(timeToRoam.ToString());
    }
   
    public override void UpdateState()
    {
        timeAwayToRoam += Time.deltaTime;
        damageTimer += Time.deltaTime;
        timeToCheckCollision += Time.deltaTime;
        //TODO change state to roam and maybe have random time
        if (timeToCheckCollision > 1)//check and do damage every one second
        {
            if (checkForCollision())
            {
                timeToCheckCollision = 0;
            }
        }
        if (timeAwayToRoam > timeToRoam)
        {
            timeAwayToRoam = 0;//change rand time
            timeToRoam = rng.Random;
            manager.changeState("roam");//change state
        }

        //When Health < 0, change to dead state
        if (health.m_health <= 0)
        {
            timeAwayToRoam = 0;//change rand time
            timeToRoam = rng.Random;
            manager.changeState("dead");//change state
        }
    }
}
