using UnityEngine;
using UnityEngine.UI;

public class PlayMenuControl : MonoBehaviour {

    public Transform NewGame, Load1, Load2, Load3;
    private SoundEffectsManager soundEffects;

    public enum CURRENTBUTTON
    {
        NEWGAME,
        LOAD1,
        LOAD2,
        LOAD3,
    };

    public CURRENTBUTTON selected;
#if UNITY_STANDALONE
	// Use this for initialization
	void Start () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        selected = PlayMenuControl.CURRENTBUTTON.NEWGAME;
        NewGame.GetComponent<Button>().interactable = false;
        Load1.GetComponent<Button>().interactable = false;
        Load2.GetComponent<Button>().interactable = false;
        Load3.GetComponent<Button>().interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        if (!LocalDataSingleton.instance.transform.GetChild(0).gameObject.activeSelf)
        {
            if (selected == CURRENTBUTTON.NEWGAME)
            {
                NewGame.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                NewGame.GetComponent<Button>().interactable = true;

                Load1.localScale = new Vector3(1, 1, 1);
                Load1.GetComponent<Button>().interactable = false;

                Load2.localScale = new Vector3(1, 1, 1);
                Load2.GetComponent<Button>().interactable = false;

                Load3.localScale = new Vector3(1, 1, 1);
                Load3.GetComponent<Button>().interactable = false;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    selected = CURRENTBUTTON.LOAD3;
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.LOAD1;
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //LocalDataSingleton.instance.GoNext();
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                    NewGame.BroadcastMessage("beginLoadingData");
                }
            }
            else if (selected == CURRENTBUTTON.LOAD1)
            {
                NewGame.localScale = new Vector3(1, 1, 1);
                NewGame.GetComponent<Button>().interactable = false;

                Load1.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Load1.GetComponent<Button>().interactable = true;

                Load2.localScale = new Vector3(1, 1, 1);
                Load2.GetComponent<Button>().interactable = false;

                Load3.localScale = new Vector3(1, 1, 1);
                Load3.GetComponent<Button>().interactable = false;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    selected = CURRENTBUTTON.NEWGAME;
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.LOAD2;
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    //ADD CODES FOR THE LOADING.
                    Load1.BroadcastMessage("beginLoadingData");
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
            }
            else if (selected == CURRENTBUTTON.LOAD2)
            {
                NewGame.localScale = new Vector3(1, 1, 1);
                NewGame.GetComponent<Button>().interactable = false;

                Load1.localScale = new Vector3(1, 1, 1);
                Load1.GetComponent<Button>().interactable = false;

                Load2.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Load2.GetComponent<Button>().interactable = true;

                Load3.localScale = new Vector3(1, 1, 1);
                Load3.GetComponent<Button>().interactable = false;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    selected = CURRENTBUTTON.LOAD1;
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.LOAD3;
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    //ADD CODES FOR THE LOADING.
                    Load2.BroadcastMessage("beginLoadingData");
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
            }
            else if (selected == CURRENTBUTTON.LOAD3)
            {
                NewGame.localScale = new Vector3(1, 1, 1);
                NewGame.GetComponent<Button>().interactable = false;

                Load1.localScale = new Vector3(1, 1, 1);
                Load1.GetComponent<Button>().interactable = false;

                Load2.localScale = new Vector3(1, 1, 1);
                Load2.GetComponent<Button>().interactable = false;

                Load3.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Load3.GetComponent<Button>().interactable = true;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    selected = CURRENTBUTTON.LOAD2;
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.NEWGAME;
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    //ADD CODES FOR THE LOADING.
                    Load3.BroadcastMessage("beginLoadingData");
                    if (soundEffects != null)
                    {
                        soundEffects.playPositiveSound();

                    }
                }
            }
        }
    }
#endif
}
