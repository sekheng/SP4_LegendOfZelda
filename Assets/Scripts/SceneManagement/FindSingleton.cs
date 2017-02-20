using UnityEngine;
using System.Collections;

public class FindSingleton : MonoBehaviour {

    public void GoScene()
    {
       LocalDataSingleton.instance.GoNext();
    }

    public void OpenOptions()
    {
        LocalDataSingleton.instance.OptionsCanvas.SetActive(true);
    }
}
