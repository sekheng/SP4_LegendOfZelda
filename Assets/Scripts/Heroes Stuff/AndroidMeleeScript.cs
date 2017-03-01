using UnityEngine;
using System.Collections;

public class AndroidMeleeScript : MonoBehaviour {
#if UNITY_ANDROID
    // To find the attack button
    private HeroMeleeButton heroAttack;

    private AndroidInventoryScript heroInventory;

	// Use this for initialization
	void Start () {
        heroAttack = GameObject.FindObjectOfType<HeroMeleeButton>();
        heroInventory = FindObjectOfType<AndroidInventoryScript>();
	}
	   
    public void pressedAttack()
    {
        //Debug.Log("Trying to attack");
        MessageSystem.instance.triggerEventCall("MeleeAttack");
        if (!heroInventory.isOpeningInventory)
        {
            if (heroAttack == null)
            {
                heroAttack = GameObject.FindObjectOfType<HeroMeleeButton>();
            }
            heroAttack.doAttack();
            //Debug.Log("Attacked");
            //else
        }
        //Debug.Log("Successful to attack");
    }
#else
    void Start()
    {
        gameObject.SetActive(false);
    }
#endif
}
