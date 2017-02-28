using UnityEngine;
using System.Collections;

public class MovingRock : MonoBehaviour {

    Rigidbody2D rb;
    SoundEffectsManager soundEffects;

	// Use this for initialization
	void Start () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        if(rb.velocity != Vector2.zero)
        {
            if (soundEffects != null)
            {
                soundEffects.playSound("movingObj");

            }
        }
    }
}
