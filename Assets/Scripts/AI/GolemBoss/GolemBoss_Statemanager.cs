using UnityEngine;
using System.Collections;

public class GolemBoss_Statemanager : MonoBehaviour {

    public GolemBoss_IdleState idle;
    public GolemBoss_SleepState sleep;
    public GolemBoss_AwakeState awake;
    public GolemBoss_AttackState attack;
    public GolemBoss_DeadState dead;

    public BoxCollider2D box;

    public Animator anim;
    public Rigidbody2D golemboss_RB;

    public State currState;

    void Start()
    {
        currState = sleep;
        anim.Play("golemboss_sleep");
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
        else if (str == "sleep")
        {
            currState = sleep;
        }
        else if (str == "attack")
        {
            currState = attack;
        }
        else if (str == "awake")
        {
            currState = awake;
        }
        else if (str == "dead")
        {
            currState = dead;
        }
    }

    public void changeAnim(string animName)
    {
        anim.Play(animName);
    }

    public BoxCollider2D getBox()
    {
        return box;
    }
}
