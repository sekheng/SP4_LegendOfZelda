using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// This is to test out editor debugging stuff
/// </summary>
public class LOLMenu {

    [MenuItem("Very Important/Press This")]
    static public void LOL()
    {
        //GameObject go = (GameObject)Selection.activeGameObject;
        //if (go != null)
        //{
        //    go.transform.position = new Vector3(0, 0, 0);
        //    EditorUtility.SetDirty(go);
        //}
        Debug.Log("LOL");
    }

}
