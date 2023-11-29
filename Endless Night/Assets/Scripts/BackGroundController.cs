using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    //Represents if on the same or different layer than player
    public float depth = 1;
    public float exitDistance;
    public float enterDistance;
    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Sets the speed of the background objects to be less than the players velocity
        float backGroundSpeed = player.velocity.x / depth;

        Vector2 pos = transform.position;

        //Moves background objects in the opposite direction of the player
        pos.x -= backGroundSpeed * Time.fixedDeltaTime;

        //Resets background from left to right when out of bounds
        if(pos.x <= exitDistance)
        {
            if(this.GetComponent<SpriteRenderer>().flipX == false)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            pos.x = enterDistance;
        }
        transform.position = pos;
    }
}
