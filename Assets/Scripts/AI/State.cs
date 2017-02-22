using UnityEngine;
using System.Collections;

public class State : MonoBehaviour {

    public MonsterInfomation monsterInfo;
    public Transform monsterTransform;
    public Rigidbody2D monsterRigidbody2D;
    public Sprite monsterSprite;// well this is now more of a sprite to check for collision
    public SpriteRenderer monsterSpriteRenderer;
    public HealthScript health;
    public CircleCollider2D circleCollider;

    protected float timeToCheckCollision;
    protected float distToPlayer;

    protected GameObject thePlayer;

    protected RaycastHit2D[] collision;
    protected RaycastHit2D[] inRange;

    private void Start()
    {
        timeToCheckCollision = 0;
    }

    public virtual void UpdateState () {

	}

    public virtual bool checkForCollision()
    {
        collision = Physics2D.CircleCastAll(monsterTransform.position, circleCollider.bounds.size.x, Vector2.zero, 0);

        foreach (RaycastHit2D temp in collision)
        {
            if (temp.collider != null)
            {
                if (temp.collider.gameObject.tag == "Player" && temp.collider.gameObject.GetComponent<HealthScript>() != null)
                {
                    temp.collider.gameObject.GetComponent<HealthScript>().modifyHealth(-monsterInfo.dps);
                    return true;
                }
            }
        }
        return false;
    }
    public virtual bool checkForPlayerInRange(Vector3 playerPos, float range)
    {
        //inRange = Physics2D.CircleCastAll(monsterTransform.position, circleCollider.bounds.size.x * 5, Vector2.zero, 0);

        //foreach (RaycastHit2D temp in inRange)
        //{
        //    if (temp.collider != null)
        //    {
        //        if (temp.collider.gameObject.tag == "Player")
        //        {
        //            return true;
        //        }
        //    }
        //}
        //return false;
        distToPlayer = (playerPos - monsterTransform.position).magnitude;
        if(distToPlayer < range)
        {
            return true;
        }

        return false;
    }

}
