using UnityEngine;
using System.Collections;

/// <summary>
/// Hardcoded range attack for hero
/// </summary>
[RequireComponent(typeof(HeroesMovement))]
public class HeroRangeScript : MonoBehaviour {
    [Tooltip("Firing rate of the arrow")]
    public float m_firingRate = 0.5f;
    // Need to know whr the hero is facing
    private HeroesMovement directionOfHero;
    // To animate the hero's shooting!
    private HeroAnimateScript heroAnimator;

    [Tooltip("Set the arrow for it to shoot")]
    public GameObject theArrow;

    private float m_TimeCounter = 0;

	// Use this for initialization
	void Start () {
        directionOfHero = GetComponent<HeroesMovement>();
        heroAnimator = GetComponent<HeroAnimateScript>();
	}
	
	// Update is called once per frame
	void Update () {
        m_TimeCounter += Time.deltaTime;
	}

    public void shootArrow()
    {
        if (m_TimeCounter > m_firingRate)
        {
            m_TimeCounter = 0;
            GameObject zeArrow = Instantiate(theArrow);
            zeArrow.BroadcastMessage("Start");
            zeArrow.transform.position = transform.position;
            zeArrow.BroadcastMessage("setDirection", directionOfHero.directionOfHero);
        }
    }
}
