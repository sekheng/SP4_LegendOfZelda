using UnityEngine;
using System.Collections;

/// <summary>
/// This is for player to press the certain key then it will activate the button!
/// </summary>
[RequireComponent(typeof(MeleeScript))]
public class HeroMeleeButton : MonoBehaviour {
    // Since this is for hero
    private MeleeScript theHeroMeleeSystem;
#if UNITY_ANDROID
    // have to interrupt player's movement!
    private PlayerDrag thePlayerJoystick;
#endif

    //[Tooltip("The Melee Button for mobile")]
    private GameObject theAttackButton;

#if UNITY_STANDALONE
    [Tooltip("The Key pressed for Melee Attack!")]
    public KeyCode attackButton = KeyCode.Z;
#endif

    // Use this for initialization
	void Start () {
        theHeroMeleeSystem = GetComponent<MeleeScript>();
	#if UNITY_STANDALONE
        //theAttackButton.SetActive(false);
#else
        thePlayerJoystick = GameObject.FindObjectOfType<PlayerDrag>();
        theAttackButton = GameObject.Find("AttackButton");
#endif
    }
	
	// Update is called once per frame
	void Update () {
	#if UNITY_STANDALONE
        // Need to check whether the player has pressed the key and theEnemy is not null
        if (Input.GetKeyDown(attackButton))
        {
            theHeroMeleeSystem.meleeAttack();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>().TryInteract();
        }
#endif
	}

#if UNITY_ANDROID
    public void doAttack()
    {
        thePlayerJoystick.fingerHasPressedIt = false;
        theHeroMeleeSystem.meleeAttack();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>().TryInteract();
    }
#endif
}
