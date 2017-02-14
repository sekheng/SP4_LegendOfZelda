using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// To handle the drag movement of the player from Android mobile
/// </summary>
public class PlayerDrag : MonoBehaviour {
#if UNITY_ANDROID
    private Image ImgFG;
    // joystick bg
    private Image ImgBG;
    private Vector3 directionOfStick;

    // There will only be 1 hero throughout the entire game!
    private HeroesMovement theOnlyHero;

    [Tooltip("The offset between the center position of background and the joystick so that the hero can start moving")]
    public float offsetDistance = 10.0f;
    // The finger that pressed the joystick!
    private Touch theFingerTouched;

    // Use this for initialization
	void Start () {
        ImgFG = GetComponent<Image>();
        ImgBG = transform.parent.GetComponent<Image>();
        //ImgBG = GetComponentInParent<Image>();
        //Debug.Log("Parent Name: " + ImgBG.gameObject.name);
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
                && zeTouch.position.y < (ImgBG.rectTransform.position.y + ImgBG.rectTransform.sizeDelta.y * 0.5f) && zeTouch.position.y > (ImgBG.rectTransform.position.y - (ImgBG.rectTransform.sizeDelta.y * 0.5f)))
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
                // Since heroes can only move in 4 direction, then we should only do just that!
                // 1st, we will need to check whether it has gone more than a certain threshold!, otherwise stop movement!
                if (directionOfStick.sqrMagnitude < offsetDistance * offsetDistance)
                {
                    theOnlyHero.stopMovement();
                    return; // Otherwise the hero will still be moving!
                }
                if (Mathf.Abs(directionOfStick.x) > Mathf.Abs(directionOfStick.y))
                {
                    directionOfStick = new Vector3(directionOfStick.x, 0);
                }
                else
                {
                    directionOfStick = new Vector3(0, directionOfStick.y);
                }
                theOnlyHero.moveDirection(directionOfStick);
                break;
            }
        }

        //Debug.Log("Dragging Position");
        // Need to check whether the point is in the square!
        //directionOfStick = Input.mousePosition - ImgBG.rectTransform.position;
        //if (directionOfStick.magnitude > ImgBG.rectTransform.sizeDelta.x * 0.5f)
        //{
        //    directionOfStick.Normalize();
        //    directionOfStick *= ImgBG.rectTransform.sizeDelta.x * 0.5f;
        //}
        //ImgFG.rectTransform.anchoredPosition = directionOfStick;
        ////Debug.Log("Direction: " + directionOfStick);
        //// Since heroes can only move in 4 direction, then we should only do just that!
        //// 1st, we will need to check whether it has gone more than a certain threshold!, otherwise stop movement!
        //if (directionOfStick.sqrMagnitude < offsetDistance * offsetDistance)
        //{
        //    theOnlyHero.stopMovement();
        //    return;
        //}
        //if (Mathf.Abs(directionOfStick.x) > Mathf.Abs(directionOfStick.y))
        //{
        //    directionOfStick = new Vector3(directionOfStick.x, 0);
        //}
        //else
        //{
        //    directionOfStick = new Vector3(0, directionOfStick.y);
        //}
        //theOnlyHero.moveDirection(directionOfStick);
    }
    public void ReturnOrigin()
    {
        ImgFG.rectTransform.anchoredPosition = new Vector3(0, 0, 1);
        directionOfStick = new Vector3(0, 0, 1);
        theOnlyHero.stopMovement();
    }   
#else
    void Start()
    {
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
#endif
}
