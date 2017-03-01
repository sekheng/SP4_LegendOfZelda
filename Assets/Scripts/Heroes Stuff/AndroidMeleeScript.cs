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
        MessageSystem.instance.triggerEventCall("MeleeAttack");
        if (!heroInventory.isOpeningInventory)
        {
            if (heroAttack == null)
            {
                heroAttack = GameObject.FindObjectOfType<HeroMeleeButton>();
            }
            else
            {
                heroAttack.doAttack();
            }
            if (heroInventory == null)
                heroInventory = FindObjectOfType<AndroidInventoryScript>();
            //else
        }
    }
#else
    void Start()
    {
        gameObject.SetActive(false);
    }
#endif
}
