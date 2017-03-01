using UnityEngine;
using UnityEngine.UI;

public class OptionMenuControl : MonoBehaviour {

    public GameObject[] stuff;
    public GameObject WindowsCTRL, SoundCTRL;
#if UNITY_STANDALONE
    private int lookAt = 0;

    private SoundEffectsManager soundEffects;

    // Use this for initialization
    void Start () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        GetComponent<RectTransform>().anchoredPosition = stuff[lookAt].GetComponent<RectTransform>().anchoredPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        if (!WindowsCTRL.activeSelf)
        {
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
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if (soundEffects != null)
                {
                    soundEffects.playPositiveSound();

                }
                switch (lookAt)
                {
                    case 0:
                    {
                        WindowsCTRL.SetActive(!WindowsCTRL.activeSelf);
                        break;
                    }
                    case 1:
                    {
                        //some other things
                        SoundCTRL.SetActive(!SoundCTRL.activeSelf);
                        break;
                    }
                }
            }
        }
    }
#endif
}
