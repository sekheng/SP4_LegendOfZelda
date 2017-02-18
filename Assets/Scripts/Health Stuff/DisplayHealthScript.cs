using UnityEngine;
using System.Collections;

/// <summary>
/// Display the health bar on top of the characters
/// As for the adjustment, unfortunately you have to do it by yourself
/// These codes are unoptimized. Will be optimizing soon!
/// </summary>
[RequireComponent(typeof(HealthScript))]
public class DisplayHealthScript : MonoBehaviour {
    // Used to keep track of character's health
    private HealthScript characterHealth;
    //private TextMesh theDebuggingText;
    // To Display the health bar of the character, We will need the gameobject's name to be DisplayHealth
    private SpriteRenderer healthBarUI;
    // The original scale X of the healthbar!
    private float originalScaleX;
    [Tooltip("The GameObject's name so as to display the healthbar")]
    public string m_gameObjectName = "DisplayHealth";

	// Use this for initialization
	void Start () {
        characterHealth = GetComponent<HealthScript>();
        //theDebuggingText = GetComponentInChildren<TextMesh>();
        SpriteRenderer[] zeSpritesRenderer = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer zeSprite in zeSpritesRenderer)
        {
            if (zeSprite.gameObject.name.Equals(m_gameObjectName))
            {
                healthBarUI = zeSprite;
                originalScaleX = zeSprite.transform.localScale.x;
                break;
            }
        }
        //Debug.Log(healthBarUI.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
        //theDebuggingText.text = characterHealth.m_health.ToString();
        float percentageOfOverallHealth = characterHealth.m_health / characterHealth.max_health;
        //Debug.Log("Percentage of health: " + percentageOfOverallHealth);
        if (percentageOfOverallHealth < 0.25f)
        {
            healthBarUI.color = new Color32(237, 7, 7, 255);
        }
        else if (percentageOfOverallHealth < 0.5f)
        {
            healthBarUI.color = new Color32(226, 145, 4, 255);
        }
        else
            healthBarUI.color = new Color32(30, 255, 0, 255);
        float resultingScaleX = originalScaleX * percentageOfOverallHealth;
        healthBarUI.transform.localScale = new Vector3(resultingScaleX, healthBarUI.transform.localScale.y, healthBarUI.transform.localScale.z);
	}
}
