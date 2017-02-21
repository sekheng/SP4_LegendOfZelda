using UnityEngine;
using System.Collections;

public class GolemBoss_IdleState : State {

    private GolemBoss_Statemanager manager;
    private IntRange rngTime;
    private float timeToAttack;
    private float accumTimeToAttack;

    // Use this for initialization
    void Start()
    {
        manager = transform.parent.GetComponent<GolemBoss_Statemanager>();
        timeToAttack = 0;
        accumTimeToAttack = 0;
    }

    // Update is called once per frame
    public override void UpdateState()
    {
        if(rngTime == null)
        {
            rngTime = new IntRange(3, 6);
            timeToAttack = rngTime.Random;
        }
        //Debug.Log("hi");
        timeToCheckCollision += Time.deltaTime;
        accumTimeToAttack += Time.deltaTime;
        if (timeToCheckCollision > 1)//check and do damage every one second
        {
            if (checkForCollision())
            {
                timeToCheckCollision = 0;
            }
        }
        if(accumTimeToAttack > timeToAttack)
        {
            timeToAttack = rngTime.Random;
            accumTimeToAttack = 0;
            manager.changeState("attack");//change state
            manager.anim.SetBool("idleToAttack", true);
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
