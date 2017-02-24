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

    // Used to keep track if the inventory is open or not
    private bool openInventory = false;
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
            if (!openInventory)
            {
                if (!LocalDataSingleton.instance.talking)
                    theHeroMeleeSystem.meleeAttack();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>().TryInteract();
            }
        }
        else if (Input.GetKeyDown(rangeButton) && !LocalDataSingleton.instance.talking)
        {
            theHeroRangeSystem.shootArrow();
        }
        else if (Input.GetKeyDown(inventoryButton))
        {
            // Need to make sure that the player is not talking and he is not opening inventory
            switch (openInventory)
            {
                case true:
                    if (LocalDataSingleton.instance.talking)
                    {
                        LocalDataSingleton.instance.Inventorycanvas.SetActive(!LocalDataSingleton.instance.Inventorycanvas.activeSelf);
                        LocalDataSingleton.instance.talking = LocalDataSingleton.instance.Inventorycanvas.activeSelf;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<HeroesMovement>().stopMovement();
                        openInventory = false;
                    }
                    break;
                default:
                    if (!LocalDataSingleton.instance.talking)
                    {
                        LocalDataSingleton.instance.Inventorycanvas.SetActive(!LocalDataSingleton.instance.Inventorycanvas.activeSelf);
                        LocalDataSingleton.instance.talking = LocalDataSingleton.instance.Inventorycanvas.activeSelf;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<HeroesMovement>().stopMovement();
                        openInventory = true;
                    }
                    break;
            }
            //if (!LocalDataSingleton.instance.talking && !openInventory)
            //{
            //    LocalDataSingleton.instance.Inventorycanvas.SetActive(!LocalDataSingleton.instance.Inventorycanvas.activeSelf);
            //    LocalDataSingleton.instance.talking = LocalDataSingleton.instance.Inventorycanvas.activeSelf;
            //    GameObject.FindGameObjectWithTag("Player").GetComponent<HeroesMovement>().stopMovement();
            //}
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
