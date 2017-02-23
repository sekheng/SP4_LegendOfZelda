using UnityEngine;

public class ToggleActive : MonoBehaviour {

	public void Toggle(GameObject toggleObj)
    {
        toggleObj.SetActive(!toggleObj.activeSelf);
    }
}
