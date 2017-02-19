﻿using UnityEngine;
using System.Collections;

public class Wolf_AttackState : State
{
    public Pathfinding astar;
    public LayerMask unwalkableMask;
    public GameObject target;
    private Wolf_Statemanager manager;
    private GameObject thePlayer;
    private Vector3 dirToAttack;
    private float dist;
    private float damageTimer;
    // public CircleCollider2D circleCollider;
    // Use this for initialization
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        manager = transform.parent.GetComponent<Wolf_Statemanager>();
        damageTimer = 1;
    }

    public override void UpdateState()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        else
        {
            damageTimer += Time.deltaTime;
            target.transform.position = thePlayer.transform.position;
            astar.FindPath(monsterTransform.position, target.transform.position);

            if (!astar.getGrid().NodeFromWorldPoint(target.transform.position).m_bWalkable)//chek if path is walkable
            {
                monsterRigidbody2D.velocity = Vector3.zero;
                manager.changeState("roam");
                return;
            }

            if (astar.getPath().Count > 1)//not close enoguh to attack
            {
                monsterRigidbody2D.velocity = Vector3.zero;
                manager.changeState("chase");
                return;
            }

            dirToAttack = (astar.getPath()[0].m_v2_worldPosition - (Vector2)monsterTransform.position).normalized;
            dist = (astar.getPath()[0].m_v2_worldPosition - (Vector2)monsterTransform.position).magnitude;

            if (dirToAttack.x > 0 && dirToAttack.x > Mathf.Abs(dirToAttack.y))
            {
                manager.changeAnim(7);
            }
            else if (dirToAttack.x < 0 && dirToAttack.x < -Mathf.Abs(dirToAttack.y))
            {
                manager.changeAnim(6);
            }
            else if (dirToAttack.y > 0 && dirToAttack.y > Mathf.Abs(dirToAttack.x))
            {
                manager.changeAnim(4);
            }
            else if (dirToAttack.y < 0 && dirToAttack.y < -Mathf.Abs(dirToAttack.x))
            {
                manager.changeAnim(5);
            }
            if(damageTimer > 1)
            {
                if(checkForCollision())
                {
                    damageTimer = 0;
                }
            }

            //When Health < 0, change to dead state
            if (health.m_health <= 0)
            {
                manager.changeState("dead");//change state
            }
        }
    }

    public override bool checkForCollision()
    {
        collision = Physics2D.BoxCastAll(monsterTransform.position, circleCollider.bounds.size, 0, dirToAttack, dist);

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
