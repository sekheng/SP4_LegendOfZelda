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
    [Tooltip("Play how many times at a time")]
    public int m_times = 2;
    [Tooltip("How many spacing should the number of times be player in X-axis")]
    public float m_SpacingX = 50.0f;
    // we need to know it's original position
    private Vector3 particleSystemPos;

	// Use this for initialization
	void Start () {
        m_TheParticleSystem = GetComponent<ParticleScript>();
        particleSystemPos = transform.position;
	}
	
    void Update()
    {
        m_timeCounter += Time.deltaTime;
        if (m_timeCounter > m_timeInterval)
        {
            for (int zeNum = 0; zeNum < m_times; ++zeNum)
            {
                m_TheParticleSystem.stopEffect();
                m_TheParticleSystem.playEffect();
                transform.position = new Vector3(transform.position.x + m_SpacingX, transform.position.y, transform.position.z);
            }
            transform.position = particleSystemPos;
            m_timeCounter = 0;
        }
    }
}
