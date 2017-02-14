using UnityEngine;
using System.Collections;

/// <summary>
/// This is for player to press the certain key then it will activate the button!
/// </summary>
[RequireComponent(typeof(MeleeScript))]
public class HeroMeleeButton : MonoBehaviour {
    // Since this is for hero
    private MeleeScript theHeroMeleeSystem;

    [Tooltip("The Melee Button for mobile")]
    public GameObject theAttackButton;

#if UNITY_STANDALONE
    [Tooltip("The Key pressed for Melee Attack!")]
    public KeyCode attackButton = KeyCode.Z;
#endif

    // Use this for initialization
	void Start () {
        theHeroMeleeSystem = GetComponent<MeleeScript>();
	#if UNITY_STANDALONE
        theAttackButton.SetActive(false);
#endif
    }
	
	// Update is called once per frame
	void Update () {
	#if UNITY_STANDALONE
        // Need to check whether the player has pressed the key and theEnemy is not null
        if (Input.GetKeyDown(attackButton))
        {
            theHeroMeleeSystem.meleeAttack();
        }
#endif
	}

#if UNITY_ANDROID
    public void doAttack()
    {
        theHeroMeleeSystem.meleeAttack();
    }
#endif
}
