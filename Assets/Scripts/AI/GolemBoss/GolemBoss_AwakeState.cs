using UnityEngine;
using System.Collections;

public class GolemBoss_AwakeState : State {

    private GolemBoss_Statemanager manager;

    // Use this for initialization
    void Start()
    {
        manager = transform.parent.GetComponent<GolemBoss_Statemanager>();
    }

    public override void UpdateState()
    {
        //timeToCheckCollision += Time.deltaTime;
        //if (timeToCheckCollision > 1)//check and do damage every one second
        //{
        //    if (checkForCollision())
        //    {
        //        timeToCheckCollision = 0;
        //    }
        //}
        if(manager.anim.GetCurrentAnimatorStateInfo(0).IsName("golemboss_idle"))
        {
            manager.changeState("idle");//change state
        }
        if (health.m_health <= 0)
        {
            manager.changeState("dead");//change state
        }
    }

    //public override bool checkForCollision()
    //{
    //    collision = Physics2D.BoxCastAll(monsterTransform.position, manager.getBox().bounds.size, 0, Vector2.zero);

    //    foreach (RaycastHit2D temp in collision)
    //    {
    //        if (temp.collider != null)
    //        {
    //            if (temp.collider.gameObject.tag == "Player" && temp.collider.gameObject.GetComponent<HealthScript>() != null)
    //            {
    //                temp.collider.gameObject.GetComponent<HealthScript>().modifyHealth(-monsterInfo.dps);
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}
}
