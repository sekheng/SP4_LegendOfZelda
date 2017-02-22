using UnityEngine;
using System.Collections;

public class GolemBoss_AttackState : State {

    private GolemBoss_Statemanager manager;
    private float accumTimeToChangeState;
    private float alphaValue;
    private float alphaPercentage;
    private bool startAccum;
    private GolemProjectile golemProjectile;
    

    // Use this for initialization
    void Start()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        manager = transform.parent.GetComponent<GolemBoss_Statemanager>();
        accumTimeToChangeState = 0;
        startAccum = false;
    }

    public override void UpdateState()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
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
        if(golemProjectile == null)
        {
            if(manager.currProjectile != null)
             golemProjectile = manager.currProjectile.GetComponent<GolemProjectile>();
        }
        if(accumTimeToChangeState < 1)
        {
            
            alphaPercentage = accumTimeToChangeState / 1;
            alphaValue = 1 * alphaPercentage;
            if (manager.currProjectile != null)
                golemProjectile.spriteR.color = new Color(golemProjectile.spriteR.color.r, golemProjectile.spriteR.color.g, golemProjectile.spriteR.color.b, alphaValue);
        }
        else if (accumTimeToChangeState > 1 && manager.anim.GetCurrentAnimatorStateInfo(0).IsName("golemboss_idle"))
        {
            manager.changeState("idle");
            accumTimeToChangeState = 0;
            startAccum = false;
            if (manager.currProjectile != null)
            {
                if (health.m_health / health.max_health > 0.20f)
                {
                    golemProjectile.speed = 1000;
                    golemProjectile.damage = monsterInfo.dps;
                }
                else
                {
                    golemProjectile.speed = 500;
                    golemProjectile.damage = monsterInfo.dps/2;
                }
                golemProjectile.direction = (thePlayer.transform.position - monsterTransform.transform.position).normalized;
                golemProjectile = null;
            }
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
