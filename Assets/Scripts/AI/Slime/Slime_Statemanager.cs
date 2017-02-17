using UnityEngine;
using System.Collections;

public class Slime_Statemanager : MonoBehaviour {

    public Slime_IdleState idle;
    public Slime_RoamState roam;
    public Slime_DeadState dead;
    public Animator anim;
    
    public State currState;

    void Start(){
        currState = idle;
        anim.Play("slime_idle");
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
            anim.Play("slime_idle");
        }
        else if(str == "roam")
        {
            currState = roam;
            anim.Play("slime_walk");
        }
        else if(str == "dead")
        {
            currState = dead;
        }
    }
}
