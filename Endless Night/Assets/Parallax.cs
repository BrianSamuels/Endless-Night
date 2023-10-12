using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
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
        //Sets the velocity of scene object to be less than the players velocity
        float sceneVelocity = player.velocity.x / depth;

        Vector2 pos = transform.position;

        //Moves scene object in the opposite direction of the player
        pos.x -= sceneVelocity * Time.fixedDeltaTime;

        if(pos.x <= -48)
        {
            pos.x = 114;
        }
        transform.position = pos;
    }
}
