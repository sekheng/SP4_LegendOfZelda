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
    // To display the item image UI!
    private Image theItemImage;
    // To display the text name!
    private Text theItemText;
    //To know how many slots are there!
    private RecogniseItemUIScript toKnowTheSlots;
    // To know which slot is in
    private short m_slotAt = 0;

	// Use this for initialization
	void Start () {
        theItemImage = GameObject.FindGameObjectWithTag(m_tagnameOfDisplayImage).GetComponent<Image>();
        theItemText = GameObject.FindGameObjectWithTag(m_tagnameOfDescriptionText).GetComponent<Text>();
	}
}
