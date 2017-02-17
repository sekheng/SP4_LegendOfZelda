using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuControl : MonoBehaviour {

    public Transform PlayButton, OptionsButton;

    public ToggleActive optionsMenu;

    public enum CURRENTBUTTON
    {
        PLAY,
        OPTION,
    };

    public CURRENTBUTTON selected;
#if UNITY_STANDALONE
	// Use this for initialization
	void Start () {
        selected = CURRENTBUTTON.PLAY;
        PlayButton.GetComponent<Button>().interactable = false;
        OptionsButton.GetComponent<Button>().interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(!LocalDataSingleton.instance.transform.GetChild(0).gameObject.activeSelf)
        {
            if (selected == CURRENTBUTTON.PLAY)
            {
                GetComponent<RectTransform>().anchoredPosition = new Vector3(575, 0, 0);

                PlayButton.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                //PlayButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
                PlayButton.GetComponent<Button>().interactable = true;

                OptionsButton.localScale = new Vector3(1, 1, 1);
                //OptionsButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                OptionsButton.GetComponent<Button>().interactable = false;

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.OPTION;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    LocalDataSingleton.instance.GoNext();
                }
            }
            else if (selected == CURRENTBUTTON.OPTION)
            {
                GetComponent<RectTransform>().anchoredPosition = new Vector3(575, -210, 0);

                OptionsButton.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                //OptionsButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
                OptionsButton.GetComponent<Button>().interactable = true;

                PlayButton.localScale = new Vector3(1, 1, 1);
                //PlayButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                PlayButton.GetComponent<Button>().interactable = false;

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.PLAY;
                }
                else if (Input.GetKeyDown(KeyCode.Return) && optionsMenu != null)
                {
                    optionsMenu.Toggle(LocalDataSingleton.instance.transform.GetChild(0).gameObject);
                }
            }
        }
    }
#endif
}
