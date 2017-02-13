using UnityEngine;
using System.Collections;

public class Slime_Statemanager : MonoBehaviour {

    public Slime_IdleState idle;
    public Slime_RoamState roam;
    public Slime_DeadState dead;

    private State currState;

    void Start(){
        currState = idle;
    }
    
    void Update()
    {
        currState.UpdateState();
    }
}
