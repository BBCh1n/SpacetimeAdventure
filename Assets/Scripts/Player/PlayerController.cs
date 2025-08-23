using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public float runSpeed = 5;
    public float jumpSpeed = 7;
    public float bounceRate = 1.25f;
    public float acceleration = 10;
    public float deceleration = 15;

    public bool canMove = false;
    public bool isGrounded = true;
    public bool isBlocked = false;

    private bool moveRight = true;
    private bool jumpRequest = false;
    private bool isJumping = false;
    private bool isFalling = false;

    private float moveDirection;

    private Rigidbody2D rb;
    private Animator anim;
    public RelativityController rc;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rc = GetComponent<RelativityController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }

        moveDirection = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Run();
            Jump();
        }

        Fall();

        if (!jumpRequest && isGrounded && rb.velocity.y > 0 && rb.velocity.y < jumpSpeed * 0.9)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        rc.SetObjectVel(rb.velocity);
    }

    void Flip()
    {
        if (moveDirection > 0 && !moveRight)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            moveRight = true;
        }
        else if (moveDirection < 0 && moveRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            moveRight = false;
        }
    }

    void Run()
    {
        bool isRunning = isGrounded && Mathf.Abs(moveDirection) > Mathf.Epsilon;
        anim.SetBool("Run", isRunning);

        Flip();

        float targetSpeed = moveDirection * runSpeed;
        if (isBlocked && moveDirection * (moveRight ? 1 : -1) > 0)
        {
            targetSpeed = 0;
        }

        float currentSpeedX = rb.velocity.x;

        if (!GameController.Instance.hasInertia)
        {
            currentSpeedX = targetSpeed;
        }
        else
        {
            if (moveDirection != 0)
            {
                currentSpeedX = Mathf.MoveTowards(currentSpeedX, targetSpeed, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                currentSpeedX = Mathf.MoveTowards(currentSpeedX, 0, deceleration * Time.fixedDeltaTime);
            }
        }

        rb.velocity = new Vector2(currentSpeedX, rb.velocity.y);
    }

    void Jump()
    {
        if (jumpRequest)
        {
            jumpRequest = false;
            isJumping = true;
            anim.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            AudioController.Instance.PlaySFX(AudioController.Instance.playerJumpSFX);
        }
        else if (isJumping && isGrounded)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }
    }

    void Fall()
    {
        if (!isGrounded && rb.velocity.y < -Mathf.Epsilon)
        {
            isFalling = true;
            anim.SetBool("Fall", true);
        }
        else if (isFalling && isGrounded)
        {
            isFalling = false;
            anim.SetBool("Fall", false);
        }
    }

    public void BounceUp()
    {
        isJumping = true;
        anim.SetBool("Jump", true);
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * bounceRate);
        AudioController.Instance.PlaySFX(AudioController.Instance.playerStepSFX);
    }

    public void OnAppearAnimEnd()
    {
        canMove = true;
    }
}
