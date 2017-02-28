using UnityEngine;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public GameObject[] stuff;
    public GameObject PlayCanvas, OptionCanvas;
    private int lookAt;

#if UNITY_STANDALONE
    // Use this for initialization
    void Start()
    {
        lookAt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(OptionCanvas == null)
        {
            OptionCanvas = LocalDataSingleton.instance.transform.GetChild(0).gameObject;
        }

        if (!PlayCanvas.activeSelf && !OptionCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && lookAt > 0)
            {
                --lookAt;
                GetComponent<RectTransform>().anchoredPosition = stuff[lookAt].GetComponent<RectTransform>().anchoredPosition;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && lookAt < (stuff.Length - 1))
            {
                ++lookAt;
                GetComponent<RectTransform>().anchoredPosition = stuff[lookAt].GetComponent<RectTransform>().anchoredPosition;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (lookAt)
                {
                    case 0:
                    {
                        //play
                        PlayCanvas.SetActive(!PlayCanvas.activeSelf);
                        break;
                    }
                    case 1:
                    {
                        //some other things
                        OptionCanvas.SetActive(!OptionCanvas.activeSelf);
                        break;
                    }
                    case 2:
                    {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                        break;
                    }
                }
            }
        }
    }
#endif
}
