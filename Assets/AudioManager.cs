using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public AudioSource BackgroundMusic;
    public Slider slider;
    //private AudioSource[] arrayOfAudioSource;
    //private 
    // Use this for initialization
    void Start () {
        //arrayOfAudioSource = GetComponentsInChildren<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeBgm(AudioClip clip)
    {
        if (BackgroundMusic.clip.name != clip.name)
        {
            BackgroundMusic.Stop();
            BackgroundMusic.clip = clip;
            BackgroundMusic.Play();
        }
        else
        {
            return;
        }
    }
}
