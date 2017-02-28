using UnityEngine;
using UnityEngine.UI;

public class PlayMenuControl : MonoBehaviour {

    public GameObject[] stuff;
    private int lookAt;
    private SoundEffectsManager soundEffects;

#if UNITY_STANDALONE
	// Use this for initialization
	void Start () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        lookAt = 0;
        GetComponent<RectTransform>().anchoredPosition = stuff[lookAt].GetComponent<RectTransform>().anchoredPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }

        // navigation
        if (Input.GetKeyDown(KeyCode.UpArrow) && lookAt > 0)
        {
            if (soundEffects != null)
            {
                soundEffects.playPositiveSound();

            }
            --lookAt;
            GetComponent<RectTransform>().anchoredPosition = stuff[lookAt].GetComponent<RectTransform>().anchoredPosition;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && lookAt < (stuff.Length - 1))
        {
            if (soundEffects != null)
            {
                soundEffects.playPositiveSound();

            }
            ++lookAt;
            GetComponent<RectTransform>().anchoredPosition = stuff[lookAt].GetComponent<RectTransform>().anchoredPosition;
        }

        //press enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (soundEffects != null)
            {
                soundEffects.playPositiveSound();

            }
            for (int i = 0; i < stuff.Length; i++ )
            {
                if(i == lookAt)
                {
                    stuff[i].GetComponent<LoadGameScript>().BroadcastMessage("beginLoadingData");
                    break;
                }
            }
        }
    }
#endif
}
