using UnityEngine;
using System.Collections;

public class Wolf_AttackState : State
{
    public Pathfinding astar;
    public LayerMask unwalkableMask;
    public GameObject target;
    private Wolf_Statemanager manager;
    private GameObject thePlayer;
    // Use this for initialization
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        manager = transform.parent.GetComponent<Wolf_Statemanager>();
    }

    public override void UpdateState()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            target.transform.position = thePlayer.transform.position;
            astar.FindPath(monsterTransform.position, target.transform.position);

            if (!astar.getGrid().NodeFromWorldPoint(target.transform.position).m_bWalkable || astar.getPath().Count == 0)//chek if path is walkable
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

            Vector3 dirToAttack = (astar.getPath()[0].m_v2_worldPosition - (Vector2)monsterTransform.position).normalized;

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
        }
    }
}
