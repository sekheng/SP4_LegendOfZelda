using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// To handle the drag movement of the player from Android mobile
/// </summary>
public class PlayerDrag : MonoBehaviour {
    [Tooltip("The Joystick background")]
    public Image ImgFG;
    // joystick bg
    [Tooltip("Actual joystick")]
    public Image ImgBG;
#if UNITY_ANDROID
    private Vector3 directionOfStick;

    // There will only be 1 hero throughout the entire game!
    private HeroesMovement theOnlyHero;

    [Tooltip("The offset between the center position of background and the joystick so that the hero can start moving")]
    public float offsetDistance = 10.0f;
    // The finger that pressed the joystick!
    private int theFingerTouchedID;
    // Check for any finger that has pressed the joystick!
    private bool fingerHasPressedIt = false;
    // Screen resolution!
    private int screenSizeX, screenSizeY;

    // Use this for initialization
	void Start () {
        //ImgFG = GetComponent<Image>();
        //ImgBG = transform.parent.GetComponent<Image>();
        //ImgBG = GetComponentInParent<Image>();
        //Debug.Log("Parent Name: " + ImgBG.gameObject.name);
        
        screenSizeX = Screen.width;
        screenSizeY = Screen.height;
        //Debug.Log("Screen height: " + screenSizeY);
    }
	
