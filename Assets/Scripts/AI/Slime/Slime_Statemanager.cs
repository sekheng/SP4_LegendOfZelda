using UnityEngine;
using System.Collections;

public class Slime_Statemanager : MonoBehaviour {

    public Slime_IdleState idle;
    public Slime_RoamState roam;
    public Slime_DeadState dead;
    
    public State currState;

    void Start(){
        currState = idle;
    }
    
    void Update()
    {
        currState.UpdateState();
    }

    public void changeState(string str)
    {
        if(str == "idle")
        {
            currState = idle;
        }
        else if(str == "roam")
        {
            currState = roam;
        }
        else if(str == "dead")
        {
            currState = dead;
        }
    }
}
