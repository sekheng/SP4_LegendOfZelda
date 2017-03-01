using UnityEngine;

public class NextSceneIn2s : MonoBehaviour {

    public float TimeToWaitFor = 2.0f;
    private bool Attacked = false;

	// Update is called once per frame

    void OnEnable()
    {
        MessageSystem.instance.setListener("MeleeAttack", PlayerPressedMelee);
    }

	void Update () {

#if UNITY_STANDALONE
        Attacked = Input.GetKeyDown(KeyBindScript.attackKey) ? true : Attacked;
#endif

        if(Attacked)
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

    public void PlayerPressedMelee()
    {
        Attacked = true;
    }

    void OnDisable()
    {
        MessageSystem.instance.removeListener("MeleeAttack", PlayerPressedMelee);
    }
}
