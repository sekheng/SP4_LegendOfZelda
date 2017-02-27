using UnityEngine;
using System.Collections;

/// <summary>
/// This will be used to open a load data canvas then the player can choose which loads to save!
/// </summary>
public class SaveScript : PauseButtonScript {
    /// <summary>
    /// In this case, opens up the load data canvas!
    /// </summary>
    /// <returns>
    /// Returns true if this is a workable button
    /// </returns>
    public override bool executeButton()
    {
        //Debug.Log("SAVE");
        GameObject.Find("Save Canvas").SetActive(true);
        return true;
    }
}
