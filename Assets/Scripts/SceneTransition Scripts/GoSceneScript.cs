using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// A simple script to go towards the next scene
/// </summary>
public class GoSceneScript : MonoBehaviour {

    [Tooltip("Going to next scene name")]
    public string sceneName = "";

    public void GoNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
