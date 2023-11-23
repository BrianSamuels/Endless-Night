using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public float gravity;
    public Vector2 velocity;
    public float maxVelocity = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;
    public float killCount = 0;
    public float jumpVelocity = 20;
    public float groundHeight = 12;
    public bool isSliding = false;
    public bool isAttacking = false;
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
    //public Rigidbody2D rb;

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
        playerController(pos);
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
                    anim.SetBool("isJumping", false);
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
                        anim.SetBool("isGrounded", true);
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
                anim.SetBool("isGrounded", false);
            }

            //Debug for checking if not on ground
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        enemyCollisionDetection(pos);
        
        //Resets player position
        transform.position = pos;
    }

    void playerController(Vector2 pos)
    {
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            //Holding jump button
            if (Input.GetKey(KeyCode.J))
            {

                isGrounded = false;
                anim.SetBool("isGrounded", false);
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                anim.SetBool("isJumping", true);
                holdingJumpTimer = 0;

                //make sure player isn't falling while jumping
                if (fall != null)
                {
                    fall.player = null;
                    fall = null;
                    cameraController.stopShaking();
                }
            }

            //Controls player attack
            if (Input.GetKeyDown(KeyCode.K))
            {
                isAttacking = true;
                anim.SetBool("isAttacking", true);
                anim.speed = 2;
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                isAttacking = false;
                anim.SetBool("isAttacking", false);
                anim.speed = 1;
            }

            //Controls player slide
            if (Input.GetKeyDown(KeyCode.L))
            {
                isSliding = true;
                anim.SetBool("isSliding", true);
                //anim.speed = 2;
            }
            if (Input.GetKeyUp(KeyCode.L))
            {
                isSliding = false;
                anim.SetBool("isSliding", false);
                //anim.speed = 1;
            }
        }

        //Not holding jump button
        if (Input.GetKeyUp(KeyCode.J))
        {
            isHoldingJump = false;
            anim.SetBool("isJumping", false);
        }
    }


    void enemyCollisionDetection(Vector2 pos)
    {
        //Checks if player hit an obstacle
        Vector2 enmyOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D obstHitX = Physics2D.Raycast(enmyOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, obstaclesLayerMask);
        if (obstHitX.collider != null)
        {
            enemy enemies = obstHitX.collider.GetComponent<enemy>();
            if (enemies != null)
            {
                hitEnemy(enemies);
            }
        }

        RaycastHit2D obstHitY = Physics2D.Raycast(enmyOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime, obstaclesLayerMask);
        if (obstHitY.collider != null)
        {
            enemy enemies = obstHitY.collider.GetComponent<enemy>();
            if (enemies != null)
            {
                hitEnemy(enemies);
            }
        }
    }
    
    void hitEnemy(enemy enemies)
    {
        Destroy(enemies.gameObject);
        killCount++;
        velocity.x *= 0.7f;
    }
    
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collision detected");
            Destroy(collision.gameObject);
            killCount++;
            velocity.x *= 0.7f;
        }
    }
    */
}