using UnityEngine;
using System.Collections;

/// <summary>
/// The logic of the splash scene!
/// TODO: Make it complicated!
/// </summary>
public class SplashScripts : MonoBehaviour {
    [Tooltip("Set the time for this splash")]
    public float m_timer = 3.0f;
    private float timeCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;
        if (timeCounter > m_timer)
            gameObject.GetComponent<GoSceneScript>().GoNextScene(); // Will change soon!
	}
}
