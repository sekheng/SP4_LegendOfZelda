using UnityEngine;
using System.Collections;

/// <summary>
/// This will based on what the player decide
/// 0 will be the new game while numbers between 1 and 3 will be the load data!
/// </summary>
public class LoadGameScript : MonoBehaviour {
    [Tooltip("What save data do you want to load")]
    public int m_loadNumber = 0;

    /// <summary>
    /// Can be used to load the data!
    /// </summary>
    public void beginLoadingData()
    {
        // If the designer decides to set it to be more than 3 or less than 0, we will put it to 0!
        if (m_loadNumber > 3 || m_loadNumber < 0)
            m_loadNumber = 0;
        switch (m_loadNumber)
        {
            case 0:
                break;
            default:
                HeroDataScript.m_playerID = m_loadNumber;
                break;
        }
    }
}
