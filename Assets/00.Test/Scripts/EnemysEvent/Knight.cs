using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RequireComponent attribute ensures that the specified components are automatically added if they are missing
[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections), typeof(EnemyDamageable))]
public class Knight : MonoBehaviour
{
    // Movement parameters
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;

    // Reference to detection zones for attack and cliff detection
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    // References to required components
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    EnemyDamageable damageable;

    // Enumeration to represent the direction the knight is walking
    public enum WalkableDirection {Right, Left}

    // Private fields for tracking walk direction and whether the knight has a target
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set { 
            if (_walkDirection != value)
            {
                // Flip the sprite horizontally when changing direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                // Update walk direction vector
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
                
            
            _walkDirection = value; }
    }

    public bool _hasTarget = false;

    // Property to check if the knight has a target
    public bool HasTarget {
        get { return _hasTarget; } 
        private set 
        {
            _hasTarget = value;
            // Set animator parameter to indicate whether the knight has a target
            animator.SetBool(AnimationsStrings.hasTarget, value);
        } 
    }

    // Property to check if the knight can move
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationsStrings.canMove);
        }
    }

    // Property to get and set the attack cooldown
    public float AttackCooldown { get 
        {
            return animator.GetFloat(AnimationsStrings.attackCooldown);
        } private set 
        {
            animator.SetFloat(AnimationsStrings.attackCooldown, Mathf.Max(value, 0));
        } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<EnemyDamageable>();
    }

    private void Update()
    {
        // Update whether the knight has a target based on detected colliders in the attack zone
        HasTarget = attackZone.detectedColliders.Count > 0;

        // Reduce attack cooldown over time
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // If the knight is grounded and touching a wall, flip direction
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        // Adjust velocity based on movement parameters
        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.IsGrounded)
            {
                // Accelerate towards max Speed
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
            }
            else
            {
                // Slow down gradually when not moving or not grounded
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }

    // Method to flip the direction the knight is facing
    private void FlipDirection()
    {
        // Toggle between left and right directions
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            // Log an error if the walkable direction is not set correctly
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    // Method called when the knight is hit
    public void OnHit(int damage, Vector2 knockback)
    {
        // Apply knockback force
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    // Method called when a cliff is detected
    public void OnCliffDetected()
    {
        // If grounded, flip direction when detecting a cliff
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
