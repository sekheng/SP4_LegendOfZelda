using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsControllerPC : MonoBehaviour {

    public GameObject holderOfText;

    public GameObject helperText;

    private GameObject[] children;
    private int pointingAt = 0;
    private KeyCode temp;
    private float startingX;
    private Text extraInfo;

	// Use this for initialization
	void Start () {
        children = new GameObject[holderOfText.transform.childCount];
	    for (int i = 0; i < holderOfText.transform.childCount; i++)
        {
            children[i] = holderOfText.transform.GetChild(i).gameObject;
        }
        GetComponent<RectTransform>().position = children[0].transform.position;
        temp = KeyCode.None;
        startingX = helperText.GetComponent<RectTransform>().anchoredPosition.x;
        extraInfo = helperText.transform.GetChild(0).GetComponent<Text>(); 
	}
	
	// Update is called once per frame
	void Update () {
        //we handle the input from here.
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //we need to create a interface to activate some helper text
            helperText.SetActive(!helperText.activeSelf); // invert the activetext state;
            //allow for modification if the helpertext is active
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !helperText.activeSelf)
        {
            //SAVE HERE

            //only allow the thing to be closed if not editing a keybind
            transform.parent.gameObject.SetActive(false);
        }

        if (helperText.activeSelf)
        {
            helperText.GetComponent<RectTransform>().anchoredPosition = new Vector2(startingX, children[pointingAt].GetComponent<RectTransform>().anchoredPosition.y);
            string extra = "";
            switch (pointingAt)
            {
                case 0:
                    {
                        extra = KeyBindScript.upKey.ToString();
                        break;
                    }
                case 1:
                    {
                        extra = KeyBindScript.downKey.ToString();
                        break;
                    }
                case 2:
                    {
                        extra = KeyBindScript.leftKey.ToString();
                        break;
                    }
                case 3:
                    {
                        extra = KeyBindScript.rightKey.ToString();
                        break;
                    }
                case 4:
                    {
                        extra = KeyBindScript.attackKey.ToString();
                        break;
                    }
                case 5:
                    {
                        extra = KeyBindScript.rangeKey.ToString();
                        break;
                    }


            }
            extraInfo.text = "Key Bound to: " + extra;
            //handle the input here
            if(temp == KeyCode.None)
            {
                temp = FetchKey();
            }
        }
        if (temp != KeyCode.None && helperText.activeSelf)
        {
            switch (pointingAt)
            {
                case 0:
                {
                    KeyBindScript.upKey = temp;
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 1:
                {
                    KeyBindScript.downKey = temp;
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 2:
                {
                    KeyBindScript.leftKey = temp;
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 3:
                {
                    KeyBindScript.rightKey = temp;
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 4:
                {
                    KeyBindScript.attackKey = temp;
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 5:
                {
                    KeyBindScript.rangeKey = temp;
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
            }
        }
        else //allow navigation if not waiting
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && pointingAt > 0)
            {
                --pointingAt;
                GetComponent<RectTransform>().position = children[pointingAt].transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && pointingAt < (children.Length - 1))
            {
                ++pointingAt;
                GetComponent<RectTransform>().position = children[pointingAt].transform.position;
            }
        }
	}

    KeyCode FetchKey()
    {
        int e = System.Enum.GetNames(typeof(KeyCode)).Length;
        for (int i = (int)KeyCode.Backspace; i < e; i++)
        {
            if (Input.GetKeyDown((KeyCode)i) && (KeyCode)i != KeyCode.Return)
            {
                return (KeyCode)i;
            }
        }

        return KeyCode.None;
    }
}
