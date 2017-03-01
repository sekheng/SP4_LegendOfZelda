using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

#if UNITY_STANDALONE
    private bool MovedUp, MovedDown, MovedLeft, MovedRight;
#else
    private Vector3 playerPos = Vector3.zero;
#endif
    private bool Attacked, Ranged;

    public enum TUTORIAL_STAGE
    {
        movement,
        attack
    }
    [HideInInspector]
    public TUTORIAL_STAGE current;
	// Use this for initialization
	void Start () {
        Attacked = false;
        Ranged = false;
#if UNITY_STANDALONE
        MovedUp = false;
        MovedDown = false;
        MovedLeft = false;
        MovedRight = false;
#endif
        
        current = TUTORIAL_STAGE.movement;
	}
	
	// Update is called once per frame
	void Update () {

        if(current.Equals(TUTORIAL_STAGE.movement))
        {
#if UNITY_STANDALONE
            MovedUp = (Input.GetKeyDown(KeyBindScript.upKey) ? true : MovedUp);
            MovedDown = (Input.GetKeyDown(KeyBindScript.downKey) ? true : MovedDown);
            MovedLeft = (Input.GetKeyDown(KeyBindScript.leftKey) ? true : MovedLeft);
            MovedRight = (Input.GetKeyDown(KeyBindScript.rightKey) ? true : MovedRight);

            if (!MovedUp && !MovedDown && !MovedLeft && !MovedRight)
            {
                GetComponent<Text>().text = "Use the " + KeyBindScript.leftKey + ", " +
                    KeyBindScript.rightKey + ", " +
                    KeyBindScript.upKey + " and " +
                    KeyBindScript.downKey + " to move around.";
            }
            else if (MovedUp && MovedDown && MovedLeft && MovedRight)
            {
                GetComponent<Text>().text = "";
                current = TUTORIAL_STAGE.attack;
            }
#else
            if(playerPos.Equals(Vector3.zero))
            {
                playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            }
            if(GameObject.FindGameObjectWithTag("Player").transform.position.Equals(playerPos))
            {
                GetComponent<Text>().text = "Use the joystick to move around.";
            }
            else
            {
                GetComponent<Text>().text = "";
                current = TUTORIAL_STAGE.attack;
                MessageSystem.instance.setListener("MeleeAttack", PlayerPressedMelee);
                MessageSystem.instance.setListener("RangeAttack", PlayerPressedRange);
            }
#endif
        }
        else if(current.Equals(TUTORIAL_STAGE.attack))
        {
#if UNITY_STANDALONE
            Attacked = (Input.GetKeyDown(KeyBindScript.attackKey) ? true : Attacked);
            Ranged = (Input.GetKeyDown(KeyBindScript.rangeKey) ? true : Ranged);
            if (!Attacked && !Ranged)
            {
                GetComponent<Text>().text = "Use the " + KeyBindScript.attackKey + " to melee, or interact with NPCs, "
                    + KeyBindScript.rangeKey + " to fire an arrow.";
            }
#else
            if(!Attacked && !Ranged)
            {
                GetComponent<Text>().text = "Press the sword button to melee and the bow button for ranged attack.";
            }
#endif
            else if (Attacked && Ranged)
            {
                GetComponent<Text>().text = "";
                //current = TUTORIAL_STAGE.talk_toNPC;
#if UNITY_ANDROID
                MessageSystem.instance.removeListener("MeleeAttack", PlayerPressedMelee);
                MessageSystem.instance.removeListener("RangeAttack", PlayerPressedRange);
#endif
                // we are done with the tutorial lol
                Destroy(transform.parent.gameObject);
            }
        }
	}

    //void OnEnable()
    //{
    //    MessageSystem.instance.setListener("MeleeAttack", PlayerPressedMelee);
    //    MessageSystem.instance.setListener("RangeAttack", PlayerPressedRange);
    //}

    //void OnDisable()
    //{
    //    MessageSystem.instance.removeListener("MeleeAttack", PlayerPressedMelee);
    //    MessageSystem.instance.removeListener("RangeAttack", PlayerPressedRange);
    //}

    public void PlayerPressedMelee()
    {
        Attacked = true;
    }
    public void PlayerPressedRange()
    {
        Ranged = true;
    }
}
