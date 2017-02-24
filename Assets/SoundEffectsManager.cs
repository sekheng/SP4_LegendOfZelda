using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    //[Range(0f,1f)]
    //public float Volume = 1f;
    [Range(0.5f, 1.5f)]
    public float Pitch = 1f;
    public bool PlayOnAwake = false;

    [HideInInspector]
    public Slider volumeSlider;

    private AudioSource source;

    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volumeSlider.value;
        source.pitch = Pitch;
        source.playOnAwake = PlayOnAwake;
    }
}

public class SoundEffectsManager : MonoBehaviour {

    public Slider volumeSlider;
    [SerializeField]
    Sound[] sounds;

    private float Volume;

	// Use this for initialization
	void Start () {
        Volume = volumeSlider.value;
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + ":" + sounds[i].name);
            go.transform.SetParent(this.transform); 
            sounds[i].volumeSlider = volumeSlider; 
            sounds[i].setSource(go.AddComponent<AudioSource>());

        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(Volume == volumeSlider.value)
        {
            return;
        }
        else
        {
            Volume = volumeSlider.value;
            for (int i = 0; i < sounds.Length; i++)
            {
                //GameObject go = new GameObject("Sound_" + i + ":" + sounds[i].name);
                sounds[i].volumeSlider = volumeSlider;
                //sounds[i].setSource(go.AddComponent<AudioSource>());

            }
        }
	}

    public void playSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        Debug.LogWarning("Sound effect" + _name + "not found");
    }
}
