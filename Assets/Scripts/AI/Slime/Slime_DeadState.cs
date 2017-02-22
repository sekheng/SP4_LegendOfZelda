using UnityEngine;
using System.Collections;

public class Slime_DeadState : State {
    public float timeToSetInactive;
    //public SpriteRenderer sprite;
    private float accumTimeToSetInactive;

    void Start()
    {
        accumTimeToSetInactive = 0.0f;
        if(timeToSetInactive > 1)
        {
            timeToSetInactive = 1;
        }
    }

    public override void UpdateState()
    {
        accumTimeToSetInactive += Time.deltaTime;// time for the death animation
        monsterSpriteRenderer.color = new Color(monsterSpriteRenderer.color.r, monsterSpriteRenderer.color.g, monsterSpriteRenderer.color.b, (timeToSetInactive - accumTimeToSetInactive));
        if(accumTimeToSetInactive > timeToSetInactive)
        {
            Destroy(monsterTransform.gameObject);
        }
    }
}
