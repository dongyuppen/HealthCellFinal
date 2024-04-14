using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// RequireComponent attribute ensures that the specified components are automatically added if they are missing
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    // Movement speed parameters
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;


    private bool canDash = true; // 대쉬 가능 여부
    private bool isDashing; // 대쉬 중일때 체크
    private float dashingPower = 20f; // 대쉬 파워
    private float dashingTime = 0.2f; // 대쉬 지속 시간
    private float dashingCooldown = 1f; // 대쉬 쿨타임

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    // Input value for movement
    Vector2 moveInput;

    // References to required components
    TouchingDirections touchingDirections;
    Damageable damageable;

    // Property to get the current movement speed based on player's state
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            // Grounded movement speed
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        // Air movement speed
                        return airWalkSpeed;
                    }
                }
                else
                {
                    // Idle speed is 0
                    return 0;
                }
            }
            else 
            {
                // Movement Locked
                return 0;
            }
        }
    }

    // Flag indicating whether the player is moving
    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationsStrings.isMoving, value);
        }
    }

    // Flag indicating whether the player is running
    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationsStrings.isRunning, value);
        }
    }

    // Flag indicating the direction the player is facing
    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight; } private set { 
            if (_isFacingRight != value)
            {
                // Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        
        } }

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        // If the player is not locked in velocity, update the velocity based on movement input
        if (!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

            // Update yVelocity parameter in animator
            animator.SetFloat(AnimationsStrings.yVelocity, rb.velocity.y);
        }
    }

    // Method called by the input system when move input is received
    public void OnMove(InputAction.CallbackContext context)
    {
        // Read the move input value
        moveInput = context.ReadValue<Vector2>();

        // Update movement flags and facing direction based on input
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    // Method to set the facing direction based on movement input
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face to the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // Face to the left
            IsFacingRight = false;
        }
    }

    // Property to check if the player can move
    public bool CanMove { get
        {
            return animator.GetBool(AnimationsStrings.canMove);
        }
    }

    // Property to check if the player is alive
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationsStrings.isAlive);
        }
    }

    // Method called by the input system when the run input action is started or canceled
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Start running
            IsRunning = true;
        }
        else if (context.canceled)
        {
            // Stop running
            IsRunning = false;
        }
    }

    // Method called by the input system when the jump input action is started
    public void OnJump(InputAction.CallbackContext context)
    {
        // Check if the player is alive, grounded, and can move
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            // Trigger the jump animation and apply jump impulse
            animator.SetTrigger(AnimationsStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        damageable.isInvincible = true; // 무적 활성화
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        damageable.isInvincible = false; // 무적 비활성화
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    // Method called by the input system when the attack input action is started
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Trigger the attack animation
            animator.SetTrigger(AnimationsStrings.attackTrigger);
        }
    }

    // Method called by the input system when the ranged attack input action is started
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Trigger the ranged attack animation
            animator.SetTrigger(AnimationsStrings.rangedAttackTrigger);
        }
    }

    // Method called when the player is hit
    public void OnHit(int damage, Vector2 knockback)
    {
        // Apply knockback force
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
