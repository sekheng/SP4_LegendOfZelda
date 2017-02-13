using UnityEngine;
using System.Collections;

/// <summary>
/// Used for keyboard and android mobile controller!
/// Too lazy to create another script for android mobile
/// Will add for mobile soon!
/// </summary>
public class PlayerController : MonoBehaviour {
#if UNITY_STANDALONE
    //private int sumOfHorizontal = 0;
    //private int sumOfVertical = 0;

    //private int up, down, left, right;

    // There will only be 1 hero throughout the entire game!
    private HeroesMovement theOnlyHero;
    // This is to check if the player is not moving the character, then stop the movement!
    private bool checkPlayerMoved = false;

	// Use this for initialization
	void Start () {
        //up = down = left = right = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(theOnlyHero == null)
        {
            theOnlyHero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroesMovement>();
        }
        //up = Input.GetKey("w") ? 1 : 0;
        //down = Input.GetKey("s") ? -1 : 0;
        //left = Input.GetKey("a") ? -1 : 0;
        //right = Input.GetKey("d") ? 1 : 0;

        //sumOfHorizontal = left + right;
        //sumOfVertical = up + down;

        //transform.position += new Vector3(sumOfHorizontal * 5, sumOfVertical * 5, 0) * Time.deltaTime;
    
        if (Input.GetKey(KeyCode.UpArrow))
        {
            theOnlyHero.moveDirection(new Vector2(0, 1));
            checkPlayerMoved = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            theOnlyHero.moveDirection(new Vector2(0, -1));
            checkPlayerMoved = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            theOnlyHero.moveDirection(new Vector2(-1, 0));
            checkPlayerMoved = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            theOnlyHero.moveDirection(new Vector2(1, 0));
            checkPlayerMoved = true;
        }
    }

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
                break;
        }
    }
#endif
}
