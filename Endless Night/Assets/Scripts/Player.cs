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
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public float holdJumpTimer = 0.4f;
    public float maxHoldJumpTimer = 0.4f;
    public float holdingJumpTimer = 0.0f;
    public float jumpGroundThreshold = 1;

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

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

            if(hit2D.collider != null)
            {
                //looks for the ground
                Ground ground = hit2D.collider.GetComponent<Ground>();

                if(ground != null) 
                {
                    groundHeight = ground.groundHeight;
                    velocity.y = 0;
                    isGrounded = true;
                }
            }

            //Debug for checking if there is ground beneath player
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
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

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

            //Player falls if not colliding with anything
            if (hit2D.collider == null)
            {
                isGrounded = false;
            }

            //Debug for checking if not on ground
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        

        //Resets player position
        transform.position = pos;
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