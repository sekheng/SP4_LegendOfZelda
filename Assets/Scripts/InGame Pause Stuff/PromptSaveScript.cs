using UnityEngine;
using System.Collections;

/// <summary>
/// Will prompt the player to go to save
/// </summary>
public class PromptSaveScript : PauseButtonScript {
    //[Tooltip("The Save option canvas tagname")]
    //public string m_saveCanvasTagName = ""
    private GameObject m_SaveCanvas;
    //  We will need to keep track of which save prompt canvas is it
    private GameObject m_SavePromptAlert;

    void Start()
    {
        m_SavePromptAlert = GameObject.FindGameObjectWithTag("SavePromptCanvas");
    }

    public override bool executeButton()
    {
        Debug.Log("Trying go save button");
        m_SaveCanvas.SetActive(true);
        m_SavePromptAlert.SetActive(false);
        return true;
    }
}
