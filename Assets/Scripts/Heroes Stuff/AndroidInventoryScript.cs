using UnityEngine;
using System.Collections;

/// <summary>
/// To activate the button script
/// </summary>
public class AndroidInventoryScript : MonoBehaviour {
    [Tooltip("The inventory UI")]
    public GameObject itemUI;
#if UNITY_ANDROID
    private PlayerDrag thePlayerDrag;

    //// Use this for initialization
    //void Start () {
	
    //}
	
    public void pressedTheButtonScript()
    {
        LocalDataSingleton.instance.talking = !LocalDataSingleton.instance.talking;
        //if (itemUI == null)
        //{
        //    itemUI = GameObject.Find(m_inventoryName);
        //}
        if (thePlayerDrag == null)
        {
            thePlayerDrag = FindObjectOfType<PlayerDrag>();
        }
        itemUI.SetActive(LocalDataSingleton.instance.talking);
        thePlayerDrag.playerHasPressedButton();
    }

#else
    void Start()
    {
        enabled = false;
    }
#endif
}
