using UnityEngine;
using System.Collections;

public class ToggleActive : MonoBehaviour {

	public void Toggle(GameObject toggleObj)
    {
        toggleObj.SetActive(!toggleObj.activeSelf);
    }
}
