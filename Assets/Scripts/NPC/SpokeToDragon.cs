using UnityEngine;
using System.Collections;

public class SpokeToDragon : MonoBehaviour {

    private Animator myAnim;

    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
	    if(LocalDataSingleton.instance.talkedToDragon)
        {
            myAnim.Play("DragonSleeping");
        }
	}
}
