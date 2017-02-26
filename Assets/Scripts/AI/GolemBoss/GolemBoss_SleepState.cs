using UnityEngine;
using System.Collections;

public class GolemBoss_SleepState : State
{

    private GolemBoss_Statemanager manager;

    // Use this for initialization
    void Start()
    {
        manager = transform.parent.GetComponent<GolemBoss_Statemanager>();
        if(thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public override void UpdateState()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        timeToCheckCollision += Time.deltaTime;
        if (timeToCheckCollision > 1)//check and do damage every one second
        {
            if (checkForCollision())
            {
                timeToCheckCollision = 0;
            }
        }
        if(checkForPlayerInRange(thePlayer.transform.position, 3) || health.m_health != health.max_health)
        {
            manager.changeState("awake");//change state
            //manager.changeAnim("golemboss_awake");
            manager.anim.SetBool("sleepToAwake", true);
            if (manager.soundEffects != null)
            {
                manager.soundEffects.playSound("earthquake");
            }
            if (manager.bgm != null)
            {
                manager.bgm.changeMusic("Will_You_Still_Cry");
            }
        }
        if (health.m_health <= 0)
        {
            manager.changeState("dead");//change state
        }
    }

    public override bool checkForCollision()
    {
        collision = Physics2D.BoxCastAll(monsterTransform.position, manager.getBox().bounds.size, 0, Vector2.zero);

        foreach (RaycastHit2D temp in collision)
        {
            if (temp.collider != null)
            {
                if (temp.collider.gameObject.tag == "Player" && temp.collider.gameObject.GetComponent<HealthScript>() != null)
                {
                    temp.collider.gameObject.GetComponent<HealthScript>().modifyHealth(-monsterInfo.dps);
                    return true;
                }
            }
        }
        return false;
    }

    
}
