using UnityEngine;
using System.Collections;

public class GolemBoss_IdleState : State {

    private GolemBoss_Statemanager manager;
    private IntRange rngTime;
    private float timeToAttack;
    private float accumTimeToAttack;
    private int randomNumber;

    private CSVReader reader;

    // Use this for initialization
    void Start()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        manager = transform.parent.GetComponent<GolemBoss_Statemanager>();
        timeToAttack = 0;
        randomNumber = 0;
        accumTimeToAttack = 0;
    }

    // Update is called once per frame
    public override void UpdateState()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        if (reader == null)
        {
            reader = GameObject.Find("CSVReader").GetComponent<CSVReader>();
        }
            if (rngTime == null)
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
            manager.Arrow.SetActive(false);
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

            if (health.m_health / health.max_health < 0.25f)
            {
                randomNumber = Random.Range(0, 10);
                if (randomNumber < 5)
                {
                    manager.attackMode = "cross";
                }
                else
                {
                    manager.attackMode = "random";
                }
            }
            else if (health.m_health / health.max_health < 0.5f)
            {
                randomNumber = Random.Range(0, 10);
                if (randomNumber < 1)
                {
                    manager.attackMode = "normal";
                }
                else
                {
                    manager.attackMode = "random";
                }
            }
            else if (health.m_health / health.max_health < 0.75f)
            {
                randomNumber = Random.Range(0, 10);
                if(randomNumber < 2)
                {
                    manager.attackMode = "normal";
                }
                else
                {
                    manager.attackMode = "cross";
                }
            }
            else if(health.m_health / health.max_health < 0.5f)
            {
                randomNumber = Random.Range(0, 10);
                if (randomNumber < 1)
                {
                    manager.attackMode = "normal";
                }
                else
                {
                    manager.attackMode = "random";
                }
            }
            
            accumTimeToAttack = 0;
            manager.changeState("attack");//change state
            manager.anim.SetBool("idleToAttack", true);
            if (manager.soundEffects != null)
            {
                manager.soundEffects.playSound("monster_active");
            }
            if (manager.attackMode == "normal")
            {
                manager.currProjectile = Instantiate(manager.projectile, monsterTransform.position, Quaternion.identity) as GameObject;
            }
            else if(manager.attackMode == "cross")
            {
                for (int i = 0; i < 8; ++i)
                {
                    manager.currProjectiles[i] = Instantiate(manager.projectiles[i], new Vector3(0, 0, monsterTransform.position.z), new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject;
                    //manager.currProjectiles[i].transform.GetChild(0).position = new Vector3(monsterTransform.position.x, monsterTransform.position.y + 1, monsterTransform.position.z);
                    //manager.currProjectiles[i].transform.rotation = Quaternion.Euler(0, 0, 45 * i);
                    if (i == 0)
                        manager.currProjectiles[i].transform.GetChild(0).position = new Vector3(monsterTransform.position.x, monsterTransform.position.y + 1, monsterTransform.position.z);
                    if (i == 1)
                        manager.currProjectiles[i].transform.GetChild(0).position = new Vector3(monsterTransform.position.x, monsterTransform.position.y - 1, monsterTransform.position.z);
                    if (i == 2)                    
                        manager.currProjectiles[i].transform.GetChild(0).position = new Vector3(monsterTransform.position.x + 1, monsterTransform.position.y, monsterTransform.position.z);
                    if (i == 3)                    
                        manager.currProjectiles[i].transform.GetChild(0).position = new Vector3(monsterTransform.position.x - 1, monsterTransform.position.y, monsterTransform.position.z);
                    if (i == 4)                    
                        manager.currProjectiles[i].transform.GetChild(0).position = new Vector3(monsterTransform.position.x - 1, monsterTransform.position.y + 1, monsterTransform.position.z);
                    if (i == 5)                    
                        manager.currProjectiles[i].transform.GetChild(0).position = new Vector3(monsterTransform.position.x - 1, monsterTransform.position.y - 1, monsterTransform.position.z);
                    if (i == 6)                   
                        manager.currProjectiles[i].transform.GetChild(0).position = new Vector3(monsterTransform.position.x + 1, monsterTransform.position.y + 1, monsterTransform.position.z);
                    if (i == 7)                    
                        manager.currProjectiles[i].transform.GetChild(0).position = new Vector3(monsterTransform.position.x + 1, monsterTransform.position.y - 1, monsterTransform.position.z);
                }
            }
            else if(manager.attackMode == "random")
            {
                if (reader != null)
                {
                    int width, height;
                    for (int i = 0; i < manager.projectiles.Length; ++i)
                    {
                        if (manager.projectiles[i] != null)
                        {
                            //Debug.Log(i.ToString());
                            if(thePlayer.transform.position.x > monsterTransform.position.x)
                            {
                                width = Random.Range((-reader.map_width / 4 + 1), (reader.map_width / 2 - 1));
                            }
                            else
                            {
                                width = Random.Range((-reader.map_width / 2 + 1), (reader.map_width / 4 - 1));
                            }
                            if (thePlayer.transform.position.y > monsterTransform.position.y)
                            {
                                //width = Random.Range((-reader.map_width / 4 + 1), (reader.map_width / 2 - 1));
                                height = Random.Range((-reader.map_height / 2 + 1), (reader.map_height / 2 - 1));
                            }
                            else
                            {
                                height = Random.Range((-reader.map_height / 2 + 1), (reader.map_height / 4 - 1));
                                //width = Random.Range((-reader.map_width / 2 + 1), (reader.map_width / 4 - 1));
                            }
                            
                            manager.currProjectiles[i] = Instantiate(manager.projectiles[i], new Vector3(width, height, monsterTransform.position.z), new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject;
                        }
                    }
                }
                
            }
            //if(health.m_health / health.max_health < 0.25f)
            //{
            //    for(int i = 0; i < manager.currProjectiles.Length; ++i)
            //    {
            //        if(i == 0)
            //            manager.currProjectiles[i] = Instantiate(manager.projectiles[i], new Vector3(monsterTransform.position.x, monsterTransform.position.y + 1, monsterTransform.position.z), new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject;
            //        if (i == 1)
            //            manager.currProjectiles[i] = Instantiate(manager.projectiles[i], new Vector3(monsterTransform.position.x, monsterTransform.position.y - 1, monsterTransform.position.z), new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject;
            //        if (i == 2)
            //            manager.currProjectiles[i] = Instantiate(manager.projectiles[i], new Vector3(monsterTransform.position.x + 1, monsterTransform.position.y , monsterTransform.position.z), new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject;
            //        if (i == 3)
            //            manager.currProjectiles[i] = Instantiate(manager.projectiles[i], new Vector3(monsterTransform.position.x - 1, monsterTransform.position.y , monsterTransform.position.z), new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject;

            //    }
            //}
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
