using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Music
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float Volume = 1f;
    [Range(0.5f, 1.5f)]
    public float Pitch = 1f;
    public bool PlayOnAwake = false;

    private AudioSource source;

    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = Volume;
        source.pitch = Pitch;
        source.playOnAwake = PlayOnAwake;
        if (!source.isPlaying)
        {
            source.Play();
        }
    }
}

public class BgmManager : MonoBehaviour
{

    [SerializeField]
    Music[] music;

    Music mainMusic;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < music.Length; i++)
        {
            GameObject go = new GameObject("music" + i + ":" + music[i].name);
            go.transform.SetParent(this.transform);
            music[i].setSource(go.AddComponent<AudioSource>());

        }
    }


    public void Update()
    {
        if(mainMusic != null)
        {
            mainMusic.Play();
        }
    }
    public void changeMusic(string _name)
    {
        for (int i = 0; i < music.Length; i++)
        {
            if (music[i].name == _name && mainMusic != music[i] )
            {
                //music[i].Play();
                mainMusic = music[i];
                return;
            }
        }

        Debug.LogWarning("music" + _name + "not found");
    }
}
