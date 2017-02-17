using UnityEngine;
using System.Collections;

public class AndroidMeleeScript : MonoBehaviour {
#if UNITY_ANDROID
    // To find the attack button
    private HeroMeleeButton heroAttack;

	// Use this for initialization
	void Start () {
        heroAttack = GameObject.FindObjectOfType<HeroMeleeButton>();
	}
	   
    public void pressedAttack()
    {
        if (heroAttack == null)
            heroAttack = GameObject.FindObjectOfType<HeroMeleeButton>();
        else
         heroAttack.doAttack();
    }
#else
    void Start()
    {
        gameObject.SetActive(false);
    }
#endif
}
