using UnityEngine;

public class NextSceneIn2s : MonoBehaviour {

    public float TimeToWaitFor = 2.0f;
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyBindScript.attackKey))
        {
            //skip
            TimeToWaitFor = 0.0f;
        }
	    if(TimeToWaitFor > 0.0f)
        {
            TimeToWaitFor -=  Time.deltaTime;
        }
        else
        {
            LocalDataSingleton.instance.GoNext();
        }
	}
}
