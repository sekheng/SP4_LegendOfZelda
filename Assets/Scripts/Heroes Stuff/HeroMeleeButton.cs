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
#if UNITY_STANDALONE
    private HeroRangeScript theHeroRangeSystem;
#else
    // have to interrupt player's movement!
    private PlayerDrag thePlayerJoystick;
#endif

    //[Tooltip("The Melee Button for mobile")]
    //private GameObject theAttackButton;

#if UNITY_STANDALONE
    //[Tooltip("The Key pressed for Melee Attack!")]
    //public KeyCode attackButton = KeyCode.Z;
    //[Tooltip("The Key to change weapon")]
    //public KeyCode rangeButton = KeyCode.X;
    //[Tooltip("The key to open inventory list")]
    //public KeyCode inventoryButton = KeyCode.C;

    // Used to keep track if the inventory is open or not
    private bool openInventory = false;
#endif

    // Use this for initialization
	void Start () {
        theHeroMeleeSystem = GetComponent<MeleeScript>();
	#if UNITY_STANDALONE
          theHeroRangeSystem = GetComponent<HeroRangeScript>();
      //theAttackButton = GameObject.Find("AttackButton");
        //theAttackButton.SetActive(false);
        //ItemUI = GameObject.Find(m_ItemUI_Name);
        //Debug.Log("Item Canvas name: " + ItemUI.name);
        //ItemUI.gameObject.SetActive(false);
#else
        thePlayerJoystick = GameObject.FindObjectOfType<PlayerDrag>();
        //theAttackButton = GameObject.Find("AttackButton");
#endif
    }
	
	// Update is called once per frame
	void Update () {
#if UNITY_STANDALONE
        // Need to check whether the player has pressed the key and theEnemy is not null
        if (Input.GetKeyDown(KeyBindScript.attackKey))
        {
            //Need to see if the Player is talking to NPC or not
            if (!openInventory)
            {
                if (!LocalDataSingleton.instance.talking)
                    theHeroMeleeSystem.meleeAttack();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>().TryInteract();
            }
        }
        else if (Input.GetKeyDown(KeyBindScript.rangeKey) && !LocalDataSingleton.instance.talking)
        {
            theHeroRangeSystem.shootArrow();
        }
        else if (Input.GetKeyDown(KeyBindScript.inventoryKey))
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
        else if (LocalDataSingleton.instance.InGamePauseCanvas.activeSelf)
        {
            // If the in game pause is active and player hit the attack button, then use that button!
            LocalDataSingleton.instance.InGamePauseCanvas.GetComponentInChildren<InGamePauseScript>().selectedTheButton();
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>().TryInteract();
        MessageSystem.instance.triggerEventCall("PressedAndroidMeleeButton");
   }
#endif
}
