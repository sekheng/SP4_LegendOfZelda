using UnityEngine;
using System.Collections;

public class LocalDataSingleton : MonoBehaviour {

    public static LocalDataSingleton instance = null;

    public int previousSceneFrom = -1;
    public bool talkedToDragon = false;

    // Earlier than start
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
