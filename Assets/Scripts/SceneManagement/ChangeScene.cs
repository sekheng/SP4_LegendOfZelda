using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) //mostly is just for compass
    {
        if(other.CompareTag("Player"))
        {
            ChangeSceneTo(SceneManager.GetActiveScene().buildIndex + 1);    
        }
    }

    public void GotoNext()
    {
        ChangeSceneTo(SceneManager.GetActiveScene().buildIndex + 1);
    }

	public void ChangeSceneTo(int index)
    {
        LocalDataSingleton.instance.previousSceneFrom = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
}
