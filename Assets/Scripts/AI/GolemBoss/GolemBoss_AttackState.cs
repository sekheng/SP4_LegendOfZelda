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

    private Vector3 targetDir;
    private float angle;

    private GameObject arrowParent;

    private CSVReader reader;


    // Use this for initialization
    void Start()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        golemProjectiles = new GolemProjectile[12];
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
        if (reader == null)
        {
            reader = GameObject.Find("CSVReader").GetComponent<CSVReader>();
            manager.Arrow.transform.localScale = new Vector3(1, reader.map_height / 2, 1);
            manager.Arrow.transform.position = (new Vector3(0, reader.map_height / 2 + monsterTransform.transform.position.y, 0));
            arrowParent = manager.Arrow.transform.parent.gameObject;
        }
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
        if(accumTimeToChangeState == 0)
        {
            if (golemProjectile == null)
            {
                if (manager.currProjectile != null)
                    golemProjectile = manager.currProjectile.GetComponentInChildren<GolemProjectile>();
            }
            for (int i = 0; i < golemProjectiles.Length; ++i)
            {
                if (golemProjectiles[i] == null && manager.currProjectiles[i] != null)
                {
                    //Debug.Log(i.ToString());
                    golemProjectiles[i] = manager.currProjectiles[i].GetComponentInChildren<GolemProjectile>();
                    //if (golemProjectiles[i] != null)
                    //{
                    //    Debug.Log("hisssssss");
                    //}
                }
            }
            if (manager.currProjectile != null || manager.attackMode == "cross")
            {
                manager.TargetLock.SetActive(true);
                manager.Arrow.SetActive(true);
            }
        }
        accumTimeToChangeState += Time.deltaTime;
        
        if(accumTimeToChangeState < 1)
        {
            
            alphaPercentage = accumTimeToChangeState / 1;
            alphaValue = 1 * alphaPercentage;
            if (manager.currProjectile != null)
            {
                golemProjectile.spriteR.color = new Color(golemProjectile.spriteR.color.r, golemProjectile.spriteR.color.g, golemProjectile.spriteR.color.b, alphaValue);
            }
            for (int i = 0; i < golemProjectiles.Length; ++i)
            {
                
                if (golemProjectiles[i] != null)
                {
                    golemProjectiles[i].spriteR.color = new Color(golemProjectiles[i].spriteR.color.r, golemProjectiles[i].spriteR.color.g, golemProjectiles[i].spriteR.color.b, alphaValue);
                }
            }
            
            if (golemProjectile != null || manager.attackMode == "cross")
            {
                manager.TargetLock.transform.position = thePlayer.transform.position;
                targetDir = thePlayer.transform.position - monsterTransform.position;
                angle = Vector3.Angle(targetDir, Vector3.up);
                //Debug.Log(angle.ToString());
                //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, angle + 90, transform.rotation.w);
                if (thePlayer.transform.position.x > monsterTransform.position.x)
                {
                    arrowParent.transform.rotation = Quaternion.Euler(0, 0, -angle);
                }
                else//vector3 angle is < 180 always
                {
                    arrowParent.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                if(manager.attackMode == "cross")
                {
                    for (int i = 0; i < golemProjectiles.Length; ++i)
                    {

                        if (golemProjectiles[i] != null)
                        {
                            if (thePlayer.transform.position.x > monsterTransform.position.x)
                            {
                                golemProjectiles[i].transform.parent.rotation = Quaternion.Euler(0, 0, -angle);
                            }
                            else//vector3 angle is < 180 always
                            {
                                golemProjectiles[i].transform.parent.rotation = Quaternion.Euler(0, 0, angle);
                            }
                            
                        }
                    }
                }
            }
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
            if(manager.attackMode == "normal")
            {
                if (manager.currProjectile != null)
                {
                    golemProjectile.spriteR.color = new Color(golemProjectile.spriteR.color.r, golemProjectile.spriteR.color.g, golemProjectile.spriteR.color.b, 1);
                    if (health.m_health / health.max_health > 0.50f)
                    {
                        golemProjectile.speed = 500;
                        golemProjectile.damage = monsterInfo.dps;
                    }
                    else
                    {
                        golemProjectile.speed = 1000;
                        golemProjectile.damage = monsterInfo.dps;
                    }
                    golemProjectile.direction = (thePlayer.transform.position - monsterTransform.transform.position).normalized;
                    golemProjectile = null;
                    manager.TargetLock.transform.position = thePlayer.transform.position;
                    targetDir =  thePlayer.transform.position - monsterTransform.position;
                    angle = Vector3.Angle(targetDir, Vector3.up);
                    if (thePlayer.transform.position.x > monsterTransform.position.x)
                    {
                        arrowParent.transform.rotation = Quaternion.Euler(0, 0, -angle);
                    }
                    else//vector3 angle is < 180 always
                    {
                        arrowParent.transform.rotation = Quaternion.Euler(0, 0, angle);
                    }
                }
            }
            else if(manager.attackMode == "cross")
            {
                targetDir = thePlayer.transform.position - monsterTransform.position;
                angle = Vector3.Angle(targetDir, Vector3.up);
                for (int i = 0; i < golemProjectiles.Length; ++i)
                {
                    if (golemProjectiles[i] != null)
                    {
                        golemProjectiles[i].spriteR.color = new Color(golemProjectiles[i].spriteR.color.r, golemProjectiles[i].spriteR.color.g, golemProjectiles[i].spriteR.color.b, 1);

                        if (health.m_health / health.max_health > 0.20f)
                        {
                            golemProjectiles[i].speed = 500;
                            golemProjectiles[i].damage = monsterInfo.dps;
                            //golemProjectiles[i].speed = 1000;
                            //golemProjectiles[i].damage = monsterInfo.dps;
                        }
                        else
                        {
                            golemProjectiles[i].speed = 1000;
                            golemProjectiles[i].damage = monsterInfo.dps;
                        }
   
                        if(i == 0)
                        {
                            golemProjectiles[i].direction = Vector3.up;
                        }
                        else if(i == 1)
                        {
                            golemProjectiles[i].direction = Vector3.down;
                        }
                        else if (i == 2)
                        {
                            golemProjectiles[i].direction = Vector3.right;
                        }
                        else if (i == 3)
                        {
                            golemProjectiles[i].direction = Vector3.left;
                        }
                        else if (i == 4)
                        {
                            golemProjectiles[i].direction = Vector3.up;
                        }
                        else if (i == 5)
                        {
                            golemProjectiles[i].direction = Vector3.down;
                        }
                        else if (i == 6)
                        {
                            golemProjectiles[i].direction = Vector3.up;
                        }
                        else if (i == 7)
                        {
                            golemProjectiles[i].direction = Vector3.down;
                        }
                        if (thePlayer.transform.position.x > monsterTransform.position.x)
                        {
                            golemProjectiles[i].direction = Quaternion.Euler(0, 0, -angle) * golemProjectiles[i].direction;
                        }
                        else//vector3 angle is < 180 always
                        {
                            golemProjectiles[i].direction = Quaternion.Euler(0, 0, angle) * golemProjectiles[i].direction;
                        }
                        
                        golemProjectiles[i] = null;
                        
                        //golemProjectiles[i] = manager.currProjectiles[i].GetComponent<GolemProjectile>();
                    }
                }
                manager.TargetLock.transform.position = thePlayer.transform.position;
                targetDir = thePlayer.transform.position - monsterTransform.position;
                angle = Vector3.Angle(targetDir, Vector3.up);
                //Debug.Log(angle.ToString());
                //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, angle + 90, transform.rotation.w);
                if (thePlayer.transform.position.x > monsterTransform.position.x)
                {
                    arrowParent.transform.rotation = Quaternion.Euler(0, 0, -angle);
                }
                else//vector3 angle is < 180 always
                {
                    arrowParent.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
            else if(manager.attackMode == "random")
            {
                for (int i = 0; i < golemProjectiles.Length; ++i)
                {
                    if (golemProjectiles[i] != null)
                    {
                        golemProjectiles[i].spriteR.color = new Color(golemProjectiles[i].spriteR.color.r, golemProjectiles[i].spriteR.color.g, golemProjectiles[i].spriteR.color.b, 1);

                        if (health.m_health / health.max_health > 0.20f)
                        {
                            golemProjectiles[i].speed = 500;
                            golemProjectiles[i].damage = monsterInfo.dps;
                            
                        }
                        else
                        {
                            golemProjectiles[i].speed = 1000;
                            golemProjectiles[i].damage = monsterInfo.dps;
                        }
                        golemProjectiles[i] = null;
                    }

                }
            }
            
            
            
        }
        else
        {
            
            if(golemProjectile != null || manager.attackMode == "cross")
            {
                
                manager.TargetLock.transform.position = thePlayer.transform.position;
                targetDir = thePlayer.transform.position - monsterTransform.position;
                angle = Vector3.Angle(targetDir, Vector3.up);
                if (thePlayer.transform.position.x > monsterTransform.position.x)
                {
                    arrowParent.transform.rotation = Quaternion.Euler(0, 0, -angle);
                }
                else//vector3 angle is < 180 always
                {
                    arrowParent.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                 if(manager.attackMode == "cross")
                {
                    for (int i = 0; i < golemProjectiles.Length; ++i)
                    {

                        if (golemProjectiles[i] != null)
                        {
                            if (thePlayer.transform.position.x > monsterTransform.position.x)
                            {
                                golemProjectiles[i].transform.parent.rotation = Quaternion.Euler(0, 0, -angle);
                            }
                            else//vector3 angle is < 180 always
                            {
                                golemProjectiles[i].transform.parent.rotation = Quaternion.Euler(0, 0, angle);
                            }
                            
                        }
                    }
                }
            }

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
