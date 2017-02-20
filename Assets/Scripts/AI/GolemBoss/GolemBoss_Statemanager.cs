using UnityEngine;
using System.Collections;

public class GolemBoss_Statemanager : MonoBehaviour {

    public GolemBoss_IdleState idle;
    public GolemBoss_SleepState sleep;
    public GolemBoss_AwakeState awake;
    public GolemBoss_AttackState attack;
    public GolemBoss_DeadState dead;

    public Animator anim;
    public Rigidbody2D golemboss_RB;

    public State currState;

    void Start()
    {
        currState = idle;
        anim.Play("golemboss_attack");
    }

    void Update()
    {
        currState.UpdateState();
    }
}
