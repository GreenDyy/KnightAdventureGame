using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float pressHorizontal;
    public float yVelocity;
    public float speed = 6f;
    public float jumpPower = 19f;

    public bool isAttack = false;
    public bool isRight = true;

    //for DASH
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 24f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;
    private float nextDashTime;
    float originalGravity;

    public static PlayerMovement instance;
    public Animator animator;
    public Transform wallCheck;
    public Transform groundCheck;

    public bool isWallSliding;
    public float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    public LayerMask groundLayer;
    public LayerMask wallLayer;
    [SerializeField]
    private TrailRenderer trailRenderer;
    private Rigidbody2D rb;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
        wallLayer = LayerMask.GetMask("Wall");

        wallCheck = transform.Find("WallCheck");
        groundCheck = transform.Find("GroundCheck");
    }
    private void Update()
    {
        yVelocity = rb.velocity.y;
        //Movement
        Movement();
        //Xử lý Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            AudioManager.instance.PlaySFX(AudioManager.instance.playerJump);
        }

        //Wall interact
        WallSlide();
        WallJump();
        //Quay người
        if (!isWallJumping)
        {
            FlipPlayer();
        }
        FlipPlayer();

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftAlt) && canDash)
        {
            //StartCoroutine(Dash());
            if (Time.time >= nextDashTime)
            {
                Dash2();
                nextDashTime = Time.time + dashCooldown;
            }
        }
        //Set animation
        SetAnimationState();
    }
    private void FixedUpdate()
    {
        if(isDashing) // để ngăn ko cho làm hành động khác khi dash
        {
            return;
        }    
        //UpdateSpeed();

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(pressHorizontal * speed, rb.velocity.y);
        }
        //Set SFX
        //if (pressHorizontal != 0)
        //{
        //    Invoke(nameof(PlaySoundMovement), 0.1f);
        //}
        //else
        //{
        //    CancelInvoke(nameof(PlaySoundMovement));
        //}
    }
    //void PlaySoundMovement()
    //{
    //    AudioManager.instance.SFXSource.clip = AudioManager.instance.playerWalk;
    //    AudioManager.instance.SFXSource.Play();
    //}
    private void Movement()
    {
        //MOVE WITH Mũi tên
        pressHorizontal = Input.GetAxis("Horizontal");

        //MOVE WITH WASD
        if (Input.GetKey("a"))
        {
            pressHorizontal = -1;
        }
        else if (Input.GetKey("d"))
        {
            pressHorizontal = 1;
        }
        else
        {
            pressHorizontal = 0;
        }

    }
    private void UpdateSpeed()
    {
        rb.velocity = new Vector2(speed * pressHorizontal, rb.velocity.y);
    }

    private void FlipPlayer()
    {
        if (isRight && pressHorizontal < 0 || !isRight && pressHorizontal > 0)
        {
            isRight = !isRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
    public bool IsFalling()
    {
        if (rb.velocity.y < 0)
            return true;
        return false;
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && pressHorizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            animator.SetBool("IsWallSlide", true);
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("IsWallSlide", false);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallSliding = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isRight = !isRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        //dash xog thì đặt lại giá trị
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private void Dash2()
    {
        isDashing = true;
        originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        trailRenderer.emitting = true;
        Invoke(nameof(ResetDash2), 0.2f);
    }
    private void ResetDash2()
    {
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private void OnDrawGizmos()// just show in scene
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(wallCheck.position, new Vector2(0.2f, 1.8f));
        Gizmos.DrawWireSphere(groundCheck.position, 0.15f);
    }

    void SetAnimationState() // liên quan tới rigidbody x y (-1, 1)
    {
        animator.SetFloat("Move", Mathf.Abs(pressHorizontal));
        if (rb.velocity.y == 0f)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
        if (rb.velocity.y > 0f)
        {
            animator.SetBool("IsJumping", true);
        }
        if (rb.velocity.y < 0f)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        //animaton Dash
        if(isDashing)
        {
            animator.SetBool("IsDashing", true);
        }
        else
        {
            animator.SetBool("IsDashing", false);
        }
    }
}
