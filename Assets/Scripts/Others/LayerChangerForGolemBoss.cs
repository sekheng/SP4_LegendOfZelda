using UnityEngine;
using System.Collections;

public class LayerChangerForGolemBoss : layerChanger
{
	
	// Update is called once per frame
	public override void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerSpriteR = player.transform.FindChild("Actual Sprite").GetComponent<SpriteRenderer>();
            }
        }
        else
        {
            if (player.transform.position.y > gameObject.transform.position.y - spriteR.bounds.size.y/4)
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
