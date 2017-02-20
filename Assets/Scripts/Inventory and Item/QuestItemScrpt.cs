using UnityEngine;
using System.Collections;

/// <summary>
/// Hardcoded for now, as there is no time to do other stuff!
/// So there will be 5 relics for now!
/// </summary>
public class QuestItemScrpt : MonoBehaviour {
    [Tooltip("The number of relics")]
    public int m_numberOfRelics = 5;

    private BitArray checkForRelicBool;

	// Use this for initialization
	void Start () {
        checkForRelicBool = new BitArray(m_numberOfRelics);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
