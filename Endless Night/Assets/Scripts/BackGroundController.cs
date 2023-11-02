using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{   
    //Represents if on the same or different layer than player
    public float depth = 1;

    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
        if(pos.x <= -25)
        {
            pos.x = 80;
        }
        transform.position = pos;
    }
}
