using UnityEngine;
using System.Collections;

/// <summary>
/// For Different Heroes Movement!
/// So don't go and mess with RigidBody2D!!
/// </summary>
[RequireComponent(typeof(HeroAnimateScript))]
public class HeroesMovement : MonoBehaviour {
    [Tooltip("Speed of the hero")]
    public float m_speed = 5;

    // Every hero has a rigid body for collision!
    private Rigidbody2D heroPhysics;
    // To animate hero's movement
    private HeroAnimateScript heroAnimator;
    [HideInInspector]
    public Vector2 directionOfHero = new Vector2(1, 0);

	// Use this for initialization
	void Start () {
        heroPhysics = GetComponent<Rigidbody2D>();
        heroAnimator = GetComponent<HeroAnimateScript>();
	}
	
    // We will use this to move the player
    public void moveDirection(Vector2 zeDir)
    {
        // Applying the direction then the speed!
        heroPhysics.velocity = zeDir.normalized * m_speed;
        heroAnimator.moveAnimation(zeDir.normalized);

        if (GetComponent<MeleeScript>() != null)
            GetComponent<MeleeScript>().setDirection(zeDir.normalized);
        directionOfHero = zeDir.normalized;
    }

    // This will instantly set the velocity of hero to be 0!
    public void stopMovement()
    {
        // constantly allocating memory is bad!
        if (!Mathf.Approximately(0, heroPhysics.velocity.sqrMagnitude))
        {
            heroPhysics.velocity = new Vector2(0, 0);
            heroAnimator.stopAnimation();
        }
    }

    public void passInKeyPressed(KeyCode zeKey)
    {
        switch (zeKey)
        {
            case KeyCode.Z:
                stopMovement();
                break;
            default:
                Debug.Log("Something is wrong with current keypressed");
                break;
        }
    }
}
