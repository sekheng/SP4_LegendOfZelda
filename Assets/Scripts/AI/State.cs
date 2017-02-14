using UnityEngine;
using System.Collections;

public class State : MonoBehaviour {

    public MonsterInfomation monsterInfo;
    public Transform monsterTransform;
    public Rigidbody2D monsterRigidbody2D;
    public Sprite monsterSprite;
    public HealthScript health;
	public virtual void UpdateState () {
	}

    public virtual bool checkForCollision()
    {
        Vector3 dir = transform.TransformDirection(Vector3.up);
        RaycastHit2D[] hit = Physics2D.BoxCastAll(monsterTransform.position, monsterSprite.bounds.size,0,Vector2.zero);

        foreach (RaycastHit2D temp in hit)
        {
            if (temp.collider != null)
            {
                if (temp.collider.gameObject.tag == "Player" && temp.collider.gameObject.GetComponent<HealthScript>() != null)
                {
                    temp.collider.gameObject.GetComponent<HealthScript>().m_health -= monsterInfo.dps;
                    return true;
                }
            }
        }
        return false;
    }

}
