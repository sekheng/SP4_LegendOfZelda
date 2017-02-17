using UnityEngine;
using System.Collections;

public class Wolf_DeadState : State {

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
        monsterSpriteRenderer.color = new Color(0, 0, 0, (timeToSetInactive - accumTimeToSetInactive));
        if (accumTimeToSetInactive > timeToSetInactive)
        {
            Destroy(monsterTransform.gameObject);
        }
    }
}
