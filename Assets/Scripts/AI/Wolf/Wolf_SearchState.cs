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
    bool initOnUpdate;
    bool hasReached;



	// Use this for initialization
	void Start () {
        initOnUpdate = false;
        hasReached = false;
        astar.setSeeker(monsterTransform);
        manager = transform.parent.GetComponent<Wolf_Statemanager>();

    }

    // Update is called once per frame
    public override void UpdateState() {
        if(!initOnUpdate)
        {
            rngX = new IntRange(-astar.getGrid().getGridSizeX() / 2, astar.getGrid().getGridSizeX()/2);
            rngY = new IntRange(-astar.getGrid().getGridSizeY() / 2, astar.getGrid().getGridSizeY()/2);
            initOnUpdate = true;
            //Debug.Log(rngX.Random.ToString());
            //Debug.Log(rngY.Random.ToString());
            target.transform.position = new Vector3(rngX.Random, rngY.Random,0);
        }
        if(astar.getGrid().NodeFromWorldPoint(target.transform.position).m_bWalkable && !hasReached)
        {
            //Debug.Log(monsterTransform.gameObject.name);
        }
        else
        {
            //change scene
            hasReached = false;
            initOnUpdate = false;
            manager.changeState("roam");
        }

    }

    
}
