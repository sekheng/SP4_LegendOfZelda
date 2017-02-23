using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    
    public Slider slider;
    private AudioSource[] arrayOfAudioSource;
    //private 
    // Use this for initialization
    void Start () {
        arrayOfAudioSource = GetComponentsInChildren<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
