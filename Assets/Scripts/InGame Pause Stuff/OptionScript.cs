using UnityEngine;
using System.Collections;

/// <summary>
/// For option at ingame pause menu
/// </summary>
public class OptionScript : PauseButtonScript {

    public override bool executeButton()
    {
        //Debug.Log("OPTIONS0");
        LocalDataSingleton.instance.OptionsCanvas.SetActive(true);
        return true;
    }
}
