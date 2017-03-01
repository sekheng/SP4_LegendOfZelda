using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsControllerPC : MonoBehaviour {

    public GameObject holderOfText;

    public GameObject helperText;

    private GameObject[] children;
    private int pointingAt = 0;
    private KeyCode temp;
    private KeyCode[] allKeys;
    private float startingX;
    private Text extraInfo;

    private SoundEffectsManager soundEffects;

	// Use this for initialization
	void Start () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        children = new GameObject[holderOfText.transform.childCount];
	    for (int i = 0; i < holderOfText.transform.childCount; i++)
        {
            children[i] = holderOfText.transform.GetChild(i).gameObject;
        }
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, children[pointingAt].GetComponent<RectTransform>().anchoredPosition.y);
        temp = KeyCode.None;
        startingX = helperText.GetComponent<RectTransform>().anchoredPosition.x;

        allKeys = new KeyCode[7];
        allKeys[0] = KeyBindScript.upKey;
        allKeys[1] = KeyBindScript.downKey;
        allKeys[2] = KeyBindScript.leftKey;
        allKeys[3] = KeyBindScript.rightKey;
        allKeys[4] = KeyBindScript.attackKey;
        allKeys[5] = KeyBindScript.rangeKey;
        allKeys[6] = KeyBindScript.inventoryKey;

	}
	
	// Update is called once per frame
	void Update () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        //we'll just hardcode the fixed text, everytime.
        children[0].transform.GetChild(0).GetComponent<Text>().text = KeyBindScript.upKey.ToString();
        children[1].transform.GetChild(0).GetComponent<Text>().text = KeyBindScript.downKey.ToString();
        children[2].transform.GetChild(0).GetComponent<Text>().text = KeyBindScript.leftKey.ToString();
        children[3].transform.GetChild(0).GetComponent<Text>().text = KeyBindScript.rightKey.ToString();
        children[4].transform.GetChild(0).GetComponent<Text>().text = KeyBindScript.attackKey.ToString();
        children[5].transform.GetChild(0).GetComponent<Text>().text = KeyBindScript.rangeKey.ToString();
        children[6].transform.GetChild(0).GetComponent<Text>().text = KeyBindScript.inventoryKey.ToString();

        //we handle the input from here.
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //we need to create a interface to activate some helper text
            helperText.SetActive(!helperText.activeSelf); // invert the activetext state;
            if (soundEffects != null)
            {
                soundEffects.playPositiveSound();

            }
            //allow for modification if the helpertext is active
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !helperText.activeSelf)
        {
            //SAVE HERE. Hardcoding unfortunately but the fastest way to save keybinds
            int zeUpKeyValue = (int)KeyBindScript.upKey;
            int zeDownKeyValue = (int)KeyBindScript.downKey;
            int zeRightKeyValue = (int)KeyBindScript.rightKey;
            int zeLeftKeyValue = (int)KeyBindScript.leftKey;
            int zeAttackKeyValue = (int)KeyBindScript.attackKey;
            int zeRangeKeyValue = (int)KeyBindScript.rangeKey;
            int zeInventoryKeyValue = (int)KeyBindScript.inventoryKey;
            MySQLiteHandler.instance.saveSpecificResult(KeyBindScript.theSQLiteTable, KeyBindScript.upKeyField, zeUpKeyValue.ToString());
            MySQLiteHandler.instance.saveSpecificResult(KeyBindScript.theSQLiteTable, KeyBindScript.downKeyField, zeDownKeyValue.ToString());
            MySQLiteHandler.instance.saveSpecificResult(KeyBindScript.theSQLiteTable, KeyBindScript.rightKeyField, zeRightKeyValue.ToString());
            MySQLiteHandler.instance.saveSpecificResult(KeyBindScript.theSQLiteTable, KeyBindScript.leftKeyField, zeLeftKeyValue.ToString());
            MySQLiteHandler.instance.saveSpecificResult(KeyBindScript.theSQLiteTable, KeyBindScript.attackKeyField, zeAttackKeyValue.ToString());
            MySQLiteHandler.instance.saveSpecificResult(KeyBindScript.theSQLiteTable, KeyBindScript.rangeKeyField, zeRangeKeyValue.ToString());
            MySQLiteHandler.instance.saveSpecificResult(KeyBindScript.theSQLiteTable, KeyBindScript.inventoryKeyField, zeInventoryKeyValue.ToString());
            if (soundEffects != null)
            {
                soundEffects.playNegativeSound();

            }
            //only allow the thing to be closed if not editing a keybind
            transform.parent.gameObject.SetActive(false);
        }

        if (helperText.activeSelf)
        {
            helperText.GetComponent<RectTransform>().anchoredPosition = new Vector2(startingX, children[pointingAt].GetComponent<RectTransform>().anchoredPosition.y);
            children[pointingAt].transform.GetChild(0).GetComponent<Text>().text = "";
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
                    for (int i = 0; i < allKeys.Length; i++)
                    {
                        if (i != pointingAt)
                        {
                            if (allKeys[i] == temp)
                            {
                                allKeys[i] = KeyCode.None;
                            }
                        }
                    }

                    // we refresh the keys
                    KeyBindScript.downKey = allKeys[1];
                    KeyBindScript.leftKey = allKeys[2]; 
                    KeyBindScript.rightKey = allKeys[3];
                    KeyBindScript.attackKey = allKeys[4];
                    KeyBindScript.rangeKey = allKeys[5];
                    KeyBindScript.inventoryKey = allKeys[6];

                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 1:
                {
                    KeyBindScript.downKey = temp;
                    for (int i = 0; i < allKeys.Length; i++)
                    {
                        if (i != pointingAt)
                        {
                            if (allKeys[i] == temp)
                            {
                                Debug.Log("test");
                                allKeys[i] = KeyCode.None;
                            }
                        }
                    }

                    // we refresh the keys
                    KeyBindScript.upKey = allKeys[0];
                    KeyBindScript.leftKey = allKeys[2];
                    KeyBindScript.rightKey = allKeys[3];
                    KeyBindScript.attackKey = allKeys[4];
                    KeyBindScript.rangeKey = allKeys[5];
                    KeyBindScript.inventoryKey = allKeys[6];
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 2:
                {
                    KeyBindScript.leftKey = temp;
                    for (int i = 0; i < allKeys.Length; i++)
                    {
                        if (i != pointingAt)
                        {
                            if (allKeys[i] == temp)
                            {
                                Debug.Log("test");
                                allKeys[i] = KeyCode.None;
                            }
                        }
                    }

                    // we refresh the keys
                    KeyBindScript.upKey = allKeys[0];
                    KeyBindScript.downKey = allKeys[1];
                    KeyBindScript.rightKey = allKeys[3];
                    KeyBindScript.attackKey = allKeys[4];
                    KeyBindScript.rangeKey = allKeys[5];
                    KeyBindScript.inventoryKey = allKeys[6];
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 3:
                {
                    KeyBindScript.rightKey = temp;
                    for (int i = 0; i < allKeys.Length; i++)
                    {
                        if (i != pointingAt)
                        {
                            if (allKeys[i] == temp)
                            {
                                Debug.Log("test");
                                allKeys[i] = KeyCode.None;
                            }
                        }
                    }

                    // we refresh the keys
                    KeyBindScript.upKey = allKeys[0];
                    KeyBindScript.downKey = allKeys[1];
                    KeyBindScript.leftKey = allKeys[2];
                    KeyBindScript.attackKey = allKeys[4];
                    KeyBindScript.rangeKey = allKeys[5];
                    KeyBindScript.inventoryKey = allKeys[6];
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 4:
                {
                    KeyBindScript.attackKey = temp;
                    for (int i = 0; i < allKeys.Length; i++)
                    {
                        if (i != pointingAt)
                        {
                            if (allKeys[i] == temp)
                            {
                                Debug.Log("test");
                                allKeys[i] = KeyCode.None;
                            }
                        }
                    }

                    // we refresh the keys
                    KeyBindScript.upKey = allKeys[0];
                    KeyBindScript.downKey = allKeys[1];
                    KeyBindScript.leftKey = allKeys[2];
                    KeyBindScript.rightKey = allKeys[3];
                    KeyBindScript.rangeKey = allKeys[5];
                    KeyBindScript.inventoryKey = allKeys[6];
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 5:
                {
                    KeyBindScript.rangeKey = temp;
                    for (int i = 0; i < allKeys.Length; i++)
                    {
                        if (i != pointingAt)
                        {
                            if (allKeys[i] == temp)
                            {
                                Debug.Log("test");
                                allKeys[i] = KeyCode.None;
                            }
                        }
                    }

                    // we refresh the keys
                    KeyBindScript.upKey = allKeys[0];
                    KeyBindScript.downKey = allKeys[1];
                    KeyBindScript.leftKey = allKeys[2];
                    KeyBindScript.rightKey = allKeys[3];
                    KeyBindScript.attackKey = allKeys[4];
                    KeyBindScript.inventoryKey = allKeys[6];
                    temp = KeyCode.None;
                    helperText.SetActive(!helperText.activeSelf);
                    break;
                }
                case 6:
                {
                    KeyBindScript.inventoryKey = temp;
                    for (int i = 0; i < allKeys.Length; i++)
                    {
                        if (i != pointingAt)
                        {
                            if (allKeys[i] == temp)
                            {
                                Debug.Log("test");
                                allKeys[i] = KeyCode.None;
                            }
                        }
                    }

                    // we refresh the keys
                    KeyBindScript.upKey = allKeys[0];
                    KeyBindScript.downKey = allKeys[1];
                    KeyBindScript.leftKey = allKeys[2];
                    KeyBindScript.rightKey = allKeys[3];
                    KeyBindScript.attackKey = allKeys[4];
                    KeyBindScript.rangeKey = allKeys[5];
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
                if (soundEffects != null)
                {
                    soundEffects.playPositiveSound();

                }
                --pointingAt;
                GetComponent<RectTransform>().anchoredPosition = new Vector2(0, children[pointingAt].GetComponent<RectTransform>().anchoredPosition.y);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && pointingAt < (children.Length - 1))
            {
                if (soundEffects != null)
                {
                    soundEffects.playPositiveSound();

                }
                ++pointingAt;
                GetComponent<RectTransform>().anchoredPosition = new Vector2(0, children[pointingAt].GetComponent<RectTransform>().anchoredPosition.y);
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
