using UnityEngine;
using System.Collections;

/// <summary>
/// This is for player to press the certain key then it will activate the button!
/// </summary>
[RequireComponent(typeof(HeroMelee))]
public class HeroMeleeButton : MonoBehaviour {
    private HeroMelee theHeroMeleeSystem;


	// Use this for initialization
	void Start () {
	    theHeroMeleeSystem = GetComponent<HeroMelee>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
