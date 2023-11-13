using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float maxVelocity = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;
    public float killCount = 0;
    public float jumpVelocity = 20;
    public float groundHeight = 12;
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public float holdJumpTimer = 0.4f;
    public float maxHoldJumpTimer = 0.4f;
    public float holdingJumpTimer = 0.0f;
    public float jumpGroundThreshold = 1;

    public bool isDead = false;

    public LayerMask groundsLayerMask;
    public LayerMask obstaclesLayerMask;

    GroundFall fall;

    CameraController cameraController;
    //public float speed;
    //private float Move;

    //SpriteRenderer spriter;
    //Animator anim;
    //private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //spriter = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(speed * Move, rb.velocity.y);
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            //Holding jump button
            if (Input.GetKey(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdingJumpTimer = 0;

                //make sure player isn't falling while jumping
                if(fall != null)
                {
                    fall.player = null;
                    fall = null;
                    cameraController.stopShaking();
                }
            }

        }

        //Not holding jump button
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if (isDead)
        {
            return;
        }

        if(pos.y < -20)
        {
            isDead = true;
        }

        if (!isGrounded)
        {
            //Forces Jump button to false if held for too long
            if (isHoldingJump)
            {
                holdingJumpTimer += Time.fixedDeltaTime;
                if(holdingJumpTimer >= holdJumpTimer)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;

            //Gravity only works when not holding jump button(spacebar)
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            //Raycast used for jump, to detect if their is ground beneath player
            //Player ray origin located just in front of the player
            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);

            Vector2 rayDirection = Vector2.up;

            float rayDistance = velocity.y * Time.fixedDeltaTime;

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundsLayerMask);

            if(hit2D.collider != null)
            {
                //looks for the ground
                Ground ground = hit2D.collider.GetComponent<Ground>();

                if(ground != null) 
                {
                    if(pos.y >= ground.groundHeight)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        velocity.y = 0;
                        isGrounded = true;
                    }
                    fall = ground.GetComponent<GroundFall>();
                    if(fall != null)
                    {
                        fall.player = this;
                        cameraController.startShaking();
                    }
                }
            }

            //Debug for checking if there is ground beneath player
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
            if (wallHit.collider != null)
            {
                Ground ground = wallHit.collider.GetComponent<Ground>();
                if(ground != null)
                {
                    if (pos.y < ground.groundHeight)
                    {
                        velocity.x = 0;
                    }
                }
            }
        }

        // Keeps track of distance traveled

        //pos.x += velocity.x * Time.fixedDeltaTime;

        distance += velocity.x * Time.fixedDeltaTime;
        //distance += pos.x;
        if (isGrounded)
        {
            //slow down acceleration
            float velocityRatio = velocity.x / maxVelocity;

            //eventually reduces acceleration to zero
            acceleration = maxAcceleration * (1 - velocityRatio);

            holdJumpTimer = maxHoldJumpTimer * velocityRatio;

            velocity.x += acceleration * Time.fixedDeltaTime;
            
            if(velocity.x >= maxVelocity)
            {
                velocity.x = maxVelocity;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);

            Vector2 rayDirection = Vector2.up;

            float rayDistance = velocity.y * Time.fixedDeltaTime;

            //Adjust rayDistance if a ground is falling
            if (fall != null)
            {
                rayDistance = -fall.fallSpeed * Time.fixedDeltaTime;
            }

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundsLayerMask);

            //Player falls if not colliding with anything
            if (hit2D.collider == null)
            {
                isGrounded = false;
            }

            //Debug for checking if not on ground
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        //Checks if player hit an obstacle
        Vector2 obstOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, obstaclesLayerMask);
        if (obstHitX.collider != null)
        {
            obstacle obstacles = obstHitX.collider.GetComponent<obstacle>();
            if (obstacles != null)
            {
                hitObstacle(obstacles);
            }
        }

        RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime, obstaclesLayerMask);
        if (obstHitY.collider != null)
        {
            obstacle obstacles = obstHitY.collider.GetComponent<obstacle>();
            if (obstacles != null)
            {
                hitObstacle(obstacles);
            }
        }
        //Resets player position
        transform.position = pos;
    }

    void hitObstacle(obstacle obstacles)
    {
        Destroy(obstacles.gameObject);
        killCount++;
        velocity.x *= 0.7f;
    }
    /*
    private void LateUpdate()
    {
        
        anim.SetFloat("Speed", Move);

        if(Move != 0)
        {
            spriter.flipX = Move < 0;
        }
        
    }
    */
}