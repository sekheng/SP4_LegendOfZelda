using UnityEngine;

public class BackToMain : MonoBehaviour {

    public float TimeToWaitFor = 2.0f;
	// Update is called once per frame
	void Update () {
	    if(TimeToWaitFor > 0.0f)
        {
            TimeToWaitFor -= Time.deltaTime;
        }
        else
        {
            LocalDataSingleton.instance.ReturntoMain();
        }
	}
}
