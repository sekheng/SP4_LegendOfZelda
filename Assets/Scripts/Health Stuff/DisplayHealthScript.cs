using UnityEngine;
using System.Collections;

/// <summary>
/// For now, it shall be used to debug health!
/// </summary>
[RequireComponent(typeof(HealthScript))]
public class DisplayHealthScript : MonoBehaviour {
    // Used to keep track of character's health
    private HealthScript characterHealth;
    private TextMesh theDebuggingText;

	// Use this for initialization
	void Start () {
        characterHealth = GetComponent<HealthScript>();
        theDebuggingText = GetComponentInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        theDebuggingText.text = characterHealth.m_health.ToString();
	}
}
