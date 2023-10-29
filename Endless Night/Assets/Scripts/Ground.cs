using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;

    public float groundHeight;
    public float groundRight;
    public float screenRight;
    BoxCollider2D colliderG;

    bool isGroundGenerated = false;

    public obstacle obstacleHolder;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        colliderG = GetComponent<BoxCollider2D>();
        
        screenRight = Camera.main.transform.position.x * 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        groundHeight = transform.position.y + (colliderG.size.y / 2);
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;


        groundRight = transform.position.x + (colliderG.size.x / 2);

        //If ground is out of bounds
        if (groundRight < 0)
        {
            Destroy(gameObject);
            return;
        }

        if (!isGroundGenerated)
        {
            if (groundRight < screenRight)
            {
                isGroundGenerated = true;
                generateGround();
            }
        }

        transform.position = pos;
    }
    //Create new level grounds as game goes on
    void generateGround()
    {
        //Build new ground for the level based on the current one as a reference
        GameObject build = Instantiate(gameObject);
        BoxCollider2D newCollider = build.GetComponent<BoxCollider2D>();
        Vector2 pos;

        float h1 = player.jumpVelocity * player.maxHoldJumpTimer;
        //ratio of how long it should take player jump velocity to become zero
        float t = player.jumpVelocity / -player.gravity;
        float h2 = player.jumpVelocity * t + ((1 / 2) * (player.gravity * (t * t)));
        float maxJumpHeight = h1 + h2;
        float maxGroundHeight = maxJumpHeight * 0.7f;
        maxGroundHeight += groundHeight;
        float minGroundHeight = 1;
        //New random ground height 
        float newGroundHeight = Random.Range(minGroundHeight, maxGroundHeight);

        pos.y = newGroundHeight - newCollider.size.y / 2;
        //makes sure new ground height isn't too high
        if (pos.y > 2.7f)
        {
            pos.y = 2.7f;
        }

        //Time until maxGroundHeight to newGroundHeight while holding the jump button
        float t1 = t + player.maxHoldJumpTimer;
        float t2 = Mathf.Sqrt((2.0f * (maxGroundHeight - newGroundHeight)) / -player.gravity);
        float totalTime = t1 - t2;

        //Controls how far the new ground will spawn
        float maxGroundDistance = totalTime * player.velocity.x;
        maxGroundDistance *= 0.7f;
        maxGroundDistance += groundRight;
        float minGroundDistance = screenRight + 5;
        float newGroundDistance = Random.Range(minGroundDistance, maxGroundDistance);
        pos.x = newGroundDistance + newCollider.size.x / 2;


        build.transform.position = pos;

        Ground newGround = build.GetComponent<Ground>();
        newGround.groundHeight = build.transform.position.y + (newCollider.size.y / 2);

        //make sure fall data isn't copied to next build of ground
        GroundFall fall =  fall = build.GetComponent<GroundFall>();
        if (fall != null)
        {
            Destroy(fall);
            fall = null;
        }
        
        //Determines which ground will fall
        if (Random.Range(0,3) == 0)
        {
            fall = build.AddComponent<GroundFall>();
            fall.fallSpeed = Random.Range(1.0f, 3.0f);
        }

        //How many obstacles will be created
        int obstacleNum = Random.Range(0, 4);
        for (int i = 0; i < obstacleNum; i++)
        {
            GameObject box = Instantiate(obstacleHolder.gameObject);
            float y = newGround.groundHeight;
            float halfWidth = newCollider.size.x / 2 - 1;
            float left = build.transform.position.x - halfWidth;
            float right = build.transform.position.x + halfWidth;
            float x = Random.Range(left, right);
            Vector2 boxPos = new Vector2(x, y);
            box.transform.position = boxPos;

            //update obstacles if ground is falling
            if (fall != null) 
            { 
                obstacle o = box.GetComponent<obstacle>();
                fall.obstacles.Add(o);
            }
        }
    }
}
