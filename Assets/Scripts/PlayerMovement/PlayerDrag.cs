using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// To handle the drag movement of the player from Android mobile
/// </summary>
public class PlayerDrag : MonoBehaviour {

    private Image ImgFG;
    // joystick bg
    private Image ImgBG;
    private Vector3 directionOfStick;

    // There will only be 1 hero throughout the entire game!
    private HeroesMovement theOnlyHero;

    // Use this for initialization
	void Start () {
        ImgFG = GetComponent<Image>();
        ImgBG = transform.parent.GetComponent<Image>();
        //ImgBG = GetComponentInParent<Image>();
        Debug.Log("Parent Name: " + ImgBG.gameObject.name);
        theOnlyHero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroesMovement>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Dragging()
    {
        Touch[] allZeTouch = Input.touches;
        foreach (Touch zeTouch in allZeTouch)
        {
            if (zeTouch.position.x < (ImgBG.rectTransform.position.x + ImgBG.rectTransform.sizeDelta.x * 0.5f) && zeTouch.position.x > (ImgBG.rectTransform.position.x - ImgBG.rectTransform.sizeDelta.x * 0.5f)
                && zeTouch.position.y < (ImgBG.rectTransform.position.y + ImgBG.rectTransform.sizeDelta.y * 0.5f) && zeTouch.position.y > (ImgBG.rectTransform.position.y - ImgBG.rectTransform.sizeDelta.y * 0.5f))
            {
                // Need to check whether the point is in the square!
                Vector3 ze3DTouch = new Vector3(zeTouch.position.x, zeTouch.position.y, ImgBG.rectTransform.position.z);
                directionOfStick = ze3DTouch - ImgBG.rectTransform.position;
                if (directionOfStick.magnitude > ImgBG.rectTransform.sizeDelta.x * 0.5f)
                {
                    directionOfStick.Normalize();
                    directionOfStick *= ImgBG.rectTransform.sizeDelta.x * 0.5f;
                }
                ImgFG.rectTransform.anchoredPosition = directionOfStick;
                theOnlyHero.moveDirection(directionOfStick);
                break;
            }
        }

        //Debug.Log("Dragging Position");
            //{
            //    // Need to check whether the point is in the square!
            //    directionOfStick = Input.mousePosition - ImgBG.rectTransform.position;
            //    if (directionOfStick.magnitude > ImgBG.rectTransform.sizeDelta.x * 0.5f)
            //    {
            //        directionOfStick.Normalize();
            //        directionOfStick *= ImgBG.rectTransform.sizeDelta.x * 0.5f;
            //    }
            //    ImgFG.rectTransform.anchoredPosition = directionOfStick;
            //    Debug.Log("Direction: " + directionOfStick);
            //    theOnlyHero.moveDirection(directionOfStick);
            //}
    }
    public void ReturnOrigin()
    {
        ImgFG.rectTransform.anchoredPosition = new Vector3(0, 0, 1);
        directionOfStick = new Vector3(0, 0, 1);
        theOnlyHero.stopMovement();
    }   
    //public void OnDrag(PointerEventData pointer)
    //{
    //    Debug.Log("Draggin");
    //    // We get the direction from Img BG to Img FG
    //    ImgFG.rectTransform.anchoredPosition = pointer.position;
    //    theOnlyHero.moveDirection(ImgFG.rectTransform.anchoredPosition);
    //}
    //public void OnEndDrag(PointerEventData pointer)
    //{
    //    ReturnOrigin();
    //    theOnlyHero.stopMovement();
    //}
}
