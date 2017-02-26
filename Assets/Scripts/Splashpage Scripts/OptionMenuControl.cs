using UnityEngine;
using UnityEngine.UI;

public class OptionMenuControl : MonoBehaviour {

    public Transform Sound, Controls;

    public enum CURRENTBUTTON
    {
        SOUND,
        CONTROL,
    };

    public CURRENTBUTTON selected;
#if UNITY_STANDALONE
	// Use this for initialization
	void Start () {
        selected = CURRENTBUTTON.SOUND;
        Sound.GetComponent<Button>().interactable = false;
        Controls.GetComponent<Button>().interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(!LocalDataSingleton.instance.MainCanvas.transform.GetChild(7).gameObject.activeSelf &&
            !LocalDataSingleton.instance.MainCanvas.transform.GetChild(8).gameObject.activeSelf)
        {
            if (selected == CURRENTBUTTON.SOUND)
            {
                Sound.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Sound.GetComponent<Button>().interactable = true;

                Controls.localScale = new Vector3(1, 1, 1);
                Controls.GetComponent<Button>().interactable = false;

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.CONTROL;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    LocalDataSingleton.instance.MainCanvas.transform.GetChild(7).gameObject.SetActive(true);
                }
            }
            else if (selected == CURRENTBUTTON.CONTROL)
            {
                Sound.localScale = new Vector3(1, 1, 1);
                Sound.GetComponent<Button>().interactable = false;

                Controls.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Controls.GetComponent<Button>().interactable = true;

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selected = CURRENTBUTTON.SOUND;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    LocalDataSingleton.instance.MainCanvas.transform.GetChild(8).gameObject.SetActive(true);
                }
            }
        }
    }
#endif
}
