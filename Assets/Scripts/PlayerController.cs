using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        initializeDefaults();
    }

    private void Update()
    {
        //Timers
        updateTimers();

        if(isGrounded() && rb.velocity.y <= 0f)
        {
            isJumping = false;
            jumpTimer = coyoteTime;
        }

        //Input
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpInputTimer = maxJumpInputDelay;
        }

        //Jump logic
        if(jumpInputTimer > 0f && canJump())
        {
            jump();
        }

        if(isJumping)
        {
            if(Mathf.Abs(rb.velocity.y) < 0.5f)
            {
                rb.gravityScale = peakGravityScale;
            } 
            
            else if(rb.velocity.y < 0f)
            {
                rb.gravityScale = fallingGravityScale;
            }

            else
            {
                rb.gravityScale = 1f;
            }
        }

        if(rb.velocity.y > 1.5f)
        {
            transform.localScale = new Vector3(jumpSqueezeScale, defaultScale.y, defaultScale.z);
        } 
        
        else if(Mathf.Abs(rb.velocity.y) < 0.2f && lastFrameVelocity.y < -1.5f)
        {
            landSquashTimer = landSquashAnimationTime;
            transform.localScale = new Vector3(defaultScale.x, landSquashScale, defaultScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y - defaultScale.y + landSquashScale, transform.position.z);
        }

        else if(landSquashTimer <= 0f)
        {
            if(transform.localScale.y == landSquashScale)
            {
                transform.position = new Vector3(lastFramePosition.x, lastFramePosition.y + 0.1f, lastFramePosition.z);
            }

            transform.localScale = defaultScale;
        }

        lastFrameVelocity = rb.velocity;
        lastFramePosition = transform.position;
    }

    private void FixedUpdate()
    {
        horizontalMovement();
    }

    private void initializeDefaults()
    {
        defaultGravityScale = rb.gravityScale;
        defaultScale = transform.localScale;
    }

    private void horizontalMovement()
    {
        float targetSpeed = maxRunSpeed * horizontalInput;
        float acceleration = accelerationRate;

        if(Mathf.Abs(targetSpeed) < 0.1f)
        {
            acceleration = deccelerationRate;
        }

        float forceToApply = (targetSpeed - rb.velocity.x) * acceleration;

        rb.AddForce(Vector2.right * forceToApply, ForceMode2D.Force);
    }

    private void jump()
    {
        isJumping = true;

        float targetJumpForce = Mathf.Max(jumpStrength, jumpStrength - rb.velocity.y);
        rb.AddForce(Vector2.up * targetJumpForce, ForceMode2D.Impulse);
    }

    private void updateTimers()
    {
        jumpTimer -= Time.deltaTime;
        jumpInputTimer -= Time.deltaTime;
        landSquashTimer -= Time.deltaTime;

        jumpTimer = Mathf.Max(jumpTimer, 0f);
        jumpInputTimer = Mathf.Max(jumpInputTimer, 0f);
        landSquashTimer = Mathf.Max(landSquashTimer, 0f);
    }

    private bool canJump()
    {
        return jumpTimer > 0f && !isJumping;
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0f, groundLayer);
    }

    //Variables
    private Rigidbody2D rb;
    private float horizontalInput;

    private bool isJumping;
    private float jumpTimer;
    private float jumpInputTimer;

    private float landSquashTimer;

    private Vector3 defaultScale;

    private float defaultGravityScale;

    private Vector2 lastFrameVelocity;
    private Vector3 lastFramePosition;

    [Header("Run")]
    [SerializeField] private float maxRunSpeed;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float deccelerationRate;

    [Header("Jump")]
    [SerializeField] private float jumpStrength;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float maxJumpInputDelay; //if you press the jump input maxJumpInputDelay seconds or less before hitting the ground,
                                                      //you will automatically jump on the next frame

    [SerializeField] private float jumpSqueezeScale; //the player's X scale is changed to this when he jumps
    [SerializeField] private float landSquashScale; //the player's Y scale is changed to this when he lands
    [SerializeField] private float landSquashAnimationTime; //for how long should the okayer be squashed after landing

    [Header("Gravity")]
    [SerializeField] private float peakGravityScale;
    [SerializeField] private float fallingGravityScale;


    [Header("Checks")]
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;
}
