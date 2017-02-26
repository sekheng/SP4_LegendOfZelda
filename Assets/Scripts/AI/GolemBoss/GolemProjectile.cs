using UnityEngine;
using System.Collections;

public class GolemProjectile : MonoBehaviour {

    public SpriteRenderer spriteR;

    public Vector3 direction;
    public float speed;
    public float damage; 

    public CircleCollider2D circleCollider;
    public Rigidbody2D rb;
    private RaycastHit2D[] collision;
    private GameObject projectileParticles;
    private SoundEffectsManager soundEffects;
    private string particlesystemname = "ParticleFX_RockDebris";

    // Use this for initialization
    void Start () {
        soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();
        spriteR.color = new Color(spriteR.color.r, spriteR.color.g, spriteR.color.b, 0);//0alpha
        speed = 0;
        direction = Vector3.zero;
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        projectileParticles = GameObject.Find(particlesystemname);
    }
	
	// Update is called once per frame
	void Update () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        if (projectileParticles != null)
        {
            projectileParticles = GameObject.Find(particlesystemname);
        }
        if (speed != 0 && direction != Vector3.zero)
        {
            if(!circleCollider.enabled)
            {
                circleCollider.enabled = true;
            }
            rb.velocity = direction * speed * Time.deltaTime;
            if(circleCollider.enabled)
            {
                if(checkForCollision())
                {
                    Destroy(gameObject);
                }
            }
        }
	}

    public bool checkForCollision()
    {
        collision = Physics2D.CircleCastAll(transform.position, circleCollider.bounds.size.x, Vector2.zero, 0);

        foreach (RaycastHit2D temp in collision)
        {
            if (temp.collider != null)
            {
                if (temp.collider.gameObject.tag == "Player" && temp.collider.gameObject.GetComponent<HealthScript>() != null)
                {
                    if (projectileParticles != null)
                    {
                        projectileParticles.transform.position = transform.position;
                        projectileParticles.GetComponent<ParticleScript>().playEffect();
                    }
                    temp.collider.gameObject.GetComponent<HealthScript>().modifyHealth(-damage);
                    soundEffects.playSound("smashing");
                    return true;
                }
                else if(temp.collider.gameObject.tag != "GolemBoss" && temp.collider.gameObject.tag != "GolemBoss_Projectile" && temp.collider.gameObject.tag != "Arrows")
                {
                    if (projectileParticles != null)
                    {
                        projectileParticles.transform.position = transform.position;
                        projectileParticles.GetComponent<ParticleScript>().playEffect();
                    }
                    //Debug.Log(temp.collider.gameObject.name);
                    return true;
                }
            }
        }
        return false;
    }
}
