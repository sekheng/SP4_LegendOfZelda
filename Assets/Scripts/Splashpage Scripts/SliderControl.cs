using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderControl : MonoBehaviour {

	// Update is called once per frame
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
}
