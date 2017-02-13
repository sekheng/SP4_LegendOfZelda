using UnityEngine;
using System.Collections;

/// <summary>
/// This is only for Melee only! I think monster can also use this!
/// For now hero only has 1 attacking stance and can only attack 1 time at every interval
/// And Continuosly attack the enemy which you will have to alter the DPS!
/// </summary>
public class HeroMelee : MonoBehaviour {
    [Tooltip("Damage Every time the hero press certain key!")]
    public int m_damage_ = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
