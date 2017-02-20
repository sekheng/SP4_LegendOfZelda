using UnityEngine;
using System.Collections;

public class GolemBoss_SleepState : State
{

    private GolemBoss_Statemanager manager;

    // Use this for initialization
    void Start()
    {
        manager = transform.parent.GetComponent<GolemBoss_Statemanager>();
    }

    // Update is called once per frame
    public override void UpdateState()
    {

        if (health.m_health <= 0)
        {
            manager.changeState("dead");//change state
        }
    }
}
