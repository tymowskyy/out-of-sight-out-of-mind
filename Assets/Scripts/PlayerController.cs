using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = playerSpriteObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        groundedThisFrame = false;

        initializeDefaults();
    }

    private void Update()
    {
        //Timers
        updateTimers();

        //Input
        horizontalInput = InputManager.instance.GetAxisRaw("Horizontal");

        if (horizontalInput != 0f)
        {
            if(shouldPlayRunSound())
            {
                audioSource.loop = true;
                audioSource.clip = walkSound;
                audioSource.Play();
            }

            if (horizontalInput < 0f)
            {
                //if the player is trying to move to the left, flip him
                transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }

            else if (horizontalInput > 0f)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        } 
        
        if(horizontalInput == 0f || !groundedThisFrame)
        {        
            if(audioSource.isPlaying && audioSource.clip == walkSound)
            {
                audioSource.loop = false;
                audioSource.Pause();
            }
        }

        if(InputManager.instance.GetKeyDown(KeyCode.Space))
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
            
            else
            {
                rb.gravityScale = jumpGravityScale;
            }
        }

        if(rb.velocity.y < 0f)
        {
            rb.gravityScale = fallingGravityScale;
        }

        if(InputManager.instance.GetKeyDown(KeyCode.R) && !PauseMenu.instance.isPaused())
        {
            StartCoroutine(die());
        }

        handleAnimation();

        lastFrameVelocity = rb.velocity;
        lastFramePosition = transform.position;

        if(transform.position.y < deathYLevel)
        {
            StartCoroutine(die());
        }
    }

    private void FixedUpdate()
    {
        groundedThisFrame = isGrounded();

        if(groundedThisFrame && rb.velocity.y <= 0f && !isStuck)
        {
            isJumping = false;
            jumpTimer = coyoteTime;
        }

        isStuck = false;
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

        if (!audioSource.clip || audioSource.clip.name != jumpSound.name)
        {
            audioSource.clip = jumpSound;
        }
        audioSource.loop = false;
        audioSource.Play();

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

    public IEnumerator die(float waitTime=0f)
    {
        this.enabled = false;
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        stopSoundEffects();

        yield return new WaitForSeconds(waitTime);

        playerSpriteObject.GetComponent<SpriteRenderer>().enabled = false;

        audioSource.clip = deathSound;
        audioSource.loop = false;

        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);

        LevelManager.instance.RestartLevel();
    }

    public void stopSoundEffects()
    {
        audioSource.Stop();
    }

    private bool canJump()
    {
        return jumpTimer > 0f && !isJumping;
    }

    private bool isGrounded()
    {
        return feetColliderOffset.index > 0;
    }

    private bool shouldPlayRunSound()
    {
        bool isPlayingAlready = audioSource.isPlaying && audioSource.clip.name == walkSound.name;
        bool isLooped = audioSource.loop;
        bool isMovingX = Mathf.Abs(rb.velocity.x) > 0f;

        return isMovingX && groundedThisFrame && (!isPlayingAlready || !isLooped) && !isJumping;
    }

    public Animator getAnimator()
    {
        return animator;
    }

    //Variables
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    private float horizontalInput;

    private bool groundedThisFrame;
    private bool isJumping;

    private float jumpTimer;
    private float jumpInputTimer;

    private float landSquashTimer;

    private Vector3 defaultScale;

    private float defaultGravityScale;

    private Vector2 lastFrameVelocity;
    private Vector3 lastFramePosition;

    public bool isStuck = false;
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
    [SerializeField] private PlayerColliderOffset feetColliderOffset;

    [Header("SoundEffects")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip deathSound;
}
