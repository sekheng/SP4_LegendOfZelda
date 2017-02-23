using UnityEngine;
using System.Collections;

public class MonsterInfomation : MonoBehaviour {

    //public float health;//store the health
    public float dps;//store the damage done per second
    public float speed;//speed of the monster
    public HealthScript health;
    public bool toReduceSpeedWhenHalfHealth;

    [HideInInspector]
    public float maxSpeed;

    void Start()
    {
        maxSpeed = speed;
    }
    void Update()
    {
        if (toReduceSpeedWhenHalfHealth)
        {
            if (health.m_health < health.max_health / 2)
            {
                speed *= 0.5f;
            }
            else
            {
                speed = maxSpeed;
            }
        }
    }
}
