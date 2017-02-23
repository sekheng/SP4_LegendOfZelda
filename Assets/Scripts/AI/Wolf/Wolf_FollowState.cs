using UnityEngine;
using System.Collections;

public class Wolf_FollowState : State {

    public Pathfinding astar;
    public LayerMask unwalkableMask;
    public GameObject target;
    private Wolf_Statemanager manager;
    //private GameObject thePlayer;

    // Use this for initialization
    void Start()
    {
        //thePlayer = GameObject.FindGameObjectWithTag("Player");
        manager = transform.parent.GetComponent<Wolf_Statemanager>();
        if (thePlayer == null)
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
        else
        {
            //Debug.Log("wew");
            target.transform.position = thePlayer.transform.position;
            astar.FindPath(monsterTransform.position, target.transform.position);
            if (astar.getGrid().NodeFromWorldPoint(target.transform.position).m_bWalkable && astar.getPath().Count != 0)
            {
                //Debug.Log("wew");
                Vector3 dir = astar.getPath()[0].m_v2_worldPosition - (Vector2)monsterTransform.position;
                dir.Normalize();
                monsterRigidbody2D.velocity = dir * (monsterInfo.speed/2 ) * Time.deltaTime;
                if (monsterRigidbody2D.velocity.x > 0 && monsterRigidbody2D.velocity.x > Mathf.Abs(monsterRigidbody2D.velocity.y))
                {
                    manager.anim.Play("wolf_walk_right_slow");
                }
                else if (monsterRigidbody2D.velocity.x < 0 && monsterRigidbody2D.velocity.x < -Mathf.Abs(monsterRigidbody2D.velocity.y))
                {
                    manager.anim.Play("wolf_walk_left_slow");
                }
                else if (monsterRigidbody2D.velocity.y > 0 && monsterRigidbody2D.velocity.y > Mathf.Abs(monsterRigidbody2D.velocity.x))
                {
                    manager.anim.Play("wolf_walk_up_slow");
                }
                else if (monsterRigidbody2D.velocity.y < 0 && monsterRigidbody2D.velocity.y < -Mathf.Abs(monsterRigidbody2D.velocity.x))
                {
                    manager.anim.Play("wolf_walk_down_slow");
                }

                //if()
            }
            else if (checkForPlayerInRange(thePlayer.transform.position, 5.0f))
            {
                Vector3 dir = target.transform.position - monsterTransform.position;
                dir.Normalize();
                monsterRigidbody2D.velocity = dir * (monsterInfo.speed / 2) * Time.deltaTime;
                if (monsterRigidbody2D.velocity.x > 0 && monsterRigidbody2D.velocity.x > Mathf.Abs(monsterRigidbody2D.velocity.y))
                {
                    manager.anim.Play("wolf_walk_right_slow");
                }
                else if (monsterRigidbody2D.velocity.x < 0 && monsterRigidbody2D.velocity.x < -Mathf.Abs(monsterRigidbody2D.velocity.y))
                {
                    manager.anim.Play("wolf_walk_left_slow");
                }
                else if (monsterRigidbody2D.velocity.y > 0 && monsterRigidbody2D.velocity.y > Mathf.Abs(monsterRigidbody2D.velocity.x))
                {
                    manager.anim.Play("wolf_walk_up_slow");
                }
                else if (monsterRigidbody2D.velocity.y < 0 && monsterRigidbody2D.velocity.y < -Mathf.Abs(monsterRigidbody2D.velocity.x))
                {
                    manager.anim.Play("wolf_walk_down_slow");
                }
            }
            else//invalid route to take
            {
                //change scene
                manager.changeState("roam");

            }
            //if (astar.getPath().Count > 10)//out of range, change to roam again
            //{
            //    monsterRigidbody2D.velocity = Vector3.zero;
            //    manager.changeState("roam");
            //}
            if (!checkForPlayerInRange(thePlayer.transform.position, 5))
            {
                monsterRigidbody2D.velocity = Vector3.zero;
                manager.changeState("roam");
            }
            if (checkForPlayerInRange(thePlayer.transform.position, 1.0f))//close enoguh to attack
            {

                monsterRigidbody2D.velocity = Vector3.zero;
                manager.changeState("growl");
            }
        }
        if (health.m_health <= 0)
        {
            manager.changeState("dead");//change state
        }
        //Debug.Log("hi");
    }
}
