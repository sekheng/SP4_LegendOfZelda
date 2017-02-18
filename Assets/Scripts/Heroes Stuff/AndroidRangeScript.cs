using UnityEngine;
using System.Collections;

/// <summary>
/// Used only for hero's range attack in Android
/// </summary>
public class AndroidRangeScript : MonoBehaviour {
#if UNITY_ANDROID
    private HeroRangeScript heroRangeAttack;
	
    public void pressedShoot()
    {
        //Debug.Log("Trying to shoot");
        if (heroRangeAttack == null)
        {
            //Debug.Log("Finding GameObject 1st");
            heroRangeAttack = GameObject.FindObjectOfType<HeroRangeScript>();
        }
        heroRangeAttack.shootArrow();
    }
#else
    void Start()
    {
        gameObject.SetActive(false);
    }
#endif
}
