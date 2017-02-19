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

    // TODO: Remove when finish debugging for Android
    //private TextMesh debugginMesh;

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

    /// <summary>
    /// Handling the range attack of the arrow. This is the consequences of not planning well from the start
    /// </summary>
    /// <param name="zeArrow">
    /// The arrow
    /// </param>
    /// <param name="zeVictim">
    /// The creature that got shot!
    /// </param>
    public void ManageArrowAttack(ArrowScript zeArrow, HealthScript zeVictim)
    {
        //if (particleSystem == null)
        //    particleSystem = GameObject.Find(m_NameOfParticleSys);
        zeVictim.modifyHealth(-zeArrow.m_damage);
        if (particleSystem != null)
        {
            //particleSystem.SetActive(true);
            particleSystem.transform.position = zeVictim.transform.position;
            particleSystem.GetComponent<ParticleScript>().playEffect();
        }
    }

    public void ManageMeleeAttack(MeleeScript attacker, HealthScript victim)
    {
        if (particleSystem == null)
        {
            Debug.Log("Finding blood particle: " + m_NameOfParticleSys);
            particleSystem = GameObject.Find(m_NameOfParticleSys);
        }
        //if (debugginMesh == null)
        //    debugginMesh = GameObject.Find("Remove TODO").GetComponent<TextMesh>();
        float m_damage_ = attacker.m_damage_;
        // If the enemy has a magic defense
        //Debug.Log("Passing to ManageMeleeAttack");
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
        //Debug.Log("Trying to play particle effects in: " + particleSystem.name);
        //debugginMesh.text = "Trying to find particle system: " + particleSystem.name;
        if (particleSystem != null)
        {
            Debug.Log("Playing particle effects");
            //debugginMesh.text = "Successful playing particle effects";
#if UNITY_ANDROID
            //particleSystem.SetActive(true);
            //particleSystem.GetComponent<ParticleScript>().resetToActive();
#endif
            particleSystem.transform.position = victim.transform.position;
            particleSystem.GetComponent<ParticleScript>().playEffect();
        }
    }

    private static AttackSystemScript cantTouchThis = null;

	// Use this for initialization
	void Start () {
        if (particleSystem == null)
        {
            Debug.Log("Finding blood particle: " + m_NameOfParticleSys);
            particleSystem = GameObject.Find(m_NameOfParticleSys);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Updating to find: " + m_NameOfParticleSys);
        if (particleSystem == null)
            particleSystem = GameObject.Find(m_NameOfParticleSys);
    }
}
