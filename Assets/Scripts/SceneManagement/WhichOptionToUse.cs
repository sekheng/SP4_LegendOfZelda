using UnityEngine;
using System.Collections;

public class WhichOptionToUse : MonoBehaviour {

    public GameObject Windows_opt, Android_opt;

	public void choose()
    {
#if UNITY_STANDALONE
        Windows_opt.SetActive(true);
#else
        Android_opt.SetActive(true);
#endif
    }
}
