using UnityEngine;
using System.Collections;

public class Slime_IdleState : State {
    private float timeAwayToRoam;
    void Start()
    {
        timeAwayToRoam = 0;
    }
    public override void UpdateState()
    {
        timeAwayToRoam += Time.deltaTime;
        //TODO change state to roam and maybe have random time 
        if(timeAwayToRoam > 5.0f)
        {

        }
    }
}
