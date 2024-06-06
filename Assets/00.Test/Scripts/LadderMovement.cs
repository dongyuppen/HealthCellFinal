using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    public float climbspeed = 8f;
    private bool isLadder;
    private bool isClimbing;

    Animator animator;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D cd;
    TouchingDirections touchingDirections;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();


    }
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            animator.SetBool("isLadder", true);
            cd.isTrigger = true;
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbspeed);
        }
        else if(touchingDirections.IsGrounded)
        {
            rb.gravityScale = 2f;
            cd.isTrigger = false;
        }
        else
        {
            rb.gravityScale = 2f;
            cd.isTrigger = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            animator.SetBool("isLadder", false);
            isLadder = false;
            isClimbing = false;
           
        }
    }
}