	// Update is called once per frame
	void Update () {
        if (theOnlyHero == null && ImgFG.IsActive() && ImgBG.IsActive())
        {
            theOnlyHero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroesMovement>();
        }
	    if (fingerHasPressedIt)
        {
           Touch theFingerTouched = Input.GetTouch(theFingerTouchedID);
           Vector3 ze3DTouch = new Vector3(theFingerTouched.position.x, theFingerTouched.position.y, ImgBG.rectTransform.position.z);
           directionOfStick = ze3DTouch - ImgBG.rectTransform.position;
           if (directionOfStick.magnitude > Mathf.Abs(ImgBG.rectTransform.sizeDelta.y * 0.5f))
           {
               directionOfStick.Normalize();
               directionOfStick *= Mathf.Abs(ImgBG.rectTransform.sizeDelta.y * 0.5f);
           }
           ImgFG.rectTransform.anchoredPosition = directionOfStick;
           // Since heroes can only move in 4 direction, then we should only do just that!
           // 1st, we will need to check whether it has gone more than a certain threshold!, otherwise stop movement!
           if (directionOfStick.sqrMagnitude < offsetDistance)
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

           //directionOfStick = Input.mousePosition - ImgBG.rectTransform.position;
           //if (directionOfStick.magnitude > Mathf.Abs(ImgBG.rectTransform.sizeDelta.y * 0.5f))
           //{
           //    directionOfStick.Normalize();
           //    directionOfStick *= Mathf.Abs(ImgBG.rectTransform.sizeDelta.y * 0.5f);
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
        //else if (Input.touchCount > 0)
        //{
        //    Touch[] allZeTouch = Input.touches;
        //    // Check whether any of the finger is at the bottom of the screen!
        //    foreach (Touch zeTouch in allZeTouch)
        //    {
        //        // Checking whether the finger pressed in at the bottom right of the screen and the finger landed on it!
        //        if (zeTouch.position.x < screenSizeX * 0.5f && zeTouch.position.y < screenSizeY * 0.5f && zeTouch.phase == TouchPhase.Began)
        //        {
        //            theFingerTouchedID = zeTouch.fingerId;
        //            ImgFG.gameObject.SetActive(true);
        //            ImgBG.gameObject.SetActive(true);
        //            ImgBG.rectTransform.position = new Vector3(zeTouch.position.x, zeTouch.position.y, ImgBG.rectTransform.position.z);
        //            fingerHasPressedIt = true;
        //            break;
        //        }
        //    }
        //}
        //else
        //{
        //    Debug.Log("Mouse Position: " + Input.mousePosition);
        //    if (Input.mousePosition.x < screenSizeX * 0.25f && Input.mousePosition.y < screenSizeY * 0.25f)
        //    {
        //        fingerHasPressedIt = true;
        //        ImgFG.gameObject.SetActive(true);
        //        ImgBG.gameObject.SetActive(true);
        //        ImgBG.rectTransform.position = Input.mousePosition;
        //        //Debug.Log("Touching bottom right of screen");
        //    }
        //}
	}

    //public void Dragging()
    //{
    //    //Touch[] allZeTouch = Input.touches;
    //    //foreach (Touch zeTouch in allZeTouch)
    //    //{
    //    //    if (zeTouch.position.x < (ImgBG.rectTransform.position.x + ImgBG.rectTransform.sizeDelta.x * 0.5f) && zeTouch.position.x > (ImgBG.rectTransform.position.x - ImgBG.rectTransform.sizeDelta.x * 0.5f)
    //    //        && zeTouch.position.y < (ImgBG.rectTransform.position.y + ImgBG.rectTransform.sizeDelta.y * 0.5f) && zeTouch.position.y > (ImgBG.rectTransform.position.y - (ImgBG.rectTransform.sizeDelta.y * 0.5f)))
    //    //    {
    //    //        // Need to check whether the point is in the square!
    //    //        Vector3 ze3DTouch = new Vector3(zeTouch.position.x, zeTouch.position.y, ImgBG.rectTransform.position.z);
    //    //        directionOfStick = ze3DTouch - ImgBG.rectTransform.position;
    //    //        if (directionOfStick.magnitude > ImgBG.rectTransform.sizeDelta.x * 0.5f)
    //    //        {
    //    //            directionOfStick.Normalize();
    //    //            directionOfStick *= ImgBG.rectTransform.sizeDelta.x * 0.5f;
    //    //        }
    //    //        ImgFG.rectTransform.anchoredPosition = directionOfStick;
    //    //        // Since heroes can only move in 4 direction, then we should only do just that!
    //    //        // 1st, we will need to check whether it has gone more than a certain threshold!, otherwise stop movement!
    //    //        if (directionOfStick.sqrMagnitude < offsetDistance * offsetDistance)
    //    //        {
    //    //            theOnlyHero.stopMovement();
    //    //            return; // Otherwise the hero will still be moving!
    //    //        }
    //    //        if (Mathf.Abs(directionOfStick.x) > Mathf.Abs(directionOfStick.y))
    //    //        {
    //    //            directionOfStick = new Vector3(directionOfStick.x, 0);
    //    //        }
    //    //        else
    //    //        {
    //    //            directionOfStick = new Vector3(0, directionOfStick.y);
    //    //        }
    //    //        theOnlyHero.moveDirection(directionOfStick);
    //    //        break;
    //    //    }
    //    //}

    //    //Debug.Log("Dragging Position");
    //    // Need to check whether the point is in the square!
    //    //directionOfStick = Input.mousePosition - ImgBG.rectTransform.position;
    //    //if (directionOfStick.magnitude > ImgBG.rectTransform.sizeDelta.x * 0.5f)
    //    //{
    //    //    directionOfStick.Normalize();
    //    //    directionOfStick *= ImgBG.rectTransform.sizeDelta.x * 0.5f;
    //    //}
    //    //ImgFG.rectTransform.anchoredPosition = directionOfStick;
    //    ////Debug.Log("Direction: " + directionOfStick);
    //    //// Since heroes can only move in 4 direction, then we should only do just that!
    //    //// 1st, we will need to check whether it has gone more than a certain threshold!, otherwise stop movement!
    //    //if (directionOfStick.sqrMagnitude < offsetDistance * offsetDistance)
    //    //{
    //    //    theOnlyHero.stopMovement();
    //    //    return;
    //    //}
    //    //if (Mathf.Abs(directionOfStick.x) > Mathf.Abs(directionOfStick.y))
    //    //{
    //    //    directionOfStick = new Vector3(directionOfStick.x, 0);
    //    //}
    //    //else
    //    //{
    //    //    directionOfStick = new Vector3(0, directionOfStick.y);
    //    //}
    //    //theOnlyHero.moveDirection(directionOfStick);
    //}
    public void ReturnOrigin()
    {
        ImgFG.rectTransform.anchoredPosition = new Vector3(0, 0, 1);
        directionOfStick = new Vector3(0, 0, 1);
        theOnlyHero.stopMovement();
        fingerHasPressedIt = false;
        //ImgBG.gameObject.SetActive(false);
        //ImgFG.gameObject.SetActive(false);
    }
    public void FingerPressed()
    {
        Touch[] allZeTouch = Input.touches;
        foreach (Touch zeTouch in allZeTouch)
        {
            if (zeTouch.position.x < (ImgBG.rectTransform.position.x + Mathf.Abs(ImgBG.rectTransform.sizeDelta.x * 0.5f)) && zeTouch.position.x > (ImgBG.rectTransform.position.x - Mathf.Abs(ImgBG.rectTransform.sizeDelta.x * 0.5f))
                && zeTouch.position.y < (ImgBG.rectTransform.position.y + Mathf.Abs(ImgBG.rectTransform.sizeDelta.y * 0.5f)) && zeTouch.position.y > (ImgBG.rectTransform.position.y - Mathf.Abs(ImgBG.rectTransform.sizeDelta.y * 0.5f)))
            {
                theFingerTouchedID = zeTouch.fingerId;
                break;
            }
        }
        //Debug.Log("Pressing the button");
        fingerHasPressedIt = true;
    }
#else
    void Start()
    {
        //transform.parent.gameObject.SetActive(false);
        //gameObject.SetActive(false);
        ImgFG.gameObject.SetActive(false);
        ImgBG.gameObject.SetActive(false);
    }
#endif
}
