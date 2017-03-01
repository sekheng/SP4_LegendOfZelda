using UnityEngine;
using System.Collections;

/// <summary>
/// A hardcoded melee attack script
/// Right now it can be giving a definite damage every interval!
/// This will also check for any magic component too so as not to interfere with HealthScript
/// We will be using raycast for collision detection!
/// </summary>
[RequireComponent(typeof(HeroAnimateScript))]
public class MeleeScript : MonoBehaviour {
    //[Tooltip("Range of the raycast")]
    //public float m_range = 5.0f;
    [Tooltip("This will be the damage for every interval")]
    public int m_damage_ = 20;
    [Tooltip("The interval time")]
    public float m_time = 0.5f;
    // Count the timer!
    private float timeCounter = 0;
    // To track the enemy
    //private Collider2D theEnemyCollider = null;
    // The melee range of the character!
    //private BoxCollider2D theMeleeAttackCollide;
    // The raycast of the ray!
    private Vector3 directionOfRay = new Vector3(1, 0, 0);

    // To animate Hero melee animation!
    private HeroAnimateScript heroMeleeAnim;
    // Keep track of hero's movement
    private HeroesMovement heroMove;
    private bool playerIsAttacking = false;

    [Tooltip("The delay time for the player to deal damage to the enemy")]
    public float m_delayInitialAttack = 0.3f;
    //Count the timer for the initial attack
    private float m_delayCounter = 0;
    private SoundEffectsManager soundEffects;

    // Use this for initialization
    void Start () {
        //BoxCollider2D[] AllTheCollider = GetComponents<BoxCollider2D>();
        //foreach (BoxCollider2D zeBox in AllTheCollider)
        //{
        //    // If the collider isTrigger
        //    if (zeBox.isTrigger)
        //    {
        //        theMeleeAttackCollide = zeBox;
        //        break;
        //    }
        //}
        heroMeleeAnim = GetComponent<HeroAnimateScript>();
        heroMove = GetComponent<HeroesMovement>();
        soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        timeCounter += Time.deltaTime;
        //Debug.DrawRay(transform.position, directionOfRay * m_range, Color.black);
        //Debug.Log(gameObject.name + ": " + directionOfRay);
        if (timeCounter > m_time && heroMeleeAnim != null)
            heroMeleeAnim.stopMeleeAttack();
        if (playerIsAttacking)
        {
            {
                // If the player is attacking, then we delay the attack before dealing the damage!
                m_delayCounter += Time.deltaTime;
                // If the player is able to attack!
                if (m_delayCounter > m_delayInitialAttack)
                {
                    //heroMeleeAnim.stopMeleeAttack();
                    m_delayCounter = 0;
                    playerIsAttacking = false;
                    Vector3 theBoxPosition = new Vector3(transform.localScale.x * directionOfRay.x, transform.localScale.y * directionOfRay.y);
                    //Debug.Log("Position of Box: " + (transform.position + transform.localScale));
                    RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + theBoxPosition
                        , transform.localScale, 0, Vector2.zero);
                    foreach (RaycastHit2D attacked in hits)
                    {
                        // This is so that it is not attackin itself!
                        if (!attacked.collider.gameObject.Equals(gameObject) && attacked.collider.GetComponent<HealthScript>() != null)
                        {
                            //Debug.Log("Successful attack");
                            if (soundEffects != null)
                            {
                                soundEffects.playSound("MeleeHit");
                            }
                            AttackSystemScript.instance.ManageMeleeAttack(this, attacked.collider.GetComponent<HealthScript>());
                        }
                    }
                }
            }
        }
	}

    // Use call this function to attack the enemy!
    public void meleeAttack()
    {
        //Debug.Log("Trying to attack");
        //RaycastHit2D theHitObject;
        //Debug.Log("Time Counter: " + timeCounter);
        //theHitObject = Physics2D.Raycast(transform.position, directionOfRay * m_range);
        //if (timeCounter > m_time && theEnemyCollider != null && theEnemyCollider.GetComponent<HealthScript>() != null 
        //    && theHitObject != null
        //    )
        //{
        //    //Debug.Log("Successful attack");
        //    //theEnemyCollider.GetComponent<HealthScript>().modifyHealth(-m_damage_);
        //    timeCounter = 0;
        //    AttackSystemScript.instance.ManageMeleeAttack(this, theEnemyCollider.GetComponent<HealthScript>());
        //}

        // TODO: Will change the dimension to sprite bounds if everything is sprite!
        if (timeCounter > m_time)
        {
            playerIsAttacking = true;
            heroMeleeAnim.meleeAttackAnimation();
            timeCounter = 0;
            heroMove.stopMovement();
            MessageSystem.instance.triggerEventCall("MeleeAttack");
            //Vector3 theBoxPosition = new Vector3(transform.localScale.x * directionOfRay.x, transform.localScale.y * directionOfRay.y);
            ////Debug.Log("Position of Box: " + (transform.position + transform.localScale));
            //RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + theBoxPosition
            //    , transform.localScale, 0, Vector2.zero);
            //foreach (RaycastHit2D attacked in hits)
            //{
            //    // This is so that it is not attackin itself!
            //    if (!attacked.collider.gameObject.Equals(gameObject) && attacked.collider.GetComponent<HealthScript>() != null)
            //    {
            //        //Debug.Log("Successful attack");
            //        AttackSystemScript.instance.ManageMeleeAttack(this, attacked.collider.GetComponent<HealthScript>());
            //    }
            //}
        }
        //Debug.Log("Raycasted Object: " + theHitObject.collider.gameObject.name);
    }

    public void setDirection(Vector3 zeDir)
    {
        directionOfRay = zeDir;
        // If player is trying to attack then move, cancel the attack!
        if (playerIsAttacking)
        {
            playerIsAttacking = false;
            timeCounter = 0;
            m_delayCounter = 0;
            heroMeleeAnim.stopMeleeAttack();
        }
    }

    /// <summary>
    /// Stops the attack but not the animation
    /// </summary>
    void stopMeleeAttack()
    {
        playerIsAttacking = false;
        timeCounter = 0;
        m_delayCounter = 0;
    }
}
