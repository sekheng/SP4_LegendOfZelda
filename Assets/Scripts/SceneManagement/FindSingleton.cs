using UnityEngine;

public class FindSingleton : MonoBehaviour {

    public void ReturnToLastFailed()
    {
        LocalDataSingleton.instance.ReturntoPrevious();
    }

    public void PlayPosSound()
    {
        LocalDataSingleton.instance.GetComponentInChildren<SoundEffectsManager>().playPositiveSound();
    }

    public void PlayNegSound()
    {
        LocalDataSingleton.instance.GetComponentInChildren<SoundEffectsManager>().playNegativeSound();
    }

    public void GoScene()
    {
       LocalDataSingleton.instance.GoNext();
    }

    public void OpenOptions()
    {
        LocalDataSingleton.instance.OptionsCanvas.SetActive(true);
    }

    public void Quit()
    {
        LocalDataSingleton.instance.QuitApp();
    }
}
