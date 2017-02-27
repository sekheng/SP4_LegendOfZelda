using UnityEngine;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour {

    public Transform PlayButton, OptionsButton, ExitButton;
    public Transform PlayMenu;
    public ToggleActive optionsMenu;


    public enum CURRENTBUTTON
    {
        PLAY,
        OPTION,
        EXIT,
    };

    public CURRENTBUTTON selected;
#if UNITY_STANDALONE
    private bool controllable = true;
	// Use this for initialization
	void Start () {
        selected = CURRENTBUTTON.PLAY;
        PlayButton.GetComponent<Button>().interactable = false;
        OptionsButton.GetComponent<Button>().interactable = false;
        ExitButton.GetComponent<Button>().interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(controllable)
        {
            if (!LocalDataSingleton.instance.transform.GetChild(0).gameObject.activeSelf)
            {
                if (selected == CURRENTBUTTON.PLAY)
                {
                    PlayButton.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    PlayButton.GetComponent<Button>().interactable = true;

                    OptionsButton.localScale = new Vector3(1, 1, 1);
                    OptionsButton.GetComponent<Button>().interactable = false;

                    ExitButton.localScale = new Vector3(1, 1, 1);
                    ExitButton.GetComponent<Button>().interactable = false;

                    if (!PlayMenu.gameObject.activeSelf)
                    {
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            selected = CURRENTBUTTON.EXIT;
                        }
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            selected = CURRENTBUTTON.OPTION;
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            PlayMenu.gameObject.SetActive(!PlayMenu.gameObject.activeSelf);
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (!PlayMenu.gameObject.activeSelf)
                        {
                            PlayMenu.gameObject.SetActive(!PlayMenu.gameObject.activeSelf);
                        }
                    }
                }
                else if (selected == CURRENTBUTTON.OPTION)
                {
                    OptionsButton.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    OptionsButton.GetComponent<Button>().interactable = true;

                    PlayButton.localScale = new Vector3(1, 1, 1);
                    PlayButton.GetComponent<Button>().interactable = false;

                    ExitButton.localScale = new Vector3(1, 1, 1);
                    ExitButton.GetComponent<Button>().interactable = false;

                    if(!LocalDataSingleton.instance.transform.GetChild(0).gameObject.activeSelf)
                    {
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            selected = CURRENTBUTTON.PLAY;
                        }
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            selected = CURRENTBUTTON.EXIT;
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            LocalDataSingleton.instance.transform.GetChild(0).gameObject.SetActive(false);
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        LocalDataSingleton.instance.transform.GetChild(0).gameObject.SetActive(!LocalDataSingleton.instance.transform.GetChild(0).gameObject.activeSelf);
                    }
                }
                else if (selected == CURRENTBUTTON.EXIT)
                {
                    ExitButton.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    ExitButton.GetComponent<Button>().interactable = true;

                    OptionsButton.localScale = new Vector3(1, 1, 1);
                    OptionsButton.GetComponent<Button>().interactable = false;

                    PlayButton.localScale = new Vector3(1, 1, 1);
                    PlayButton.GetComponent<Button>().interactable = false;

                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        selected = CURRENTBUTTON.OPTION;
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        selected = CURRENTBUTTON.PLAY;
                    }
                    else if (Input.GetKeyDown(KeyCode.Return))
                    {
                        LocalDataSingleton.instance.QuitApp();
                    }
                }
            }
        }
    }
#endif
}
