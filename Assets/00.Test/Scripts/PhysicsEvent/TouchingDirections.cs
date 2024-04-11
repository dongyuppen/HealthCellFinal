using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    // Contact filter used for raycasting
    public ContactFilter2D castFilter;
    // Distance threshold for ground detection
    public float groundDistance = 0.05f;
    // Distance threshold for wall detection
    public float wallDistance = 0.2f;
    // Distance threshold for ceiling detection
    public float ceilingDistance = 0.05f;

    // Reference to the capsule collider attached to the object
    CapsuleCollider2D touchingCol;
    // Reference to the animator component
    Animator animator;

    // Arrays to store hit results from raycasts
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];


    // Flag indicating whether the object is grounded
    [SerializeField]
    private bool _isGrounded;
    
    public bool IsGrounded { get { 
            return _isGrounded;
        } private set { 
            _isGrounded = value;
            animator.SetBool(AnimationsStrings.isGrounded, value);
        } }

    // Flag indicating whether the object is touching a wall
    [SerializeField]
    private bool _isOnWall;

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationsStrings.isOnWall, value);
        }
    }

    // Flag indicating whether the object is touching a ceiling
    [SerializeField]
    private bool _isOnCeiling;
    // Direction for wall checking based on the object's scale
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationsStrings.isOnCeiling, value);
        }
    }


    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    

    void FixedUpdate()
    {
        // Perform raycasts to detect ground, walls, and ceilings
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
