using UnityEngine;
using System.Collections;

/// <summary>
/// A system to calculate the attack then pass the attack to it!
/// </summary>
public class AttackSystemScript : MonoBehaviour {
    // The particle system object
    private GameObject particleSystem;
    [Tooltip("The name of the particle system")]
    public string m_NameOfParticleSys = "ParticleFX_Blood";

    public static AttackSystemScript instance
    {
        get
        {
            if (!cantTouchThis)
            {
                cantTouchThis = GameObject.FindObjectOfType<AttackSystemScript>();
                cantTouchThis.Start();
            }
            return cantTouchThis;
        }
    }

    public void ManageMeleeAttack(MeleeScript attacker, HealthScript victim)
    {
        if (particleSystem == null)
            particleSystem = GameObject.Find(m_NameOfParticleSys);
        float m_damage_ = attacker.m_damage_;
        // If the enemy has a magic defense
        Debug.Log("Passing to ManageMeleeAttack");
        if (victim.GetComponent<MagicDefendScript>() != null)
        {
            // If the attack has no magic attack, reduce the damage!
            if (attacker.GetComponent<MagicAttackScript>() == null)
            {
                m_damage_ *= 0.75f;
            }
            else
            {
                m_damage_ *= attacker.GetComponent<MagicAttackScript>().CompareEffectiveOfThisToOtherMagic(victim.GetComponent<MagicDefendScript>());
            }
        }
        // If the victim has no magic defense and the attack has a magic attack, just multiply the health!
        else if (attacker.GetComponent<MagicAttackScript>() != null)
        {
            m_damage_ *= 1.25f;
        }
        victim.modifyHealth(-m_damage_);
        if (particleSystem != null)
            particleSystem.GetComponent<ParticleSystem>().Play();
    }

    private static AttackSystemScript cantTouchThis = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
