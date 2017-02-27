using UnityEngine;
using System.Collections;

/// <summary>
/// This will help to close the Save UI only!
/// </summary>
public class CloseSaveUIScript : PauseButtonScript {
    public override bool executeButton()
    {
        Debug.Log("Close save UI");
        return true;
    }
}
