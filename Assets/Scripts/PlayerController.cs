using UnityEngine;

public class Movement : MonoBehaviour
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = playerSpriteObject.GetComponent<Animator>();

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

        if(horizontalInput < 0f)
        {
            //if the player is trying to move to the left, flip him
            transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        } 
        
        else if(horizontalInput > 0f)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

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
                rb.gravityScale = jumpGravityScale;
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            LevelManager.instance.RestartLevel();
        }

        handleAnimation();

        lastFrameVelocity = rb.velocity;
        lastFramePosition = transform.position;

        if(transform.position.y < deathYLevel)
        {
            LevelManager.instance.RestartLevel();
        }
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
        animator.SetTrigger("onJump");
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

    private void handleAnimation()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
    }

    private bool canJump()
    {
        return jumpTimer > 0f && !isJumping;
    }

    private bool isGrounded()
    {
        Collider2D[] results = Physics2D.OverlapBoxAll(groundCheckPos.position, groundCheckSize, 0f, groundLayer);

        foreach(Collider2D hitCollider in results)
        {
            if(!hitCollider.isTrigger)
            {
                return true;
            }
        }

        return false;
    }

    //Variables
    private Rigidbody2D rb;
    private Animator animator;

    private float horizontalInput;

    private bool isJumping;
    private float jumpTimer;
    private float jumpInputTimer;

    private float landSquashTimer;

    private Vector3 defaultScale;

    private float defaultGravityScale;

    private Vector2 lastFrameVelocity;
    private Vector3 lastFramePosition;

    [SerializeField] private GameObject playerSpriteObject; //the child gameobject responsible for the player's sprite and animations

    [SerializeField] private float deathYLevel;

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
    [SerializeField] private float jumpGravityScale;

    [Header("Checks")]
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;
}
