using UnityEngine;
using System.Collections;

public class Slime_DeadState : State {
    public float timeToSetInactive;
    //public SpriteRenderer sprite;
    private float accumTimeToSetInactive;

    public GameObject[] relicArray; // please drag in the prefabs.

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
            if (relicArray.Length > 0 && (Random.Range(0, 10) == 0)) //we insert a 10% chance for a relic to spawn.
            {
                int randomIndex = Random.Range(0, relicArray.Length);
                Instantiate(relicArray[randomIndex], monsterTransform.position, Quaternion.identity);
            }
            Destroy(monsterTransform.gameObject);
        }
    }
}
