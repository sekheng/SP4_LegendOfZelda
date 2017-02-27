using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderControl : MonoBehaviour {


    private void Start()
    {
        GetComponent<Slider>().value = LocalDataSingleton.instance.Volume;
    }
    // Update is called once per frame
#if UNITY_STANDALONE
    void Update () {
	    if(Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Slider>().value += Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Slider>().value -= Time.deltaTime;
        }
	}
#endif
}
