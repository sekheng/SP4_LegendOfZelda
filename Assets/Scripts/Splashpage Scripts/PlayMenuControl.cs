using UnityEngine;
using UnityEngine.UI;

public class PlayMenuControl : MonoBehaviour {

    public Transform NewGame, Load1, Load2, Load3;

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
        selected = PlayMenuControl.CURRENTBUTTON.NEWGAME;
        NewGame.GetComponent<Button>().interactable = false;
        Load1.GetComponent<Button>().interactable = false;
        Load2.GetComponent<Button>().interactable = false;
        Load3.GetComponent<Button>().interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
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
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.LOAD1;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //LocalDataSingleton.instance.GoNext();
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
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.LOAD2;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    //ADD CODES FOR THE LOADING.
                    Load1.BroadcastMessage("beginLoadingData");
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
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.LOAD3;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    //ADD CODES FOR THE LOADING.
                    Load2.BroadcastMessage("beginLoadingData");
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
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.NEWGAME;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    //ADD CODES FOR THE LOADING.
                    Load3.BroadcastMessage("beginLoadingData");
                }
            }
        }
    }
#endif
}
