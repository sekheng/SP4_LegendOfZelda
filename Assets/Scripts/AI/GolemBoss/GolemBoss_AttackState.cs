using UnityEngine;
using System.Collections;

public class GolemBoss_AttackState : State {

    private GolemBoss_Statemanager manager;
    private float accumTimeToChangeState;
    private bool startAccum;

    // Use this for initialization
    void Start()
    {
        manager = transform.parent.GetComponent<GolemBoss_Statemanager>();
        accumTimeToChangeState = 0;
        startAccum = false;
    }

    public override void UpdateState()
    {
        if (manager.anim.GetBool("idleToAttack") && manager.anim.GetCurrentAnimatorStateInfo(0).IsName("golemboss_attack"))
        {
            manager.anim.SetBool("idleToAttack", false);
            
        }
        if (manager.anim.GetCurrentAnimatorStateInfo(0).IsName("golemboss_idle"))
        {
            //manager.changeState("idle");//change state
            //manager.anim.SetBool("attackToIdle", true);
            startAccum = true;
        }

        accumTimeToChangeState += Time.deltaTime;
        if (accumTimeToChangeState > 1 && manager.anim.GetCurrentAnimatorStateInfo(0).IsName("golemboss_idle"))
        {
            manager.changeState("idle");
            accumTimeToChangeState = 0;
            startAccum = false;
            Debug.Log("hi");
        }
            //Debug.Log(manager.anim.GetCurrentAnimatorClipInfo(0).Length.ToString());

        
       // Debug.Log(manager.anim.GetCurrentAnimatorClipInfo(0).Length.ToString());
        timeToCheckCollision += Time.deltaTime;
        if (timeToCheckCollision > 1)//check and do damage every one second
        {
            if (checkForCollision())
            {
                timeToCheckCollision = 0;
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
