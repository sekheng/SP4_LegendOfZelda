using UnityEngine;
using System.Collections;

/// <summary>
/// For Different Heroes Movement!
/// So don't go and mess with RigidBody2D!!
/// </summary>
public class HeroesMovement : MonoBehaviour {
    [Tooltip("Speed of the hero")]
    public float m_speed = 5;

    // Every hero has a rigid body for collision!
    private Rigidbody2D heroPhysics;

	// Use this for initialization
	void Start () {
        heroPhysics = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // We will use this to move the player
    public void moveDirection(Vector2 zeDir)
    {
        // Applying the direction then the speed!
        heroPhysics.velocity = zeDir.normalized * m_speed;
    }

    // This will instantly set the velocity of hero to be 0!
    public void stopMovement()
    {
        // constantly allocating memory is bad!
        if (!Mathf.Approximately(0, heroPhysics.velocity.sqrMagnitude))
        {
            heroPhysics.velocity = new Vector2(0, 0);
        }
    }
}
