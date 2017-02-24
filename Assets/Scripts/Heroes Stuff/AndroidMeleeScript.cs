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
        if (!heroInventory.isOpeningInventory)
        {
            if (heroAttack == null)
                heroAttack = GameObject.FindObjectOfType<HeroMeleeButton>();
            if (heroInventory == null)
                heroInventory = FindObjectOfType<AndroidInventoryScript>();
            //else
            heroAttack.doAttack();
        }
    }
#else
    void Start()
    {
        gameObject.SetActive(false);
    }
#endif
}
