using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFall : MonoBehaviour
{
    bool isFalling = false;

    public float fallSpeed = 1;

    public List<enemy> enemies = new List<enemy>();

    public Player player;
   
    private void FixedUpdate()
    {
        if (isFalling)
        {
            Vector2 pos = transform.position;
            float fallAmount = fallSpeed * Time.fixedDeltaTime;
            pos.y -= fallAmount;

            if(player != null)
            {
                player.groundHeight -= fallAmount;
                Vector2 playerPos = player.transform.position;
                playerPos.y -= fallAmount;
                player.transform.position = playerPos;
            }

            //Make sure any enemies fall with the ground their standing on
            foreach (enemy o in enemies)
            {
                if (o != null) 
                { 
                    Vector2 iPos = o.transform.position;
                    iPos.y -= fallAmount;
                    o.transform.position = iPos;
                }
            }
            transform.position = pos;
        }
        else
        {
            if(player != null)
            {
                isFalling = true;
            }
        }
    }
}
