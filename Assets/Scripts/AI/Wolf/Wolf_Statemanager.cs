using UnityEngine;
using System.Collections;

public class Wolf_Statemanager : MonoBehaviour {

    public Wolf_IdleState idle;
    public Wolf_RoamState roam;
    public Wolf_DeadState dead;
    public Wolf_ChaseState chase;
    public Wolf_AttackState attack;

    public State currState;

    void Start()
    {
        currState = idle;
    }

    void Update()
    {
        currState.UpdateState();
    }

    public void changeState(string str)
    {
        if (str == "idle")
        {
            currState = idle;
        }
        else if (str == "roam")
        {
            currState = roam;
        }
        else if (str == "dead")
        {
            currState = dead;
        }
        else if (str == "chase")
        {
            currState = chase;
        }
        else if (str == "attack")
        {
            currState = attack;
        }
    }
}
