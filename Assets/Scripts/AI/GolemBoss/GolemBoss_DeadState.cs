using UnityEngine;
using System.Collections;

public class GolemBoss_DeadState : State
{
    public float timeToSetInactive;
    private float accumTimeToSetInactive;

    void Start()
    {
        accumTimeToSetInactive = 0.0f;
        if (timeToSetInactive > 1)
        {
            timeToSetInactive = 1;
        }
    }

    public override void UpdateState()
    {
        accumTimeToSetInactive += Time.deltaTime;// time for the death animation
        monsterSpriteRenderer.color = new Color(monsterSpriteRenderer.color.r, monsterSpriteRenderer.color.g, monsterSpriteRenderer.color.b,(timeToSetInactive - accumTimeToSetInactive));
        //Debug.Log(timeToSetInactive.ToString());
        if (accumTimeToSetInactive > timeToSetInactive)
        {
            if (monsterTransform.parent != null)
            {
                Destroy(monsterTransform.parent.gameObject);
                LocalDataSingleton.instance.GoNext();
            }
            else
            {
                Destroy(monsterTransform.gameObject);
            }
        }
    }
}
