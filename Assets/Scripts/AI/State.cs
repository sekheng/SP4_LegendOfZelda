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
        collision = Physics2D.BoxCastAll(monsterTransform.position, circleCollider.bounds.size, 0,Vector2.zero);

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
    public virtual bool checkForPlayerInRange()
    {
        inRange = Physics2D.CircleCastAll(monsterTransform.position, circleCollider.bounds.size.x * 5, Vector2.zero, 0);

        foreach (RaycastHit2D temp in inRange)
        {
            if (temp.collider != null)
            {
                if (temp.collider.gameObject.tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }

}
