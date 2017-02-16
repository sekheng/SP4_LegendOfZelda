using UnityEngine;
using System.Collections;

public class Wolf_DeadState : State {

    public float timeToSetInactive;
    private float accumTimeToSetInactive;

    void Start()
    {
        accumTimeToSetInactive = 0.0f;
    }

    public override void UpdateState()
    {
        accumTimeToSetInactive += Time.deltaTime;// time for the death animation

        if (accumTimeToSetInactive > timeToSetInactive)
        {
            Destroy(monsterTransform.gameObject);
        }
    }
}
