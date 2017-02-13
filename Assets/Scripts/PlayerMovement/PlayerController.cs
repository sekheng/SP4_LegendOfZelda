using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Used for keyboard and android mobile controller!
/// Too lazy to create another script for android mobile
/// Will add for mobile soon!
/// </summary>
public class PlayerController : MonoBehaviour {


    //private int sumOfHorizontal = 0;
    //private int sumOfVertical = 0;

    //private int up, down, left, right;

    // There will only be 1 hero throughout the entire game!
    private HeroesMovement theOnlyHero;
    // This is to check if the player is not moving the character, then stop the movement!
    private bool checkPlayerMoved = false;
    // This is to check for the movement direction!
    private Vector2 directionMovement;

#if UNITY_ANDROID
    // the joystick
    private Image ImgFG;
    // joystick bg
    private Image ImgBG;
    private Vector3 directionOfStick;
#endif

	// Use this for initialization
	void Start () {
        //up = down = left = right = 0;
        theOnlyHero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroesMovement>();

        GameObject[] touchpadInput = GameObject.FindGameObjectsWithTag("Android Input");
        foreach (GameObject zeTouchButton in touchpadInput)
        {
#if UNITY_ANDROID
            // The unfortunate hardcoding stuff
            if (zeTouchButton.name.Equals("joystick_bg"))
            {
                ImgBG = zeTouchButton.GetComponent<Image>();
            }
            else if (zeTouchButton.name.Equals("joystick"))
            {
                ImgFG = zeTouchButton.GetComponent<Image>();
            }
#else
        // If it is not the touchpad!
        // Remove all touchpad!
        zeTouchButton.SetActive(false);
#endif
        }
	}
	
	// Update is called once per frame
	void Update () {

        //up = Input.GetKey("w") ? 1 : 0;
        //down = Input.GetKey("s") ? -1 : 0;
        //left = Input.GetKey("a") ? -1 : 0;
        //right = Input.GetKey("d") ? 1 : 0;

        //sumOfHorizontal = left + right;
        //sumOfVertical = up + down;

        //transform.position += new Vector3(sumOfHorizontal * 5, sumOfVertical * 5, 0) * Time.deltaTime;

#if UNITY_ANDROID

#else
        // This is so that movement can be disrupted by other arrow keys
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (directionMovement.y != 1)
            {
                directionMovement = new Vector2(0, 1);
                theOnlyHero.moveDirection(directionMovement);
            }
            checkPlayerMoved = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (directionMovement.y != -1)
            {
                directionMovement = new Vector2(0,-1);
                theOnlyHero.moveDirection(directionMovement);
            }
            checkPlayerMoved = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (directionMovement.x != -1)
            {
                directionMovement = new Vector2(-1, 0);
                theOnlyHero.moveDirection(directionMovement);
            }
            checkPlayerMoved = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (directionMovement.x != 1)
            {
                directionMovement = new Vector2(1, 0);
                theOnlyHero.moveDirection(directionMovement);
            }
            checkPlayerMoved = true;
        }
#endif
    }

#if UNITY_ANDROID
#else
    void LateUpdate()
    {
        switch (checkPlayerMoved)
        {
            case false:
                // If player hasn't move, stop the hero's movement!
                theOnlyHero.stopMovement();
                break;
            case true:
                // Then initialized it to be false!
                checkPlayerMoved = false;
                //Debug.Log("Reinitializing");
                break;
        }
    }
#endif
}
