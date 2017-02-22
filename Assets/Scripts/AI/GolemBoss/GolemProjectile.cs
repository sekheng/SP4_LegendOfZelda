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

    // Use this for initialization
    void Start () {
        spriteR.color = new Color(spriteR.color.r, spriteR.color.g, spriteR.color.b, 0);//0alpha
        speed = 0;
        direction = Vector3.zero;
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if(speed != 0 && direction != Vector3.zero)
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
                    temp.collider.gameObject.GetComponent<HealthScript>().modifyHealth(-damage);
                    return true;
                }
                else if(temp.collider.gameObject.tag != "GolemBoss" && temp.collider.gameObject.tag != "GolemBoss_Projectile" && temp.collider.gameObject.tag != "Arrows")
                {
                    //Debug.Log(temp.collider.gameObject.name);
                    return true;
                }
            }
        }
        return false;
    }
}
