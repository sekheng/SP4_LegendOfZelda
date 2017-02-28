using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EscToDeactivate : MonoBehaviour {

    public GameObject[] anyoneToTakeNoteOf;
    private bool[] shouldCarryOn;
    private bool okay;
    private SoundEffectsManager soundEffects;
    // Update is called once per frame

    void Start()
    {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
        shouldCarryOn = new bool[anyoneToTakeNoteOf.Length];
    }

	void Update () {
        if (soundEffects == null)
        {
            soundEffects = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundEffectsManager>();

        }
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
                    if (soundEffects != null)
                    {
                        soundEffects.playNegativeSound();

                    }
                    MySQLiteHandler.instance.saveSpecificResult("GlobalSettingTable", "MusicGlobalVolume", LocalDataSingleton.instance.Volume.ToString());
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (soundEffects != null)
                {
                    soundEffects.playNegativeSound();

                }
                MySQLiteHandler.instance.saveSpecificResult("GlobalSettingTable", "MusicGlobalVolume", LocalDataSingleton.instance.Volume.ToString());
                gameObject.SetActive(false);
            }
        }
    }
}
