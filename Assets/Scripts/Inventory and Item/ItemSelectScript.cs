using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This will be used to check what is the selection border at. If so, display the item!
/// </summary>
public class ItemSelectScript : MonoBehaviour {
    [Tooltip("The tagname of the Display Image UI")]
    public string m_tagnameOfDisplayImage = "ItemDisplayUI";
    [Tooltip("tagname of the description text")]
    public string m_tagnameOfDescriptionText = "ItemDescriptionText";
    [Tooltip("tagname of the inventory scrollbar")]
    public string m_tagnameOfScrollbar = "ItemScrollbar";
    // To display the item image UI!
    private Image theItemImage;
    // To display the text name!
    private Text theItemText;
    //To know how many slots are there!
    private RecogniseItemUIScript toKnowTheSlots;
    // To know which slot is in
    private short m_slotAt = 0;
    // To get the position of the UI
    private RectTransform thePosition;
    // It will also need to be able to scroll and change the rectTransform position!
    private Scrollbar theInventoryScrollbar;

#if UNITY_ANDROID
    // We need to straight away and get the joystick before it is gone!
    [Tooltip("To keep track the player's joystic")]
    public PlayerDrag thePlayerDrag;
    //void Awake()
    //{
    //    thePlayerDrag = FindObjectOfType<PlayerDrag>();
    //}
#endif

	// Use this for initialization
	void Start() {
        theItemImage = GameObject.FindGameObjectWithTag(m_tagnameOfDisplayImage).GetComponent<Image>();
        theItemText = GameObject.FindGameObjectWithTag(m_tagnameOfDescriptionText).GetComponent<Text>();
        theInventoryScrollbar = GameObject.FindGameObjectWithTag(m_tagnameOfScrollbar).GetComponent<Scrollbar>();
        toKnowTheSlots = FindObjectOfType<RecogniseItemUIScript>();
        thePosition = GetComponent<RectTransform>();
        UpdateTheUI();
	}

    void Update()
    {
#if UNITY_STANDALONE
        // For keypress so that the select border can go left and right
        //  Will also need to make sure that the slot this select is at is more than 0
        if (Input.GetKeyDown(KeyCode.LeftArrow) && m_slotAt > 0)
        {
            --m_slotAt;
            UpdateTheUI();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && m_slotAt + 1 < toKnowTheSlots.allTheSlots.Count)
        {
            ++m_slotAt;
            UpdateTheUI();
        }
#else
        // Based on the android joystick drag, we shall determine where is it moving
        if (thePlayerDrag == null)
            thePlayerDrag = FindObjectOfType<PlayerDrag>();
        if (thePlayerDrag.movingInXDirection > 0)
        {
            ++m_slotAt;
            UpdateTheUI();
        }
        else if (thePlayerDrag.movingInXDirection < 0)
        {
            --m_slotAt;
            UpdateTheUI();
        }
#endif
    }

    void UpdateTheUI()
    {
        ItemScript zeItem = toKnowTheSlots.allTheSlots[m_slotAt].GetComponentInChildren<ItemScript>();
        // Check whether is there any item script in the slot!
        if (zeItem != null)
        {
            theItemImage.enabled = true;
            theItemText.text = zeItem.m_itemInform.item_effect;
            theItemImage.sprite = zeItem.GetComponentInChildren<Image>().sprite;
        }
        else
        {
            theItemText.text = "";
            //theItemImage.sprite = null;
            theItemImage.enabled = false;
        }
        thePosition.SetParent(toKnowTheSlots.allTheSlots[m_slotAt].GetComponent<RectTransform>(), false);
        // We will need to scroll the inventory!
        float zeValue = (float)(m_slotAt) / (float)toKnowTheSlots.allTheSlots.Count;
        theInventoryScrollbar.value = zeValue;
    }
}
