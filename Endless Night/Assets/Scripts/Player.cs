using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public GameObject player;

    public AudioSource swordKill;
    public AudioSource hurt;
    public AudioSource slide;
    public AudioSource crumble;
    //public AudioSource jump;

    public float distance = 0;
    public float killCount = 0;
    public int lives = 1;

    public float gravity;
    public Vector2 velocity;
    public float maxVelocity = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    
    public float jumpVelocity = 20;
    public float groundHeight = 15;
    public float holdJumpTimer = 0.4f;
    public float maxHoldJumpTimer = 0.4f;
    public float holdingJumpTimer = 0.0f;
    public float jumpGroundThreshold = 1;


    public bool wasAttacked = false;
    public bool isSliding = false;
    public bool isAttacking = false;
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public bool isDead = false;

    public float groundOffsetY = 2f;
    public LayerMask groundsLayerMask;
    public LayerMask obstaclesLayerMask;
    GroundFall fall;

    CameraController cameraController;
    //playerSpawner spawner;
    //public GameObject tracker;
    //private playerSpawner spawnPoint;
    //public float speed;
    //private float Move;

    //SpriteRenderer spriter;
    //Animator anim;
    //public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //player.transform.position = new Vector3(7.9f, 9.6f, 0f);
        //rb = GetComponent<Rigidbody2D>();
        //spriter = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        cameraController = Camera.main.GetComponent<CameraController>();
        //spawnPoint = tracker.GetComponent<playerSpawner>();
    }
    void Awake()
    {
        //this.GetComponent<Animator>().enabled = false;
        //transform.position = new Vector3(7.9f, 9.6f, 0f);
        //this.GetComponent<Animator>().enabled = true;
        //spawner = GameObject.Find("Respawn").GetComponent<playerSpawner>();
        //transform.position = spawner.pos;
        //transform.position = spawnPoint.pos;
        /*
        GameObject[] positionObj = GameObject.FindGameObjectsWithTag("Player");
        if (positionObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        */
        print("player current position: " + transform.position);
        //player.transform.position = new Vector3(7.9f, 9.6f, 0f);
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
            //death.Play();
            anim.Play("death");
            //return;
        }
        if (!isDead)
        {
            wasAttacked = false;
            anim.SetBool("wasAttacked", false);
        }

        if (pos.y < -2 || lives == 0)
        {
            isDead = true;
            velocity.x = 0;
            //anim.SetBool("isDead", true);
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
            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y - groundOffsetY);

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
                        groundHeight = ground.groundHeight + groundOffsetY;
                        pos.y = groundHeight;
                        velocity.y = 0;
                        isGrounded = true;
                        anim.SetBool("isGrounded", true);
                    }
                    fall = ground.GetComponent<GroundFall>();
                    if(fall != null)
                    {
                        crumble.Play();
                        fall.player = this;
                        cameraController.startShaking();
                    }
                }
            }

            //Debug for checking if there is ground beneath player
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

            Vector2 wallOrigin = new Vector2(pos.x, pos.y - groundOffsetY);
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
        /*
        float checkDist = distance -1;
        if(distance > checkDist)
        {

        }
        */
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

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y - groundOffsetY);

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
    
       enemyCollisionDetection(pos, isAttacking);
        
        //Resets player position
        transform.position = pos;
    }

    void playerController(Vector2 pos)
    {
        //Mute or unmute background music
        if (Input.GetKeyUp(KeyCode.M))
        {
            if (cameraController.backgrndMusic.mute == false)
            {
                cameraController.backgrndMusic.mute = true;
            }
            else if (cameraController.backgrndMusic.mute == true)
            {
                cameraController.backgrndMusic.mute = false;
            }
        }

        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            //Holding jump button
            if (Input.GetKey(KeyCode.J))
            {
                //jump.Play();
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
                slide.Play();
                isSliding = true;
                velocity.x *= 0.7f;
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


    void enemyCollisionDetection(Vector2 pos, bool attacking)
    {
        //Checks if player hits/attacks an enemy
        Vector2 enmyOrigin = new Vector2(pos.x, pos.y - groundOffsetY);
        RaycastHit2D obstHitX = Physics2D.Raycast(enmyOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, obstaclesLayerMask);
        if (obstHitX.collider != null)
        {
            enemy enemies = obstHitX.collider.GetComponent<enemy>();
            if (enemies != null && attacking == true)
            {
                atkEnemy(enemies);
            }
            else if (enemies != null && attacking == false)
            {
                hitEnemy(enemies);
            }
        }

        RaycastHit2D obstHitY = Physics2D.Raycast(enmyOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime, obstaclesLayerMask);
        if (obstHitY.collider != null)
        {
            enemy enemies = obstHitY.collider.GetComponent<enemy>();
            if (enemies != null && attacking == true)
            {
                atkEnemy(enemies);
            }
            else if(enemies != null && attacking == false)
            {
                hitEnemy(enemies);
            }
        }
    }
    
    void hitEnemy(enemy enemies)
    {
        hurt.Play();
        wasAttacked = true;
        anim.SetBool("wasAttacked", true);
        //anim.Play("Hurt");
        lives--;
        Destroy(enemies.gameObject);
        //velocity.x *= 0.7f;
        
    }

    void atkEnemy(enemy enemies)
    {
        swordKill.Play();
        Destroy(enemies.gameObject);
        killCount++;
    }
    /*
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            rb.gravityScale = 0;

        }
    }
    */
    void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("platform"))
        {
            rb.gravityScale = 0;
            
        }
        */
        /*
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collision detected");
            Destroy(collision.gameObject);
            killCount++;
            //velocity.x *= 0.7f;
        }
        */
    }
    
}