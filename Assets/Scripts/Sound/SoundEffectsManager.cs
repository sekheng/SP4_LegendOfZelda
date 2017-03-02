using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float Volume = 1f;
    [Range(0, 255)]
    public int Priority = 125;
    [Range(0.5f, 1.5f)]
    public float Pitch = 1f;
    public bool PlayOnAwake = false;
    public bool AllowResetWhilePlaying = false;

    private AudioSource source;

    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = Volume;
        source.priority = Priority;
        source.pitch = Pitch;
        source.playOnAwake = PlayOnAwake;
        if (!source.isPlaying || AllowResetWhilePlaying)
        {
            source.Play();
        }
    }
}

public class SoundEffectsManager : MonoBehaviour {

    [SerializeField]
    Sound[] sounds;
    Sound positiveSound;
    Sound negativeSound;

    // Use this for initialization
    void Start () {
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + ":" + sounds[i].name);
            go.transform.SetParent(this.transform); 
            sounds[i].setSource(go.AddComponent<AudioSource>());

        }
	}
	
	//// Update is called once per frame
	//void Update () {
	//    if(Volume == volumeSlider.value)
 //       {
 //           return;
 //       }
 //       else
 //       {
 //           Volume = volumeSlider.value;
 //           for (int i = 0; i < sounds.Length; i++)
 //           {
 //               //GameObject go = new GameObject("Sound_" + i + ":" + sounds[i].name);
 //               sounds[i].volumeSlider = volumeSlider;
 //               //sounds[i].setSource(go.AddComponent<AudioSource>());

 //           }
 //       }
	//}

    public void playSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                //Debug.Log(sounds[i].)
                return;
            }
        }

        Debug.LogWarning("Sound effect" + _name + "not found");
    }

    public void playPositiveSound()
    {
        if(positiveSound == null)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == "positive")
                {
                    sounds[i].Play();
                    positiveSound = sounds[i];
                    return;
                }
            }
        }
        else
        {
            positiveSound.Play();
        }
    }
    public void playNegativeSound()
    {
        if (negativeSound == null)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == "negative")
                {
                    sounds[i].Play();
                    negativeSound = sounds[i];
                    return;
                }
            }
        }
        else
        {
            negativeSound.Play();
        }
    }

}
