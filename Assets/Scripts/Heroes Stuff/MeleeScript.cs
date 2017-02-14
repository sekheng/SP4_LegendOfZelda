using UnityEngine;
using System.Collections;

/// <summary>
/// A generic melee attack script
/// Can be applied to all monster and heroes
/// Right now it can be giving a definite damage every interval!
/// This will also check for any magic component too so as not to interfere with HealthScript
/// </summary>
public class MeleeScript : MonoBehaviour {
    [Tooltip("This will be the damage for every interval")]
    public int m_damage_ = 20;
    [Tooltip("The interval time")]
    public float m_time = 0.5f;
    // Count the timer!
    private float timeCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;
	}

    // Use call this function to attack the enemy!
    public void meleeAttack(GameObject zeOther)
    {
         if (zeOther.GetComponent<HealthScript>() != null)
         {
             zeOther.GetComponent<HealthScript>().modifyHealth(-m_damage_);
         }
    }
}
