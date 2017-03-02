using UnityEngine;
using System.Collections;

public class layerChanger : MonoBehaviour {
    protected GameObject player;
    protected SpriteRenderer spriteR;
    protected SpriteRenderer playerSpriteR;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerSpriteR = player.transform.FindChild("Actual Sprite").GetComponent<SpriteRenderer>();
        }
        spriteR = gameObject.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	public virtual void Update () {
	    if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerSpriteR = player.transform.FindChild("Actual Sprite").GetComponent<SpriteRenderer>();
            }
        }
        else
        {
            if(player.transform.position.y > gameObject.transform.position.y)
            {
                spriteR.sortingOrder = playerSpriteR.sortingOrder + 1;
            }
            else
            {
                spriteR.sortingOrder = playerSpriteR.sortingOrder - 1;
            }
        }
    }

}
