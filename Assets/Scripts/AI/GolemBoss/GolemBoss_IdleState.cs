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
            rngTime = new IntRange(2, 4);
            timeToAttack = rngTime.Random;
        }
        //Debug.Log("hi");
        //timeToCheckCollision += Time.deltaTime;
        accumTimeToAttack += Time.deltaTime;
        if(manager.currProjectile == null)
        {
            manager.TargetLock.SetActive(false);
        }
        //if (timeToCheckCollision > 1)//check and do damage every one second
        //{
        //    if (checkForCollision())
        //    {
        //        timeToCheckCollision = 0;
        //    }
        //}
        if(accumTimeToAttack > timeToAttack)
        {
            if (health.m_health / health.max_health > 0.50f)
            {
                timeToAttack = rngTime.Random;
            }
            else if(health.m_health / health.max_health > 0.25f)
            {
                timeToAttack = 1;
            }
            else
            {
                timeToAttack = 2;
            }
            accumTimeToAttack = 0;
            manager.changeState("attack");//change state
            manager.anim.SetBool("idleToAttack", true);
            if (manager.soundEffects != null)
            {
                manager.soundEffects.playSound("monster_active");
            }

            manager.currProjectile = Instantiate(manager.projectile, monsterTransform.position, Quaternion.identity) as GameObject;
            if(health.m_health / health.max_health < 0.25f)
            {
                for(int i = 0; i < manager.currProjectiles.Length; ++i)
                {
                    if(i == 0)
                        manager.currProjectiles[i] = Instantiate(manager.projectiles[i], new Vector3(monsterTransform.position.x, monsterTransform.position.y + 1, monsterTransform.position.z), new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject;
                    if (i == 1)
                        manager.currProjectiles[i] = Instantiate(manager.projectiles[i], new Vector3(monsterTransform.position.x, monsterTransform.position.y - 1, monsterTransform.position.z), new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject;
                    if (i == 2)
                        manager.currProjectiles[i] = Instantiate(manager.projectiles[i], new Vector3(monsterTransform.position.x + 1, monsterTransform.position.y , monsterTransform.position.z), new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject;
                    if (i == 3)
                        manager.currProjectiles[i] = Instantiate(manager.projectiles[i], new Vector3(monsterTransform.position.x - 1, monsterTransform.position.y , monsterTransform.position.z), new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject;

                }
            }
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
