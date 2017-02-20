using UnityEngine;
using System.Collections;

public class Wolf_SearchState : State {

    public Pathfinding astar;
    public LayerMask unwalkableMask;
    public GameObject target;
    private Wolf_Statemanager manager;
    //GameObject[] floors;
    IntRange rngX,rngY;
    int x, y;
    int randFloorNum;
    float timeBeforeChangeState;
    bool initOnUpdate;
    bool hasReached;
    //float timeToCheckCollision;



    // Use this for initialization
    void Start () {
        initOnUpdate = false;
        hasReached = false;
        astar.setSeeker(monsterTransform);
        manager = transform.parent.GetComponent<Wolf_Statemanager>();
        timeToCheckCollision = 0.0f;
        timeBeforeChangeState = 0.0f;
    }

    // Update is called once per frame
    public override void UpdateState() {
        timeToCheckCollision += Time.deltaTime;
        if (!initOnUpdate)
        {
            rngX = new IntRange(-astar.getGrid().getGridSizeX() / 2, astar.getGrid().getGridSizeX()/2);
            rngY = new IntRange(-astar.getGrid().getGridSizeY() / 2, astar.getGrid().getGridSizeY()/2);
            initOnUpdate = true;
            //Debug.Log(rngX.Random.ToString());
            //Debug.Log(rngY.Random.ToString());
            target.transform.position = new Vector3(rngX.Random, rngY.Random,0);
        }
        astar.FindPath(monsterTransform.position, target.transform.position);
        if (astar.getGrid().NodeFromWorldPoint(target.transform.position).m_bWalkable && !hasReached && astar.getPath().Count != 0)
        {
            timeBeforeChangeState += Time.deltaTime;
            if(timeBeforeChangeState > 10.0f)
            {
                timeBeforeChangeState = 0;
                hasReached = false;
                initOnUpdate = false;
                manager.changeState("roam");
                return;
            }
            Vector3 dir = astar.getPath()[0].m_v2_worldPosition - (Vector2)monsterTransform.position;
            dir.Normalize();
            monsterRigidbody2D.velocity = dir * monsterInfo.speed * Time.deltaTime;
            if (monsterRigidbody2D.velocity.x > 0 && monsterRigidbody2D.velocity.x > Mathf.Abs(monsterRigidbody2D.velocity.y))
            {
                manager.changeAnim(3);
            }
            else if (monsterRigidbody2D.velocity.x < 0 && monsterRigidbody2D.velocity.x < -Mathf.Abs(monsterRigidbody2D.velocity.y))
            {
                manager.changeAnim(2);
            }
            else if (monsterRigidbody2D.velocity.y > 0 && monsterRigidbody2D.velocity.y > Mathf.Abs(monsterRigidbody2D.velocity.x))
            {
                manager.changeAnim(0);
            }
            else if (monsterRigidbody2D.velocity.y < 0 && monsterRigidbody2D.velocity.y < -Mathf.Abs(monsterRigidbody2D.velocity.x))
            {
                manager.changeAnim(1);
            }

                //if()
        }
        else
        {
            //change scene
            hasReached = false;
            initOnUpdate = false;
            manager.changeState("roam");
            //Debug.Log("roam");
        }

        //check if the player is in the range
        if(checkForPlayerInRange())
        {
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeState("chase");//change state
        }

        //check and do damage every one second
        if (timeToCheckCollision > 1)
        {
            if (checkForCollision())
            {
                timeToCheckCollision = 0;
            }
        }

        //When Health < 0, change to dead state
        if (health.m_health <= 0)
        {
            monsterRigidbody2D.velocity = Vector3.zero;
            manager.changeState("dead");//change state
        }

    }

    
}
