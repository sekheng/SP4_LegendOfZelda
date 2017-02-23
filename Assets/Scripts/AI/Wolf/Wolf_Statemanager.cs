using UnityEngine;
using System.Collections;

public class Wolf_Statemanager : MonoBehaviour {

    public Wolf_IdleState idle;
    public Wolf_SearchState search;
    public Wolf_RoamState roam;
    public Wolf_DeadState dead;
    public Wolf_ChaseState chase;
    public Wolf_AttackState attack;
    public Wolf_FollowState follow;
    public Wolf_GrowlState growl;
    public Wolf_EscapeState escape;
    public Animator anim;
    public Rigidbody2D wolf_RB;

    public State currState;
    public float healthBeforeDamaged;

    void Start()
    {
        currState = roam;
        anim.Play("wolf_walk_left");
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
        else if (str == "search")
        {
            currState = search;
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
        else if(str == "follow")
        {
            currState = follow;
        }
        else if(str == "growl")
        {
            currState = growl;
        }
        else if (str == "escape")
        {
            currState = escape;
        }
    }

    public void changeAnim(int dir)
    {
        if (dir == 3)
        {
            anim.Play("wolf_walk_right");
        }
        else if (dir == 2)
        {
            anim.Play("wolf_walk_left");
        }
        else if (dir == 0)
        {
            anim.Play("wolf_walk_up");
        }
        else if (dir == 1)
        {
            anim.Play("wolf_walk_down");
        }
        else if (dir == 7)
        {
            anim.Play("wolf_walk_right_slow");
        }
        else if (dir == 6)
        {
            anim.Play("wolf_walk_left_slow");
        }
        else if (dir == 4)
        {
            anim.Play("wolf_walk_up_slow");
        }
        else if (dir == 5)
        {
            anim.Play("wolf_walk_down_slow");
        }
    }
}
