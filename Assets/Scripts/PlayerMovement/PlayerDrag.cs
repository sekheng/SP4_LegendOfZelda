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
    private Vector3 directionOfStick = new Vector3(1, 0, 0);

    // There will only be 1 hero throughout the entire game!
    private HeroesMovement theOnlyHero;

    [Tooltip("The offset between the center position of background and the joystick so that the hero can start moving")]
    public float offsetDistance = 10.0f;
    // The finger that pressed the joystick!
    private int theFingerTouchedID;
    // Check for any finger that has pressed the joystick!
    [HideInInspector]
    public bool fingerHasPressedIt = false;
    // Screen resolution!
    private int screenSizeX, screenSizeY;
    // When player pressed the button, hero's movement has to stop unless they stop dragging the joystick or move it to other directions
    private bool playerPressedButton = false;
    private short movedInXDirection = 0, movedInYDirection = 0;
    // To know the direction of where is going to be 
    [HideInInspector]
    public short movingInYDirection = 0, movingInXDirection = 0;
    // This will need to be used to check against movingInYDirection!
    private short directionOfPrevY = 0, directionOfCurrY = 0, directionOfPrevX = 0, directionOfCurrX = 0;

    // Used to debug Android inputs. Will remove soon!
    //private TextMesh debugginMesh;

    // Use this for initialization
	void Start () {
        //ImgFG = GetComponent<Image>();
        //ImgBG = transform.parent.GetComponent<Image>();
        //ImgBG = GetComponentInParent<Image>();
        //Debug.Log("Parent Name: " + ImgBG.gameObject.name);
        
        screenSizeX = Screen.width;
        screenSizeY = Screen.height;
        //Debug.Log("Screen height: " + screenSizeY);
        //Debug.Log("ImgBG sizeDelta Y: " + ImgBG.rectTransform.sizeDelta.y);
    }
	
	// Update is called once per frame
	void Update () {
        //if (debugginMesh == null)
        //    debugginMesh = GameObject.Find("Remove TODO").GetComponent<TextMesh>(); ;

        if (theOnlyHero == null && ImgFG.IsActive() && ImgBG.IsActive())
        {
            theOnlyHero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroesMovement>();
        }
	    if (fingerHasPressedIt)
        {
            Touch theFingerTouched = Input.GetTouch(theFingerTouchedID);

            switch (theFingerTouched.phase)
            {
                case TouchPhase.Canceled:
                    ReturnOrigin();
                    return;
                case TouchPhase.Ended:
                    ReturnOrigin();
                    return;
                default:
                    break;
            }

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
            if (directionOfStick.magnitude < ImgBG.rectTransform.sizeDelta.y * 0.25f)
            {
                theOnlyHero.stopMovement();
                movedInXDirection = 0;
                movedInYDirection = 0;
                playerPressedButton = false;
                movedInXDirection = directionOfCurrX = directionOfPrevX = directionOfPrevY = directionOfCurrY = movingInYDirection = 0;
                //debugginMesh.text = "Restart playerPressedButton";
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

            if (directionOfPrevY != directionOfCurrY)
            {
                directionOfPrevY = directionOfCurrY;
            }
            // Below the if statement then it will be recognized in this frame!
            directionOfCurrY = (short)(Mathf.Clamp(directionOfStick.y, -1.0f, 1.0f));
            movingInYDirection = (short)(directionOfCurrY + directionOfPrevY);
            if (directionOfPrevX != directionOfCurrX)
            {
                directionOfPrevX = directionOfCurrX;
            }
            // Below the if statement then it will be recognized in this frame!
            directionOfCurrX = (short)(Mathf.Clamp(directionOfStick.x, -1.0f, 1.0f));
            movingInXDirection = (short)(directionOfCurrX + directionOfPrevX);
            
            // If the player is talking, don't move at all!
            if (LocalDataSingleton.instance.talking)
                return;
            if (!playerPressedButton)
                theOnlyHero.moveDirection(directionOfStick);
            else
            {
                // If player pressed a button and then move the hero in other directions, all will be forgiven.
                if (!(directionOfStick.normalized.x == movedInXDirection && directionOfStick.normalized.y == movedInYDirection))
                {
                    playerPressedButton = false;
                    //debugginMesh.text = "Player moved in other directions: " + movedInXDirection + ", " + movedInYDirection;
                }
                //else
                //{
                //    debugginMesh.text = "Player not moving other directions";
                //}
            }

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
        if (theOnlyHero == null)
            return;
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

    public void playerHasPressedButton()
    {
        playerPressedButton = true;
        movedInXDirection = (short)Mathf.Ceil(directionOfStick.normalized.x);
        movedInYDirection = (short)Mathf.Ceil(directionOfStick.normalized.y);
        Debug.Log("Player Pressed a button: " + movedInXDirection + ", : " + movedInYDirection);
        //debugginMesh.text = "Player Pressed a button: " + movedInXDirection + ", " + movedInYDirection;
    }
#else
    void Start()
    {
        //transform.parent.gameObject.SetActive(false);
        //gameObject.SetActive(false);
        ImgFG.gameObject.SetActive(false);
        ImgBG.gameObject.SetActive(false);
        enabled = false;
    }
#endif
}
