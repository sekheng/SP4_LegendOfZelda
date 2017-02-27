using UnityEngine;
using System.Collections;

public class GolemBoss_AttackState : State {

    private GolemBoss_Statemanager manager;
    private float accumTimeToChangeState;
    private float alphaValue;
    private float alphaPercentage;
    private bool startAccum;
    private GolemProjectile golemProjectile;
    private GolemProjectile[] golemProjectiles;


    // Use this for initialization
    void Start()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        golemProjectiles = new GolemProjectile[4];
        for (int i = 0; i < golemProjectiles.Length; ++i)
        {
            golemProjectiles[i] = null;
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
        for (int i = 0; i < golemProjectiles.Length; ++i)
        {
            if (golemProjectiles[i] == null && manager.currProjectiles[i] != null)
            {
                golemProjectiles[i] = manager.currProjectiles[i].GetComponent<GolemProjectile>(); 
            }
        }
        if(accumTimeToChangeState < 1)
        {
            
            alphaPercentage = accumTimeToChangeState / 1;
            alphaValue = 1 * alphaPercentage;
            if (manager.currProjectile != null)
                golemProjectile.spriteR.color = new Color(golemProjectile.spriteR.color.r, golemProjectile.spriteR.color.g, golemProjectile.spriteR.color.b, alphaValue);
            for (int i = 0; i < golemProjectiles.Length; ++i)
            {
                if (manager.currProjectiles[i] != null)
                {
                    golemProjectiles[i].spriteR.color = new Color(golemProjectile.spriteR.color.r, golemProjectile.spriteR.color.g, golemProjectile.spriteR.color.b, alphaValue);
                }
            }
            manager.TargetLock.SetActive(true);
            manager.TargetLock.transform.position = thePlayer.transform.position;
        }
        else if (accumTimeToChangeState > 1 && manager.anim.GetCurrentAnimatorStateInfo(0).IsName("golemboss_idle"))
        {
            if (manager.soundEffects != null)
            {
                manager.soundEffects.playSound("target");
            }
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
            for (int i = 0; i < golemProjectiles.Length; ++i)
            {
                if (manager.currProjectiles[i] != null)
                {
                    if (health.m_health / health.max_health > 0.20f)
                    {
                        golemProjectiles[i].speed = 1000;
                        golemProjectiles[i].damage = monsterInfo.dps;
                    }
                    else
                    {
                        golemProjectiles[i].speed = 500;
                        golemProjectiles[i].damage = monsterInfo.dps / 2;
                    }
                    golemProjectiles[i].direction = (thePlayer.transform.position - monsterTransform.transform.position).normalized;
                    golemProjectiles[i] = null;
                    //golemProjectiles[i] = manager.currProjectiles[i].GetComponent<GolemProjectile>();
                }
            }
            Debug.Log("hi");
            manager.TargetLock.SetActive(true);
            manager.TargetLock.transform.position = thePlayer.transform.position;
        }
        else
        {
            manager.TargetLock.SetActive(true);
            manager.TargetLock.transform.position = thePlayer.transform.position;
        }
            //Debug.Log(manager.anim.GetCurrentAnimatorClipInfo(0).Length.ToString());

        
       // Debug.Log(manager.anim.GetCurrentAnimatorClipInfo(0).Length.ToString());
        //timeToCheckCollision += Time.deltaTime;
        //if (timeToCheckCollision > 1)//check and do damage every one second
        //{
        //    if (checkForCollision())
        //    {
        //        timeToCheckCollision = 0;
        //    }
        //}
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
