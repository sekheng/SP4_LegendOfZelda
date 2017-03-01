using UnityEngine;
using System.Collections;

/// <summary>
/// This is only used for 
/// </summary>
public class TestingParticleScript : MonoBehaviour {
    // We need to test out whether is there any thing!
    private ParticleScript m_TheParticleSystem;
    [Tooltip("What is the time interval to play the particles")]
    public float m_timeInterval = 0.5f;
    // We need a time counter
    private float m_timeCounter = 0;

	// Use this for initialization
	void Start () {
        m_TheParticleSystem = GetComponent<ParticleScript>();
	}
	
    void Update()
    {
        m_timeCounter += Time.deltaTime;
        if (m_timeCounter > m_timeInterval)
        {
            m_TheParticleSystem.playEffect();
        }
    }
}
