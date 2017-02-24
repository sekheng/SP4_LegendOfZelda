using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Used to display specific health of a monster!
/// Just 1 monster btw!
/// The monster will also need a health script
/// For now, it will only be compatible with Image UI!
/// </summary>
[RequireComponent(typeof(Image))]
public class DisplaySpecificHealthScript : MonoBehaviour {
    [Tooltip("The monster's obj name!")]
    public string monster_name = "GolemBoss";
    [Tooltip("Monster's tagname")]
    public string monster_tag = "Untagged";

    public Image healthbar;
    public GameObject bossName;
  
    // To expand and contract the monster's health
    private Image healthUI;
    // To keep track of that monster's health!
    private HealthScript monsterHealth;

    private GolemBoss_Statemanager golemStateManager;
    private GolemBoss_AwakeState golemAwakeState;

	// Use this for initialization
	void Start () {
        healthUI = GetComponent<Image>();
        if (!monster_tag.Equals("Untagged"))
        {
            GameObject []allZeGo = GameObject.FindGameObjectsWithTag(monster_tag);
            foreach (GameObject zeGo in allZeGo)
            {
                if (zeGo.name.Equals(monster_name))
                {
                    monsterHealth = zeGo.GetComponent<HealthScript>();
                    golemStateManager = monsterHealth.GetComponentInChildren<GolemBoss_Statemanager>();
                    golemAwakeState = golemStateManager.gameObject.GetComponentInChildren<GolemBoss_AwakeState>();
                    break;
                }
            }
        }
        else
        {
            monsterHealth = GameObject.Find(monster_name).GetComponent<HealthScript>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (monsterHealth == null)
            Start();
        if (golemStateManager.currState == golemAwakeState)
        {
            if (!healthbar.enabled)
                healthbar.enabled = true;
            if (!bossName.activeSelf)
                bossName.SetActive(true);
        }
        if (healthbar.enabled && bossName.activeSelf)
        {
            //theDebuggingText.text = characterHealth.m_health.ToString();
            float percentageOfOverallHealth = monsterHealth.m_health / monsterHealth.max_health;
            //Debug.Log("Percentage of health: " + percentageOfOverallHealth);
            if (percentageOfOverallHealth < 0.25f)
            {
                healthUI.color = new Color32(237, 7, 7, (byte)(healthUI.color.a * 255));
            }
            else if (percentageOfOverallHealth < 0.5f)
            {
                healthUI.color = new Color32(226, 145, 4, (byte)(healthUI.color.a * 255));
            }
            else
                healthUI.color = new Color32(30, 255, 0, (byte)(healthUI.color.a * 255));
            //float resultingScaleX = originalScaleX * percentageOfOverallHealth;
            //healthBarUI.transform.localScale = new Vector3(resultingScaleX, healthBarUI.transform.localScale.y, healthBarUI.transform.localScale.z);
            healthUI.fillAmount = percentageOfOverallHealth;
        }
	}
}
