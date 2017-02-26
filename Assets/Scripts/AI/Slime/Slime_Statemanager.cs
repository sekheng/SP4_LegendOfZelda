using UnityEngine;
using System.Collections;

public class Slime_Statemanager : MonoBehaviour {

    public Slime_IdleState idle;
    public Slime_RoamState roam;
    public Slime_DeadState dead;
    public Animator anim;
    
    public State currState;
    public SoundEffectsManager soundEffects;

    void Start(){
        soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();
        currState = idle;
        anim.Play("slime_idle");
    }
    
    void Update()
    {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
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
