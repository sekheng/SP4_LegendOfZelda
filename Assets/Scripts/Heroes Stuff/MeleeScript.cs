using UnityEngine;
using System.Collections;

/// <summary>
/// A generic melee attack script
/// Can be applied to all monster and heroes
/// Right now it can be giving a definite damage every interval!
/// This will also check for any magic component too so as not to interfere with HealthScript
/// We will be using raycast for collision detection!
/// </summary>
public class MeleeScript : MonoBehaviour {
    [Tooltip("Range of the raycast")]
    public float m_range = 5.0f;
    [Tooltip("This will be the damage for every interval")]
    public int m_damage_ = 20;
    [Tooltip("The interval time")]
    public float m_time = 0.5f;
    // Count the timer!
    private float timeCounter = 0;
    // To track the enemy
    private Collider2D theEnemyCollider = null;
    // The melee range of the character!
    private BoxCollider2D theMeleeAttackCollide;
    // The raycast of the ray!
    private Vector3 directionOfRay = new Vector3(1, 0, 0);

	// Use this for initialization
	void Start () {
        BoxCollider2D[] AllTheCollider = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D zeBox in AllTheCollider)
        {
            // If the collider isTrigger
            if (zeBox.isTrigger)
            {
                theMeleeAttackCollide = zeBox;
                break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;
        Debug.DrawRay(transform.position, directionOfRay * m_range, Color.black);
        //Debug.Log(gameObject.name + ": " + directionOfRay);
	}

    // Use call this function to attack the enemy!
    public void meleeAttack()
    {
        //Debug.Log("Trying to attack");
        RaycastHit2D theHitObject;
        //Debug.Log("Time Counter: " + timeCounter);
        theHitObject = Physics2D.Raycast(transform.position, directionOfRay * m_range);
        if (timeCounter > m_time && theEnemyCollider != null && theEnemyCollider.GetComponent<HealthScript>() != null 
            && theHitObject != null
            )
        {
            //Debug.Log("Successful attack");
            //theEnemyCollider.GetComponent<HealthScript>().modifyHealth(-m_damage_);
            //timeCounter = 0;
            AttackSystemScript.instance.ManageMeleeAttack(this, theEnemyCollider.GetComponent<HealthScript>());
        }
        //Debug.Log("Raycasted Object: " + theHitObject.collider.gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        theEnemyCollider = other;
        //Debug.Log("Enemy: " + other.gameObject.name);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        theEnemyCollider = null;
    }

    public void setDirection(Vector3 zeDir)
    {
        directionOfRay = zeDir;
    }
}
