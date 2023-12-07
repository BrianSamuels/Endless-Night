using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        //Move enemy in the opposite direction of the player at the same speed as the ground it stands on
        //and destroy if they move out of bounds.
        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        if (pos.x < -100)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}
