using UnityEngine;
using System.Collections;

public class Wolf_GrowlState : State {


    private RaycastHit2D[] hit;
    private Vector3 direction;
    public LayerMask unwalkableMask;
    private float timePassed;
    private Wolf_Statemanager manager;
    // Use this for initialization
    void Start () {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        timePassed = 0;
        manager = transform.parent.GetComponent<Wolf_Statemanager>();
    }

    // Update is called once per frame

    public override void UpdateState()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        timePassed += Time.deltaTime;
        direction = (thePlayer.transform.position - monsterTransform.position).normalized;
        if (timePassed > 0.1f)
        {
            if (direction.x > 0 && direction.x > Mathf.Abs(direction.y))
            {
                //if (monsterInfo.speed == monsterInfo.maxSpeed)
                whichDir = 3;

            }
            else if (direction.x < 0 && direction.x < -Mathf.Abs(direction.y))
            {
                whichDir = 2;

            }
            else if (direction.y > 0 && direction.y > Mathf.Abs(direction.x))
            {
                whichDir = 0;

            }
            else if (direction.y < 0 && direction.y < -Mathf.Abs(direction.x))
            {
                whichDir = 1;
            }

          
            manager.changeAnim(whichDir + 4);
            
        }
        if (timePassed > 1)
        {
            bool havePlayer = false;
            bool haveObstacles = false;
            hit = Physics2D.RaycastAll(monsterTransform.position, direction, 5, unwalkableMask);
            //RaycastHit2D hit2 = Physics2D.Raycast(monsterTransform.position, direction, 5, unwalkableMask);

            //Debug.Log(monsterTransform.name);
            foreach (RaycastHit2D temp in hit)
            {
                if (temp.collider != null && temp.collider.gameObject.name != monsterTransform.name)
                {
                    if (temp.collider.gameObject.tag == "Player")
                    {
                        if(!haveObstacles)
                            havePlayer = true;
                        break;
                    }
                    else if (temp.collider.gameObject.tag != "Player")
                    {
                        haveObstacles = true;
                    }
                    Debug.Log(temp.collider.gameObject.tag);
                }
                
            }
            if(havePlayer)
            {
                timePassed = 0;
                manager.changeState("chase");//change state
            }
            else
            {
                timePassed = 0;
                manager.changeState("roam");//change state
            }
            //hit = null;
           
        }
        if (health.m_health != manager.healthBeforeDamaged)
        {
            manager.healthBeforeDamaged = health.m_health;
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeState("chase");//change state
        }
        if (health.m_health / health.max_health <= 0.2f)
        {
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeState("escape");//change state
        }
        else if (health.m_health <= 0)
        {
            manager.changeState("dead");//change state
        }
    }
}
