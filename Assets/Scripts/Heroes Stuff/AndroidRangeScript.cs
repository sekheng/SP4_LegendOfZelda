using UnityEngine;
using System.Collections;

/// <summary>
/// Used only for hero's range attack in Android
/// </summary>
public class AndroidRangeScript : MonoBehaviour {
#if UNITY_ANDROID
    private HeroRangeScript heroRangeAttack;
    private PlayerDrag thePlayerJoystick;

	void Start()
    {
        thePlayerJoystick = GameObject.FindObjectOfType<PlayerDrag>();
    }

    public void pressedShoot()
    {
        //Debug.Log("Trying to shoot");
        if (!LocalDataSingleton.instance.talking)
        {
            if (heroRangeAttack == null)
            {
                //Debug.Log("Finding GameObject 1st");
                heroRangeAttack = GameObject.FindObjectOfType<HeroRangeScript>();
            }
            if (thePlayerJoystick == null)
                thePlayerJoystick = GameObject.FindObjectOfType<PlayerDrag>();
            thePlayerJoystick.playerHasPressedButton();
            heroRangeAttack.shootArrow();
        }
    }
#else
    void Start()
    {
        gameObject.SetActive(false);
    }
#endif
}
