using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    public float climbspeed = 8f;
    private bool isLadder;
    private bool isClimbing;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D cd;
    TouchingDirections touchingDirections;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        touchingDirections = GetComponent<TouchingDirections>();



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
            cd.isTrigger = true;
            rb.gravityScale = 0f;
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
            isLadder = false;
            isClimbing = false;
           
        }
    }
}
