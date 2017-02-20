using UnityEngine;
using System.Collections;

/// <summary>
/// Use control the arrow
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class ArrowScript : MonoBehaviour {
    [Tooltip("Set the speed of the arrow")]
    public float m_speed = 10.0f;
    [Tooltip("Set the damage of the arrow")]
    public int m_damage = 10;
    [Tooltip("Set lifetime of arrow so that it will eventually dies out if it happens to go out of boundary")]
    public float m_lifeTime = 10;
    // To countdown
    private float m_timeCounter = 0;

    // Meant to rotate the direction of the arrow
    private Vector3 directionOfArrow = new Vector3(-1, 0, 0);
    // Used to set the velocity of the arrow!
    private Rigidbody2D theArrowPhysics;
    // When the arrow collides, it shall spawn the dust effect!
    private GameObject arrowParticleEffect;
    [Tooltip("The name of the particle effect for this arrow")]
    public string m_particleSystemName = "ParticleFX_ArrowCollide";

	// Use this for initialization
	void Start () {
        //transform.rotation = Quaternion.FromToRotation(zeDirection, Vector3.down);
        theArrowPhysics = GetComponent<Rigidbody2D>();
        arrowParticleEffect = GameObject.Find(m_particleSystemName);
        //Debug.Log("Instantiated");
	}

    void Update()
    {
        m_timeCounter += Time.deltaTime;
        // Kills the object when it's lifetime is over!
        if (m_timeCounter > m_lifeTime)
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
	
    /// <summary>
    /// Helps to set the rotation of the arrow using quaternion!
    /// </summary>
    /// <param name="zeDir">
    /// Direction of where you want the arrow to be facing
    /// </param>
    public void setDirection(Vector2 zeDir)
    {
        // This means the arrow is travelling horizontally and needs to be dealt separately because bugs
        theArrowPhysics.velocity = zeDir * m_speed;
        //if (!Mathf.Approximately(zeDir.x, 0))
        {
            directionOfArrow = new Vector3(zeDir.x *= -1.0f, zeDir.y, 0);
            //Debug.Log("Change arrow direction x: " + zeDir);
        }
        //else
        //    directionOfArrow = new Vector3(zeDir.x, zeDir.y);
        transform.rotation = Quaternion.FromToRotation(directionOfArrow, Vector3.down);
    }

    // This will be used as checking whether it hits the object or not!
    void OnTriggerEnter2D(Collider2D zeVictim)
    {
        // Need to make sure it isn't friendly fire!
        if (!zeVictim.tag.Equals("Player") && zeVictim.GetComponent<HealthScript>() != null)
        {
            if (arrowParticleEffect != null)
            {
                arrowParticleEffect.transform.position = transform.position;
                arrowParticleEffect.GetComponent<ParticleScript>().playEffect();
            }
            AttackSystemScript.instance.ManageArrowAttack(this, zeVictim.GetComponent<HealthScript>());
            //Debug.Log("Hit the " + zeVictim.gameObject.name);
        }
        Destroy(gameObject);
    }
}
