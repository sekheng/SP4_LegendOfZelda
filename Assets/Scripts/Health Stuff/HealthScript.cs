using UnityEngine;
using System.Collections;

/// <summary>
/// This is a generic health script that can be applied to heroes and monster!
/// </summary>
public class HealthScript : MonoBehaviour {
    [Tooltip("Set the float value of the HP")]
    public float m_health = 100;
    // So that there can be a guage to it!
    [HideInInspector]
    public float max_health;

    // Using Awake so that max health won't be messed around with every time the application restarts
    void Awake()
    {
        max_health = m_health;
    }
	
    public void modifyHealth(float zeNum)
    {
        // Helps to limit the number of health
        m_health = Mathf.Clamp(m_health + zeNum, 0, max_health);
        //Debug.Log("Health: " + m_health);
    }
    public void modifyHealth(int zeNum)
    {
        // Helps to limit the number of health
        m_health = Mathf.Clamp(m_health + zeNum, 0, max_health);
        //Debug.Log("Health: " + m_health);
    }

    /// <summary>
    /// Reset the max health and current health will also become the max health
    /// </summary>
    /// <param name="zeNum">
    /// The value of health to pass in. Only if it is more than 0!
    /// </param>
    public void resetToMaxHealth(float zeNum)
    {
        if (zeNum > Mathf.Epsilon)
            m_health = max_health = zeNum;
    }
}
