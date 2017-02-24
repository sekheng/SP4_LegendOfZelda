using UnityEngine;
using System.Collections;

/// <summary>
/// To activate the button script
/// </summary>
public class AndroidInventoryScript : MonoBehaviour {
    //[Tooltip("The inventory UI")]
    //public GameObject itemUI;
#if UNITY_ANDROID
    private PlayerDrag thePlayerDrag;
    [HideInInspector]
    public  bool isOpeningInventory = false;

    //// Use this for initialization
    //void Start () {
	
    //}
	
    public void pressedTheButtonScript()
    {
        // We shall cheat here becuz aint no time for SP!
        // So everytime we press a button, see whether Player GameoObject has exists!
        if (GameObject.FindGameObjectWithTag("Player") == null)
            return;

        if (thePlayerDrag == null)
        {
            thePlayerDrag = FindObjectOfType<PlayerDrag>();
        }
        switch (isOpeningInventory)
        {
            case true:
                if (LocalDataSingleton.instance.talking)
                {
                    LocalDataSingleton.instance.Inventorycanvas.SetActive(!LocalDataSingleton.instance.Inventorycanvas.activeSelf);
                    LocalDataSingleton.instance.talking = LocalDataSingleton.instance.Inventorycanvas.activeSelf;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<HeroesMovement>().stopMovement();
                    isOpeningInventory = false;
                    thePlayerDrag.playerHasPressedButton();
                }
          break;
            default:
          if (!LocalDataSingleton.instance.talking)
          {
              LocalDataSingleton.instance.Inventorycanvas.SetActive(!LocalDataSingleton.instance.Inventorycanvas.activeSelf);
              LocalDataSingleton.instance.talking = LocalDataSingleton.instance.Inventorycanvas.activeSelf;
              GameObject.FindGameObjectWithTag("Player").GetComponent<HeroesMovement>().stopMovement();
              thePlayerDrag.playerHasPressedButton();
              isOpeningInventory = true;
          }
         break;
        }
        //LocalDataSingleton.instance.talking = !LocalDataSingleton.instance.talking;
        //if (itemUI == null)
        //{
        //    itemUI = GameObject.Find(m_inventoryName);
        //}
        //itemUI.SetActive(LocalDataSingleton.instance.talking);
        //thePlayerDrag.playerHasPressedButton();
    }

#else
    void Start()
    {
        enabled = false;
    }
#endif
}
