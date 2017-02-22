using UnityEngine;
using System.Collections;

/// <summary>
/// This is for player to press the certain key then it will activate the button!
/// </summary>
[RequireComponent(typeof(MeleeScript))]
public class HeroMeleeButton : MonoBehaviour {
    // Since this is for hero
    private MeleeScript theHeroMeleeSystem;
    // For hero range attack!
    private HeroRangeScript theHeroRangeSystem;
#if UNITY_ANDROID
    // have to interrupt player's movement!
    private PlayerDrag thePlayerJoystick;
#endif

    //[Tooltip("The Melee Button for mobile")]
    private GameObject theAttackButton;

#if UNITY_STANDALONE
    [Tooltip("The Key pressed for Melee Attack!")]
    public KeyCode attackButton = KeyCode.Z;
    [Tooltip("The Key to change weapon")]
    public KeyCode rangeButton = KeyCode.X;
    [Tooltip("The key to open inventory list")]
    public KeyCode inventoryButton = KeyCode.C;
    //[Tooltip("The Item UI name")]
    //public string m_ItemUI_Name = "Inventory Canvas for PC";
    // Need to keep track of the Item UI Canvas
    //private GameObject ItemUI;
#endif

    // Use this for initialization
	void Start () {
        theHeroMeleeSystem = GetComponent<MeleeScript>();
        theHeroRangeSystem = GetComponent<HeroRangeScript>();
	#if UNITY_STANDALONE
        //theAttackButton = GameObject.Find("AttackButton");
        //theAttackButton.SetActive(false);
        //ItemUI = GameObject.Find(m_ItemUI_Name);
        //Debug.Log("Item Canvas name: " + ItemUI.name);
        //ItemUI.gameObject.SetActive(false);
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
            //Need to see if the Player is talking to NPC or not
            if (!LocalDataSingleton.instance.talking)
                theHeroMeleeSystem.meleeAttack();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>().TryInteract();
        }
        else if (Input.GetKeyDown(rangeButton))
        {
            theHeroRangeSystem.shootArrow();
        }
        else if (Input.GetKeyDown(inventoryButton))
        {
            //if (ItemUI == null)
            //{
            //    ItemUI = GameObject.Find(m_ItemUI_Name);
            //}
            //ItemUI.SetActive(true);
            LocalDataSingleton.instance.Inventorycanvas.SetActive(!LocalDataSingleton.instance.Inventorycanvas.activeSelf);
        }
#endif
	}

#if UNITY_ANDROID
    public void doAttack()
    {
        if (thePlayerJoystick == null)
            thePlayerJoystick = GameObject.FindObjectOfType<PlayerDrag>();
        if (!LocalDataSingleton.instance.talking)
        {
            //thePlayerJoystick.fingerHasPressedIt = false;
            thePlayerJoystick.playerHasPressedButton();
            theHeroMeleeSystem.meleeAttack();
        } 
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>().TryInteract();
    }
#endif
}
