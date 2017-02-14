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
        Physics2D.Raycast(monsterTransform.position, -Vector3.up, monsterSprite.bounds.size.x);
        RaycastHit2D[] hit = Physics2D.BoxCastAll(monsterTransform.position, monsterSprite.bounds.size * 1.5f,0,Vector2.zero);

        foreach (RaycastHit2D temp in hit)
        {
            if (temp.collider != null)
            {
                if (temp.collider.gameObject.tag == "Player" && temp.collider.gameObject.GetComponent<HealthScript>() != null)
                {
                    temp.collider.gameObject.GetComponent<HealthScript>().m_health -= monsterInfo.dps;
                    return true;
                    //Debug.Log(temp.collider.gameObject.tag);
                }
                // Debug.Log(temp.collider.gameObject.name);
            }
        }
        return false;
    }

}
