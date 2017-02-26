using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EscToDeactivate : MonoBehaviour {

    public GameObject[] anyoneToTakeNoteOf;
    private bool[] shouldCarryOn;
    private bool okay;
	// Update is called once per frame

    void Start()
    {
        shouldCarryOn = new bool[anyoneToTakeNoteOf.Length];
    }

	void Update () {
        okay = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (anyoneToTakeNoteOf.Length > 0)
            {
                for(int i = 0; i < anyoneToTakeNoteOf.Length; i++)
                {
                    if(!anyoneToTakeNoteOf[i].activeSelf)
                    {
                        shouldCarryOn[i] = true;
                    }
                    else
                    {
                        shouldCarryOn[i] = false;
                    }
                }
                for(int i = 0; i < shouldCarryOn.Length; i++)
                {
                    if(!shouldCarryOn[i])
                    {
                        okay = false;
                    }
                }
                if(okay)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
